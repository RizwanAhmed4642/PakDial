using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Interfaces.PakDialServices.Dashboard;
using PAKDial.Interfaces.PakDialServices.Dashboard.Admin;
using PAKDial.Interfaces.PakDialServices.Dashboard.CEO;
using PAKDial.Interfaces.PakDialServices.Dashboard.Others;
using PAKDial.Interfaces.PakDialServices.Dashboard.RegionalManger;
using PAKDial.Interfaces.PakDialServices.Dashboard.ZoneManager;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminDashBoardController : Controller
    {
        #region Member
        private readonly ICompanyListingCountByUIdService _ICompanyListingCountByUIdService;
        private readonly ITellerDashboardService _ITellerDashboardService;
        private readonly IZoneManagerDashboardService _IZoneManagerDashboardService;
        private readonly IRegionalManagerDashboardService _IRegionalManagerDashboardService;
        private readonly ICEODashboardService _ICEODashboardService;
        private readonly IAdminDashboardService _IAdminDashboardService;
        private readonly ISpGetGeneralResultantProcService _ISpGetGeneralResultantProcService;
        #endregion

        #region Ctor
        public AdminDashBoardController(ICompanyListingCountByUIdService ICompanyListingCountByUIdService, ITellerDashboardService ITellerDashboardService
            , IZoneManagerDashboardService IZoneManagerDashboardService, IRegionalManagerDashboardService IRegionalManagerDashboardService, ICEODashboardService ICEODashboardService,
            IAdminDashboardService IAdminDashboardService, ISpGetGeneralResultantProcService ISpGetGeneralResultantProcService)
        {
            _ICompanyListingCountByUIdService = ICompanyListingCountByUIdService;
            _ITellerDashboardService = ITellerDashboardService;
            _IZoneManagerDashboardService = IZoneManagerDashboardService;
            _IRegionalManagerDashboardService =IRegionalManagerDashboardService;
            _ICEODashboardService =ICEODashboardService;
            _IAdminDashboardService = IAdminDashboardService;
            _ISpGetGeneralResultantProcService = ISpGetGeneralResultantProcService;
        }
        #endregion

        #region ActionMethod

        /// <summary>
        /// Get AnaLytic View
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Analytics()
        {
            return View();
        }

        //[ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public JsonResult LodESCCompanyListings()
        {
            
            return Json(_ICompanyListingCountByUIdService.getESCCompanyListingCountByUId(HttpContext.Session.GetString("UserId")));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
       [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Teller()
        {
            return View();
        }
        /// <summary>
        /// Load Teller Dashboard
        /// </summary>
        /// <returns></returns>
        public JsonResult LoadTellerDashboard()
        {
            return Json(_ITellerDashboardService.SpGetTellerDepositedOrderUId(HttpContext.Session.GetString("UserId")));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult ZoneManagerAnalytics()
        {
            return View();
        }

        /// <summary>
        /// Load Teller Dashboard
        /// </summary>
        /// <returns></returns>
        public JsonResult LoadZoneManagerDashboard()
        {
            return Json(_IZoneManagerDashboardService.SpGetZoneMgrByAreaWrapper(HttpContext.Session.GetString("UserId")));
        }

        /// <summary>
        ///    Regional Dashboard Load
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult RegionalManagerAnalytic()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult LoadRegionalManagerDashboard()
        {
            return Json(_IRegionalManagerDashboardService.SpGetRegionalMgrByAreaWrapper(HttpContext.Session.GetString("UserId")));
        }

        /// <summary>
        ///    Regional Dashboard Load
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult CEOAnalytic()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult LoadCEODashboard()
        {
            return Json(_ICEODashboardService.SpGetCEROWrapperProc());
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult AdminAnalytics()
        {
            return View();
        }

         public JsonResult LoadAdminDshboard()
        {
            return Json(_IAdminDashboardService.SpGetAdminWrapperProc());
        }
        #endregion

        #region Others
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult GeneralAnalytics()
        {
            return View();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult GeneralAnalyticsLoad()
        {
            return Json(_ISpGetGeneralResultantProcService.SpGetGeneralResultantProc());

        }

        #endregion

    }
}