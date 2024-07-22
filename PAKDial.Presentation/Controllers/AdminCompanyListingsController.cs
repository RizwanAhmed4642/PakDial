using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.IdentityManagement;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.RequestModels.CompanyListings;
using PAKDial.Domains.RequestModels.Configuration;
using PAKDial.Domains.ResponseModels;
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
    [Authorize]
    public class AdminCompanyListingsController : Controller
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


        #endregion

        #region Ctor

        public AdminCompanyListingsController(ICompanyListingsService companyListingsService
            , ICustomerService customerService, ISubMenuCategoryService subMenuCategoryService
            , ICityService cityService, IPaymentModesService paymentModesService,
            ITypeOfServicesService typeOfServicesService, UserManager<ApplicationUser> userManager,
            IListingGalleryService IListingGalleryService,
            IBusinessTypesService IBusinessTypesService,
            ICityAreaService ICityAreaService,
            IEmployeeService IEmployeeService)
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
        }

        #endregion

        #region Action Method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public  JsonResult LoadCompanyListings()
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
            requestModel.UserId = HttpContext.Session.GetString("UserId");
            var results = _companyListingsService.GetCompanyListings(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.CompanyListings });


        }

        [HttpGet]
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult AddCompanyListing()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult EditCompanyListing(int Id)
        {

            return View(new VMAddCompanyListingModel { Id = Id });
        }

        /// <summary>
        /// Add single record
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public IActionResult AddCompanyListing(VMAddCompanyListingModel companyListings)
        {

            decimal Result = 0;
            companyListings.CompanyListings.ListingTypeId = 1;
            if (companyListings.CustomerRegistration.Email != null && companyListings.CustomerRegistration.Password != null)
            {
                companyListings.Registration.Id = Guid.NewGuid().ToString();
                companyListings.Registration.UserName = companyListings.CustomerRegistration.Email.ToLower();
                companyListings.Registration.NormalizedUserName = companyListings.CustomerRegistration.Email.ToUpper();
                companyListings.Registration.Email = companyListings.CustomerRegistration.Email.ToLower();
                companyListings.Registration.NormalizedEmail = companyListings.CustomerRegistration.Email.ToUpper();
                companyListings.Registration.EmailConfirmed = true;
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
                companyListings.CompanyListings.Email= companyListings.CustomerRegistration.Email;
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
        /// Edit a company Listing by a Id
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult EditCompanyListing(VMAddCompanyListingModel companyListings)
        {
            decimal Result = 0;
            if(companyListings.CompanyListings.Id > 0)
            {
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
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult GetCompanyListingsById(decimal Id)
        {
            var lObjCompanyListings = _companyListingsService.FindRecord_2(Id);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteCompanyListing(decimal Id)
        {
            decimal Result = 0;
            Result = _companyListingsService.DeleteCompanyListings(Id);
            return Json(Result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult DeleteBannerImage(decimal Id)
        {
            decimal Result = 0;
            Result = _companyListingsService.DeleteBannerImage(Id);
            return Json(Result);
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
                ListingId =Id,
            };
            Results = _companyListingsService.UploadGalleryImage( files, HttpContext.Request.Host.Value + "/", lObjlistingGallery);
            return Json(Results);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="IsDefault"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public JsonResult LoadCustomersList(string search, string pageNo, string pageSize, bool IsDefault)
        {
            GetCustomerListRequestModel requestModel = new GetCustomerListRequestModel
            {
                PageNo = Convert.ToInt32(pageNo),
                PageSize = Convert.ToInt32(pageSize),
                SearchString = search,
                IsDefault = IsDefault,
            };
            var results = _customerService.GetCustomerList(requestModel);
            return Json(results);
        }
        /// <summary>
        /// Load PreRequist Table Listing List On Add Operation Using Wrapper Class
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public JsonResult LoadAddCompanyListingList()
        {
            var results = _companyListingsService.GetCompanyListingWrapperList(HttpContext.Session.GetString("UserId"));
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
        [ValidateAntiForgeryToken]
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

   /// <summary>
   /// 
   /// </summary>
   /// <param name="search"></param>
   /// <param name="pageNo"></param>
   /// <param name="pageSize"></param>
   /// <param name="StateId"></param>
   /// <returns></returns>
        public JsonResult LoadCityList(string search, string pageNo, string pageSize, decimal StateId)
        {
            if (StateId > 0)
            {
                CityRequestModel request = new CityRequestModel
                {
                    SearchString = search,
                    PageNo = Convert.ToInt32(pageNo),
                    PageSize = Convert.ToInt32(pageSize),
                    StateId = StateId,
                };
                var results = _cityService.GetCitySearchList(request);
                return Json(results);
            }
            else
                return Json(null);
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

        public JsonResult GetCityAreaById(decimal Id)
        {
            var lObjCityArea = _ICityAreaService.GetAllAreasByCity(Id);
            return Json(lObjCityArea);
        }

        public JsonResult GetCityAreaByCityId(string search, string pageNo, string pageSize, string CityId)
        {

           if (!string.IsNullOrEmpty(CityId))
            {
                CityAreaRequestModel request = new CityAreaRequestModel
                {
                    SearchString = search,
                    PageNo = Convert.ToInt32(pageNo),
                    PageSize = Convert.ToInt32(pageSize),
                    CityId = Convert.ToInt32(CityId)
                    
                };
                var lObjCityArea = _ICityAreaService.GetAllAreasByCity(request);
            return Json(lObjCityArea);
            }
            else { return Json(null); }
           


           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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
        /// 
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public JsonResult LoadBussinessType()
        {
          var Result = _IBusinessTypesService.GetAllBusinessTypeKey();
          return Json(Result.ToList());
        }
        /// <summary>
        /// Transfer of OwnerShip Form Default to Customer Get Method
        /// Load All Customer Except Default Customer on Bases of Listing Id 
        /// For Handling Get Request
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="ListingId"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult GetOwnerShip(decimal ListingId)
        {
            return Json(_companyListingsService.FindById(ListingId));
        }

        /// <summary>
        /// Transfer of OwnerShip Form Default to Customer Post Method
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="ListingId"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult TransferCustomerOwnerShip(decimal CustomerId, decimal ListingId)
        {
            return Json(_companyListingsService.TransferCustomerOwnerShip(CustomerId, ListingId));
        }

        /// <summary>
        /// Verify and UnVerify Listing
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="name"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="CreatedDate"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult VerifyUnVerifyListing(decimal Id, string name)
        {
            return Json(_companyListingsService.VerifyUnVerifyListing(Id, name, HttpContext.Session.GetString("UserId"), DateTime.Now));
        }

        /// <summary>
        /// Delete Gallery Image from DataBase.
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="ImgId"></param>
        /// <returns></returns>
        public JsonResult DeleteGalleryImage(string Url, decimal ImgId)
        {
            return Json(_IListingGalleryService.DeleteGalleryImage(Url,Convert.ToDecimal(ImgId)));
        }
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult ActiveInActiveList(decimal Id, string Status)
        {
            return Json(_companyListingsService.ActiveInActiveListUpdate(Id,Status, HttpContext.Session.GetString("UserId")));
        }
        #endregion

    }
}