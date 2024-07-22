using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Net.Http.Headers;
using Microsoft.Owin.Security.Google;
using PAKDial.ExceptionHandling.Logging;
using PAKDial.Implementation.CommonServices;
using PAKDial.Implementation.PakDialServices;
using PAKDial.Implementation.PakDialServices.CompaniesListingsService;
using PAKDial.Implementation.PakDialServices.Configuration;
using PAKDial.Implementation.PakDialServices.Dashboard;
using PAKDial.Implementation.PakDialServices.Dashboard.Admin;
using PAKDial.Implementation.PakDialServices.Dashboard.CEO;
using PAKDial.Implementation.PakDialServices.Dashboard.Others;
using PAKDial.Implementation.PakDialServices.Dashboard.RegionalManager;
using PAKDial.Implementation.PakDialServices.Dashboard.Teller;
using PAKDial.Implementation.PakDialServices.Dashboard.ZoneManager;
using PAKDial.Implementation.PakDialServices.Home;
using PAKDial.Implementation.PakDialServices.HomeLandingPageService;
using PAKDial.Implementation.PakDialServices.QueryTrack;
using PAKDial.Implementation.PakDialServices.SearchesBehaviour;
using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.Logger;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Interfaces.PakDialServices.Configuration;
using PAKDial.Interfaces.PakDialServices.Dashboard;
using PAKDial.Interfaces.PakDialServices.Dashboard.Admin;
using PAKDial.Interfaces.PakDialServices.Dashboard.CEO;
using PAKDial.Interfaces.PakDialServices.Dashboard.Others;
using PAKDial.Interfaces.PakDialServices.Dashboard.RegionalManger;
using PAKDial.Interfaces.PakDialServices.Dashboard.ZoneManager;
using PAKDial.Interfaces.PakDialServices.Home;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using PAKDial.Interfaces.PakDialServices.IHomeLandingPageService;
using PAKDial.Interfaces.PakDialServices.IListingQuery;
using PAKDial.Interfaces.PakDialServices.ISearchesBehaviour;
using PAKDial.Interfaces.Repository;
using PAKDial.Interfaces.Repository.Configuration;
using PAKDial.Interfaces.Repository.Dashboard;
using PAKDial.Interfaces.Repository.Dashboard.Admin;
using PAKDial.Interfaces.Repository.Dashboard.CEO;
using PAKDial.Interfaces.Repository.Dashboard.Others;
using PAKDial.Interfaces.Repository.Dashboard.RegionalManager;
using PAKDial.Interfaces.Repository.Dashboard.Teller;
using PAKDial.Interfaces.Repository.Dashboard.ZoneManager;
using PAKDial.Interfaces.Repository.Home;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using PAKDial.Interfaces.Repository.IListingQuery;
using PAKDial.Interfaces.Repository.ISearchesBehaviour;
using PAKDial.Presentation.CommonServices;
using PAKDial.Presentation.Controllers;
using PAKDial.Presentation.Filters;
using PAKDial.Presentation.Hubs;
using PAKDial.Presentation.JobScheduling;
using PAKDial.Presentation.Services;
using PAKDial.Repository.BaseRepository;
using PAKDial.Repository.IdentityContext;
using PAKDial.Repository.Repositories;
using PAKDial.Repository.Repositories.CompaniesListingsRepo;
using PAKDial.Repository.Repositories.Configuration;
using PAKDial.Repository.Repositories.Dashboard;
using PAKDial.Repository.Repositories.Dashboard.Admin;
using PAKDial.Repository.Repositories.Dashboard.CEO;
using PAKDial.Repository.Repositories.Dashboard.RegionalManager;
using PAKDial.Repository.Repositories.Dashboard.Teller;
using PAKDial.Repository.Repositories.Dashboard.ZoneManager;
using PAKDial.Repository.Repositories.QueryTrack;
using PAKDial.Repository.Repositories.SearchesBehaviour;
using PAKDial.ZongServices.SmsService;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace PAKDial.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression();

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });
            services.AddDbContext<PAKDialSolutionsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PakDialConnectionSolution")));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PakDialConnectionSolution")));

            services.AddAuthentication()

           .AddGoogle(options =>
           {
               var _googleAuth = Configuration.GetSection("Authentication:Google");
               options.ClientId = _googleAuth["ClientId"];
               options.ClientSecret = _googleAuth["ClientSecret"];
               options.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
               options.SignInScheme = IdentityConstants.ExternalScheme;
           })
           .AddFacebook(options =>
           {
               var _facebookAuth = Configuration.GetSection("Authentication:Facebook");
               options.AppId = _facebookAuth["AppId"];
               options.AppSecret = _facebookAuth["AppSecret"];
           });

            services.AddIdentity<PAKDial.Domains.IdentityManagement.ApplicationUser, PAKDial.Domains.IdentityManagement.ApplicationRole>(
                option =>
                {
                    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    option.Lockout.MaxFailedAccessAttempts = 5;
                    option.Lockout.AllowedForNewUsers = false;
                    option.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "YourAppCookieName";
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });

            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddMvc().AddSessionStateTempDataProvider();
            //Registration Of Presentation Layer Services
            services.AddTransient<IEmailSender, EmailSender>();

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = long.MaxValue;
                x.BufferBodyLengthLimit = long.MaxValue;
                x.MemoryBufferThreshold = int.MaxValue;
            });

            //Registration of Repositories
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IStateProvinceRepository, StateProvinceRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ICityAreaRepository, CityAreaRepository>();
            services.AddTransient<IListingTypesRepository, ListingTypesRespository>();
            services.AddTransient<ITypeOfServicesRepository, TypeOfServicesRepository>();
            services.AddTransient<IVerificationTypesRepository, VerificationTypesRepository>();
            services.AddTransient<ISocialMediaModesRepository, SocialMediaModesRepository>();

            services.AddTransient<IDesignationRepository, DesignationRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IEmployeeAddressRepository, EmployeeAddressRepository>();
            services.AddTransient<IEmployeeContactRepository, EmployeeContactRepository>();

            services.AddTransient<IModulesRepository, ModulesRepository>();
            services.AddTransient<IRouteControlsRepository, RouteControlsRepository>();

            services.AddTransient<IUserTypeRepository, UserTypeRepository>();
            services.AddTransient<ISystemRoleRepository, SystemRoleRepository>();
            services.AddTransient<IRoleBasedPermissionRepository, RoleBasedPermissionRepository>();
            services.AddTransient<ISystemUserRepository, SystemUserRepository>();
            services.AddTransient<IUserBasedPermissionRepository, UserBasedPermissionRepository>();

            services.AddTransient<IMainMenuCategoryRepository, MainMenuCategoryRepository>();
            services.AddTransient<ISubMenuCategoryRepository, SubMenuCategoryRepository>();
            services.AddTransient<IHomeSectionCategoryRepository, HomeSectionCategoryRepository>();
            services.AddTransient<IHomeSecMainMenuCatRepository, HomeSecMainMenuCatRepository>();
            services.AddTransient<IOutSourceAdvertismentRepository, OutSourceAdvertismentRepository>();

            services.AddTransient<IListingPackagesRepository, ListingPackagesRepository>();
            services.AddTransient<IPackagePricesRepository, PackagePricesRepository>();
            services.AddTransient<IPaymentModesRepository, PaymentModesRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICategoryTypesRepository, CategoryTypesRepository>();

            services.AddTransient<IListingCategoryRepository, ListingCategoryRepository>();
            services.AddTransient<IListingPremiumRepository, ListingPremiumRepository>();
            services.AddTransient<ICompanyListingTimmingRepository, CompanyListingTimmingRepository>();
            services.AddTransient<IListingPaymentsModeRepository, ListingPaymentsModeRepository>();
            services.AddTransient<ISocialMediaModesRepository, SocialMediaModesRepository>();
            services.AddTransient<IListingServicesRepository, ListingServicesRepository>();
            services.AddTransient<IVerifiedListingRepository, VerifiedListingRepository>();
            services.AddTransient<IListingLandlineNoRepository, ListingLandlineNoRepository>();
            services.AddTransient<IListingMobileNoRepository, ListingMobileNoRepository>();
            services.AddTransient<IListingGalleryRepository, ListingGalleryRepository>();
            services.AddTransient<IListingAddressRepository, ListingAddressRepository>();
            services.AddTransient<ICompanyListingProfileRepository, CompanyListingProfileRepository>();
            services.AddTransient<ICompanyListingRepository, CompanyListingRepository>();
            services.AddTransient<IListingSocialMediaRepository, ListingSocialMediaRepository>();
            services.AddTransient<IZonesRepository, ZonesRepository>();
            services.AddTransient<IActiveZoneRepository, ActiveZoneRepository>();
            services.AddTransient<IBusinessTypesRepository, BusinessTypesRepository>();
            services.AddTransient<IListingsBusinessTypesRepository, ListingsBusinessTypesRepository>();
            services.AddTransient<ICompanyListingRatingRepository, CompanyListingRatingRepository>();
            services.AddTransient<ICompanyListingRatingRepository, CompanyListingRatingRepository>();

            //DashBoard
            services.AddTransient<ICompanyListingCountByUIdRepository, CompanyListingCountByUIdRepository>();
            services.AddTransient<ITellerDashboardRepository, TellerDashboardRepository>();
            services.AddTransient<IZoneManagerDashboardRepository, ZoneManagerDashboardRepository>();
            services.AddTransient<IRegionalManagerDashboardRepository, RegionalManagerDashboardRepository>();
            services.AddTransient<ICEODashboardRepository, CEODashboardRepository>();
            services.AddTransient<IAdminDashboardRepository, AdminDashboardRepository>();
            services.AddTransient<ISpGetGeneralResultantProcRepository, SpGetGeneralResultantProcRepository>();
            services.AddTransient<IVLoadHomePopularServiceRepository, VLoadHomePopularServiceRepository>();

            //Search Behaviour
            services.AddTransient<ISearchBehaviourRepository, SearchBehaviourRepository>();
            services.AddTransient<IListingQueryTrackRepository, ListingQueryTrackRepository>();


            //Registration of Services
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IStateProvinceService, StateProvinceService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<ICityAreaService, CityAreaService>();
            services.AddTransient<IListingTypesService, ListingTypesService>();
            services.AddTransient<ITypeOfServicesService, TypeOfServicesService>();
            services.AddTransient<IVerificationTypesService, VerificationTypesService>();
            services.AddTransient<ISocialMediaModesService, SocialMediaModesService>();

            services.AddTransient<IDesignationService, DesignationService>();
            services.AddTransient<IEmployeeService, EmployeeService>();

            services.AddTransient<IModuleService, ModuleService>();
            services.AddTransient<IRouteControlsService, RouteControlsService>();

            services.AddTransient<ISystemRoleService, SystemRoleService>();
            services.AddTransient<IRoleBasedPermissionService, RoleBasedPermissionService>();
            services.AddTransient<ISystemUserService, SystemUserService>();
            services.AddTransient<IUserBasedPermissionService, UserBasedPermissionService>();

            services.AddTransient<IMainMenuCategoryService, MainMenuCategoryService>();
            services.AddTransient<ISubMenuCategoryService, SubMenuCategoryService>();
            services.AddTransient<IHomeSectionCategoryService, HomeSectionCategoryService>();
            services.AddTransient<IHomeSecMainMenuCatService, HomeSecMainMenuCatService>();
            services.AddTransient<IOutSourceAdvertismentService, OutSourceAdvertismentService>();

            services.AddTransient<IListingPackagesService, ListingPackagesService>();
            services.AddTransient<IPackagePricesService, PackagePricesService>();
            services.AddTransient<IPaymentModesService, PaymentModesService>();

            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ICategoryTypesService, CategoryTypesService>();

            services.AddTransient<IListingCategoryService, ListingCategoryService>();
            services.AddTransient<IListingPremiumService, ListingPremiumService>();
            services.AddTransient<ICompanyListingTimmingService, CompanyListingTimmingService>();
            services.AddTransient<IListingPaymentsModeService, ListingPaymentsModeService>();
            services.AddTransient<IListingSocialMediaService, ListingSocialMediaService>();
            services.AddTransient<IListingServicesService, ListingServicesService>();
            services.AddTransient<IVerifiedListingService, VerifiedListingService>();
            services.AddTransient<IListingLandlineNoService, ListingLandlineNoService>();
            services.AddTransient<IListingMobileNoService, ListingMobileNoService>();
            services.AddTransient<IListingGalleryService, ListingGalleryService>();
            services.AddTransient<IListingAddressService, ListingAddressService>();
            services.AddTransient<ICompanyListingProfileService, CompanyListingProfileService>();
            services.AddTransient<ICompanyListingsService, CompanyListingsService>();
            services.AddTransient<IListingSocialMediaService, ListingSocialMediaService>();
            services.AddTransient<IZonesService, ZonesService>();
            services.AddTransient<IActiveZoneService, ActiveZoneService>();
            services.AddTransient<IBusinessTypesService, BusinessTypesService>();
            services.AddTransient<ICompanyListingRatingService, CompanyListingRatingService>();

            //Dashboard
            services.AddTransient<ICompanyListingCountByUIdService, CompanyListingCountByUIdService>();
            services.AddTransient<ITellerDashboardService, TellerDashboardService>();
            services.AddTransient<IZoneManagerDashboardService, ZoneManagerDashboardService>();
            services.AddTransient<IRegionalManagerDashboardService, RegionalManagerDashboardService>();
            services.AddTransient<ICEODashboardService, CEODashboardService>();
            services.AddTransient<IAdminDashboardService, AdminDashboardService>();

            services.AddTransient<ISpGetGeneralResultantProcService, SpGetGeneralResultantProcService>();
            services.AddTransient<IVLoadHomePopularServiceService, VLoadHomePopularServiceService>();

            services.AddTransient<IHomeListingService, HomeListingService>();


            services.AddScoped<CommonService>();
            services.AddTransient<IService, SendSmsService>();
            services.AddScoped<CustomAuthorizationAttribute>();
            services.AddScoped<ClientAuthorizationAttribute>();

            //Search Behaviour
            services.AddTransient<ISearchBehaviourService, SearchBehaviourService>();
            services.AddTransient<IListingQueryTrackService, ListingQueryTrackService>();

            //Registration Of Logging Services
            services.AddTransient<IErrorLoggingWriter, LoggingErrors>();

            //services.AddMvc(options => {
            //    options.Filters.Add<CustomAuthorizationAttribute>();
            //}).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCloudscribePagination();
            services.AddMemoryCache();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //Registration of Session
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddCors(options => options.AddPolicy("CorsPolicy",
            //builder =>
            //{
            //    builder.AllowAnyMethod().AllowAnyHeader()
            //           .WithOrigins("http://localhost:44385")
            //           .AllowCredentials();
            //}));

            // Auto Jobs

            //services.AddCronJob<AutoDeleteUnVerfiedRatings>(c =>
            //{
            //    c.TimeZoneInfo = TimeZoneInfo.Local;
            //    c.CronExpression = @"30 2 * * *";
            //});

            services.AddCronJob<AutoUpdateExpiredOrders>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.Local;
                c.CronExpression = @"30 3 * * *";
            });
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
                options.KeepAliveInterval = TimeSpan.FromMinutes(1);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseResponseCompression();
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 43200;
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                        "public,max-age=" + durationInSeconds;
                    ctx.Context.Request.Headers[HeaderNames.Expires] =
                        DateTime.UtcNow.AddHours(12).ToString("R");
                }
            });
            app.UseCookiePolicy();
            //app.UseCors("CorsPolicy");
            app.UseAuthentication();







            app.UseSignalR(routes =>
            {
                routes.MapHub<OnlineUsersCount>("/onlineUsersCount", options =>
                {
                    options.Transports = HttpTransportType.ServerSentEvents;
                });
                //routes.MapHub<OnlineUsersCount>("/onlineUsersCount");
            });
            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "Login",
                //    template: "{controller=Account}/{action=Login}/{id?}");
                routes.MapRoute(
                    name: "Home",
                    template: "{controller=Home}/{action=Index}");

                routes.MapRoute(
                    name: "loadsubcategory-route",
                    template: "Product/{Location}/{CatId}/{CatName}",
                    defaults: new { controller = "Home", action = "LoadSubCategory" }
                );

                routes.MapRoute(
                    name: "loadsubchildcategory-route",
                    template: "Products/{Location}/{CatId}/{SubCatId}/{SubCatName}",
                    defaults: new { controller = "Home", action = "LoadSubChildCategory" }
                );
                routes.MapRoute(
                    name: "listingdescription-route",
                    template: "ProductDetail/{Location}/{CityArea}/{ListingId}/{Name}",
                    defaults: new { controller = "Home", action = "ListingDescription" }
                    );
                //routes.MapRoute(
                //    name: "AdminAnalytics",
                //    template: "{controller=AdminDashBoard}/{action=AdminAnalytics}");
                //routes.MapRoute(
                //    name: "SalesAnalytics",
                //    template: "{controller=AdminDashBoard}/{action=Analytics}");
            });
        }
    }
}
