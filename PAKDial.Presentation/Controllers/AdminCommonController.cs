using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminCommonController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly ISystemUserService systemUserService;
        private readonly ICompanyListingsService _ICompanyListingsService;

        public AdminCommonController(IEmployeeService employeeService, ISystemUserService systemUserService,
            ICompanyListingsService ICompanyListingsService)
        {
            this.employeeService = employeeService;
            this.systemUserService = systemUserService;
            _ICompanyListingsService =ICompanyListingsService;
        }

        public JsonResult CheckCNICExits(string Cnic, decimal? EmpId)
        {
            var IsExits = employeeService.FindByCnic(Cnic, EmpId) == null ? 0 : 1;
            return Json(IsExits);
        }

        public JsonResult CheckEmailExits(string Email)
        {
            var IsExits = systemUserService.FindByEmail(Email) == null ? 0 : 1;
            return Json(IsExits);
        }

        public JsonResult CheckMobileNoExits(string MobileNo, int Id)
        {
            var IsExits = 0;
           
                IsExits = _ICompanyListingsService.CheckMobileNo(MobileNo, Id).Count >= 1 ? 0 : 1;           

            return Json(IsExits);
        }
        public JsonResult CheckLandineNoExits(string LandLineNo, int Id)
        {
            var IsExits = 0;
           
                IsExits = _ICompanyListingsService.CheckLandlineNo(LandLineNo, Id).Count >= 1 ? 0 : 1;
            
            return Json(IsExits);
        }


    }
}