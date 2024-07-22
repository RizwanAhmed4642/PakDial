using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.IdentityManagement;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.RequestModels.CompanyListings;
using PAKDial.Domains.RequestModels.Configuration;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Interfaces.PakDialServices.Configuration;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using PAKDial.Interfaces.PakDialServices.IHomeLandingPageService;
using PAKDial.Interfaces.Repository;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
 
    public class ClientListingController : Controller
    {

        #region Prop

        private readonly ICompanyListingsService _companyListingsService;
        private readonly ICustomerService _customerService;
        private readonly ISubMenuCategoryService _subMenuCategoryService;
        private readonly ICityService _cityService;
        private readonly IPaymentModesService _paymentModesService;
        private readonly ITypeOfServicesService _typeOfServicesService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IListingGalleryService _IListingGalleryService;
        private readonly IBusinessTypesService _IBusinessTypesService;
        private readonly ICityAreaService _ICityAreaService;
        private readonly IEmployeeService _IEmployeeService;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IListingPremiumService _listingPremiumService;


        #endregion

        #region Ctor

        public ClientListingController(ICompanyListingsService companyListingsService,
            ICustomerService customerService, ISubMenuCategoryService subMenuCategoryService,
            ICityService cityService, IPaymentModesService paymentModesService,
            ITypeOfServicesService typeOfServicesService, UserManager<ApplicationUser> userManager,
            IListingGalleryService IListingGalleryService,
            IBusinessTypesService IBusinessTypesService,
            ICityAreaService ICityAreaService,
            IEmployeeService IEmployeeService, IHostingEnvironment hostingEnvironment,
            IListingPremiumService listingPremiumService)
        {
            _companyListingsService = companyListingsService;
            _customerService = customerService;
            _subMenuCategoryService = subMenuCategoryService;
            _cityService = cityService;
            _paymentModesService = paymentModesService;
            _typeOfServicesService = typeOfServicesService;
            _userManager = userManager;
            _IListingGalleryService = IListingGalleryService;
            _IBusinessTypesService = IBusinessTypesService;
            _ICityAreaService = ICityAreaService;
            _IEmployeeService = IEmployeeService;
            this.hostingEnvironment = hostingEnvironment;
            _listingPremiumService = listingPremiumService;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(ClientAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(ClientAuthorizationAttribute))]
        public JsonResult LoadClientCompanyListings()

        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];

            CompanyListingsRequestModel requestModel = new CompanyListingsRequestModel
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false
            };
            requestModel.SortColumnName = sortColumnName;
            requestModel.CustomerId = Convert.ToDecimal(HttpContext.Session.GetString("CustomerId"));
            var results = _companyListingsService.GetClientCompanyListings(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.CompanyListings });


        }

        [HttpGet]
        [ServiceFilter(typeof(ClientAuthorizationAttribute))]
        public IActionResult AddClientCompanyListing()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyListings"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(ClientAuthorizationAttribute))]
        public IActionResult AddCompanyListing(VMAddCompanyListingModel companyListings)
        {
            decimal Result = 0;
            companyListings.CompanyListings.CustomerId =Convert.ToDecimal(HttpContext.Session.GetString("CustomerId"));
            if (companyListings.CustomerRegistration.Email != null && companyListings.CustomerRegistration.Password != null)
            {
                companyListings.Registration.Id = Guid.NewGuid().ToString();
                companyListings.Registration.UserName = companyListings.CustomerRegistration.Email.ToLower();
                companyListings.Registration.NormalizedUserName = companyListings.CustomerRegistration.Email.ToUpper();
                companyListings.Registration.Email = companyListings.CustomerRegistration.Email.ToLower();
                companyListings.Registration.NormalizedEmail = companyListings.CustomerRegistration.Email.ToUpper();
                companyListings.Registration.EmailConfirmed = false;
                companyListings.Registration.PasswordHash = _userManager.PasswordHasher.HashPassword(new ApplicationUser { UserName = companyListings.Registration.UserName, Email = companyListings.Registration.UserName }, companyListings.CustomerRegistration.Password);
                companyListings.Registration.SecurityStamp = Guid.NewGuid().ToString();
                //companyListings.Registration.PhoneNumber = companyListings.CompanyListings.;
                companyListings.Registration.PhoneNumberConfirmed = false;
                companyListings.Registration.LockoutEnabled = false;
                companyListings.Registration.AccessFailedCount = 0;
                companyListings.Registration.CreatedBy = HttpContext.Session.GetString("UserId");
                companyListings.Registration.CreatedDate = DateTime.Now;
                companyListings.Registration.UpdatedBy = HttpContext.Session.GetString("UserId");
                companyListings.Registration.UpdatedDate = DateTime.Now;
                companyListings.Registration.UserTypeId = 2;
                companyListings.CompanyListings.Email = companyListings.CustomerRegistration.Email.ToUpper(); ;
                companyListings.CompanyListings.CreatedBy = HttpContext.Session.GetString("UserId");
                companyListings.CompanyListings.CreatedDate = DateTime.Now;
                companyListings.CompanyListings.UpdatedBy = HttpContext.Session.GetString("UserId");
                companyListings.CompanyListings.UpdatedDate = DateTime.Now;
                Result = _companyListingsService.AddCompanyListing(companyListings);
            }
            else if (companyListings.CompanyListings.CustomerId > 0)
            {
                companyListings.CompanyListings.CreatedBy = HttpContext.Session.GetString("UserId");
                companyListings.CompanyListings.CreatedDate = DateTime.Now;
                companyListings.CompanyListings.UpdatedBy = HttpContext.Session.GetString("UserId");
                companyListings.CompanyListings.UpdatedDate = DateTime.Now;
                Result = _companyListingsService.AddCompanyListing(companyListings);
            }
            //Result = 1;
            return Json(Result);
        }

        /// <summary>
        /// Load PreRequist Table Listing List On Add Operation Using Wrapper Class
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(ClientAuthorizationAttribute))]
        public JsonResult LoadClientCompanyListingList()
        {
            var results = _companyListingsService.GetClientCompanyListingWrapperList();
            return Json(results);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
   
        public JsonResult LoadClientAddSubMenuCategories(string search, string pageNo, string pageSize, decimal CategoryId)
        {
            if (CategoryId > 0)
            {
                SubCategoryRequestModel request = new SubCategoryRequestModel
                {
                    SearchString = search,
                    PageNo = Convert.ToInt32(pageNo),
                    PageSize = Convert.ToInt32(pageSize),
                    MainCatId = CategoryId,
                };
                var results = _subMenuCategoryService.GetSubMenusSearchList(request);
                return Json(results);
            }
            else
                return Json(null);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public JsonResult LoadBussinessType()
        {
            var Result = _IBusinessTypesService.GetAllBusinessTypeKey();
            return Json(Result.ToList());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
       
        public JsonResult LoadPaymentModeList(string search, string pageNo, string pageSize)
        {
            PaymentModesRequestModel request = new PaymentModesRequestModel
            {
                SearchString = search,
                PageNo = Convert.ToInt32(pageNo),
                PageSize = Convert.ToInt32(pageSize),
            };
            var results = _paymentModesService.GetPaymentModesList(request);
            return Json(results);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>

        public JsonResult LoadTypeofServiceList(string search, string pageNo, string pageSize)
        {
            TypeOfServicesRequestModel request = new TypeOfServicesRequestModel
            {
                SearchString = search,
                PageNo = Convert.ToInt32(pageNo),
                PageSize = Convert.ToInt32(pageSize),
            };
            var results = _typeOfServicesService.GetTypeofServicesList(request);
            return Json(results);
        }
        /// <summary>
        /// Get City by state Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetCityListById(decimal Id)
        {
            var CityList = _cityService.GetAllCitiesByStates(Id);
            return Json(CityList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetCityAreaById(decimal Id)
        {
            var lObjCityArea = _ICityAreaService.GetAllAreasByCity(Id);
            return Json(lObjCityArea);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [ServiceFilter(typeof(ClientAuthorizationAttribute))]
        public IActionResult EditCompanyListing(int Id , string Value)
        {
            ViewBag.Id = Id;
            ViewBag.Value = Value;
            return View(new VMAddCompanyListingModel { Id = Id });
        }

        /// <summary>
        /// Edit a company Listing by a Id
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(ClientAuthorizationAttribute))]
        public JsonResult EditCompanyListing(VMAddCompanyListingModel companyListings)
        {
            decimal Result = 0;
            if (companyListings.CompanyListings.Id > 0)
            {
                companyListings .CompanyListings .CustomerId = Convert.ToDecimal(HttpContext.Session.GetString("CustomerId"));
                companyListings.CompanyListings.UpdatedBy = HttpContext.Session.GetString("UserId");
                companyListings.CompanyListings.UpdatedDate = DateTime.Now;
                Result = _companyListingsService.UpdateCompanyListing(companyListings);
            }
            return Json(Result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns> 
        [ServiceFilter(typeof(ClientAuthorizationAttribute))]
        public JsonResult GetCompanyListingsById(decimal Id)
        {
            var lObjCompanyListings = _companyListingsService.FindRecord_2(Id ,Convert.ToDecimal(HttpContext.Session.GetString("CustomerId")));


            return Json(new
            {
                lObjCompanyListings.CompanyListings,
                lObjCompanyListings.CompanyListingProfile,
                lObjCompanyListings.CompanyListingTimming,
                lObjCompanyListings.ListingAddress,
                lObjCompanyListings.ListingCategory,
                lObjCompanyListings.ListingGallery,
                lObjCompanyListings.ListingLandlineNo,
                lObjCompanyListings.ListingMobileNo,
                lObjCompanyListings.ListingPaymentsMode,
                lObjCompanyListings.ListingServices,
                lObjCompanyListings.SocialMediaModes,
                lObjCompanyListings.ListingsBusinessTypes
            });
        }
        [ServiceFilter(typeof(ClientAuthorizationAttribute))]
        public JsonResult CheckCustomerListing(decimal Id)
        {
            string message = "";
            if(Convert.ToDecimal(HttpContext.Session.GetString("CustomerId")) > 0)
            {
                bool check = _companyListingsService.CheckCustomerListing(Convert.ToDecimal(HttpContext.Session.GetString("CustomerId")), Id);
                if(check == true)
                {
                    message = "OK";
                }
                else
                {
                    message = "InValid";
                }
            }
            else
            {
                message = "LoginPlease";

            }
            return Json(message);
        }

        [ServiceFilter(typeof(ClientAuthorizationAttribute))]
        public IActionResult EditCustomer()
        {
            ViewBag.Id= HttpContext.Session.GetString("CustomerId");
            return View();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [ServiceFilter(typeof(ClientAuthorizationAttribute))]
        public JsonResult GetCustomerById(decimal Id)
        {
            var GetCustomer = _customerService.GetCustomerById(Id);
            return Json(GetCustomer.Customers);
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(ClientAuthorizationAttribute))]
        public JsonResult UpdateCustomer(CreateUpdateCustomer customer)
        {
            int Result = 0;
            if (customer != null)
            {
                customer.IsActive = true;
                customer.UpdatedBy = HttpContext.Session.GetString("UserId");
                customer.UpdatedDate = DateTime.Now;
                Result = _customerService.Update(customer);
            }
            return Json(Result);
        }

        public JsonResult CheckMobileExiting(string phone, decimal id)
        {

            return Json(_customerService.CheckExiting(phone, id));
        }
        [HttpPost]
        [DisableRequestSizeLimit]
        [RequestSizeLimit(long.MaxValue)]
        public JsonResult UploadProfileImage(decimal Id)
        {
            int Results = 0;
            var file = Request.Form.Files[0];
            var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
            var Emppath = Path.Combine(new string[]
                 {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","CustomerProfile",Id.ToString()
                 });
            if (Directory.Exists(Emppath))
            {
                Directory.Delete(Emppath, true);
            }
            var path = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","CustomerProfile",Id.ToString(),
                           Guid.NewGuid() + filename
                    });

            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (FileStream fs = System.IO.File.Create(path))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            path = path.Replace(hostingEnvironment.WebRootPath, "").Replace("\\", "/");
            Results = _customerService.ImageUpdate(Id, path);
            return Json(Results);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableRequestSizeLimit]
        [RequestSizeLimit(long.MaxValue)]
        public JsonResult UpdateBannerImage(decimal Id)
        {
            int Results = 0;
            var file = Request.Form.Files[0];
            Results = _companyListingsService.UploadBannerImage(Id, file, HttpContext.Request.Host.Value + "/");
            return Json(Results);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableRequestSizeLimit]
        [RequestSizeLimit(long.MaxValue)]
        public JsonResult UpdateGalleryBannerImage(decimal Id)
        {
            decimal Results = 0;
            var files = Request.Form.Files;
            ListingGallery lObjlistingGallery = new ListingGallery()
            {
                CreatedBy = HttpContext.Session.GetString("UserId"),
                CreatedDate = DateTime.Now,
                UpdatedBy = HttpContext.Session.GetString("UserId"),
                UpdatedDate = DateTime.Now,
                ListingId = Id,
            };
            Results = _companyListingsService.UploadGalleryImage(files, HttpContext.Request.Host.Value + "/", lObjlistingGallery);
            return Json(Results);
        }

        /// <summary>
        /// Delete Gallery Image from DataBase.
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="ImgId"></param>
        /// <returns></returns>
        public JsonResult DeleteGalleryImage(string Url, decimal ImgId)
        {
            return Json(_IListingGalleryService.DeleteGalleryImage(Url, Convert.ToDecimal(ImgId)));
        }

        public IActionResult PaymentMode(decimal Id)
        {
            ViewBag.Id = Id;
            return View(new VMAssignPackageModel { Id = Id });
        }
        [ServiceFilter(typeof(ClientAuthorizationAttribute))]
        public JsonResult LoadAssignedListingPackages(decimal ListingId)
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            AssignListingPackageRequestModel requestModel = new AssignListingPackageRequestModel
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false,
                ListingId = ListingId,
                CustomerId=Convert.ToDecimal( HttpContext.Session.GetString("CustomerId"))
        };
            //requestModel.SortColumnName = sortColumnName;
            var results = _listingPremiumService.Get(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.AssignListingPackages });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public JsonResult LoadAddSubMenuCategories(string search, string pageNo, string pageSize, decimal CategoryId)
        {
            if (CategoryId > 0)
            {
                SubCategoryRequestModel request = new SubCategoryRequestModel
                {
                    SearchString = search,
                    PageNo = Convert.ToInt32(pageNo),
                    PageSize = Convert.ToInt32(pageSize),
                    MainCatId = CategoryId,
                };
                var results = _subMenuCategoryService.GetSubMenusSearchList(request);
                return Json(results);
            }
            else
                return Json(null);

        }
    }
}