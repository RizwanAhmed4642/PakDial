using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.RequestModels;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    public class AdminStatusController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public AdminStatusController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public JsonResult LoadEmployees()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];

            EmployeeRequestModel requestModel = new EmployeeRequestModel
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false
            };

            var results = _employeeService.GetSpecificEmployees(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.Employees });
        }
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult EmpReport(string LocalId)
        {
            return View(_employeeService.EmployeeReportings(LocalId));
        }

    }
}