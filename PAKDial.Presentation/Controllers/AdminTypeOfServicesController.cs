using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels.Configuration;
using PAKDial.Interfaces.PakDialServices.Configuration;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{

    [Authorize]
    public class AdminTypeOfServicesController : Controller
    {
        #region Prop

        private readonly ITypeOfServicesService _typeOfServicesService;

        #endregion

        #region Ctor

        public AdminTypeOfServicesController(ITypeOfServicesService typeOfServicesService)
        {
            _typeOfServicesService = typeOfServicesService;
        }

        #endregion

        #region ActionMethod

        /// <summary>
        /// Load Index View
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Load Type Of Service From DataBase.
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public JsonResult LoadTypeOfService()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

           TypeOfServicesRequestModel requestModel = new TypeOfServicesRequestModel
           {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false
            };
            //requestModel.SortColumnName = sortColumnName;
            var results = _typeOfServicesService.GetTypeOfService(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.TypeOfServices });
        }

        /// <summary>
        /// Add type Of service in Database
        /// </summary>
        /// <param name="typeOfServices"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult AddTypeOfServices(TypeOfServices typeOfServices)
        {
            decimal Result = 0;

            if (typeOfServices != null)
            {
                typeOfServices.CreatedBy = HttpContext.Session.GetString("UserId");
                typeOfServices.UpdatedBy = HttpContext.Session.GetString("UserId");
                typeOfServices.CreatedDate = DateTime.Now;
                typeOfServices.UpdatedDate = DateTime.Now;
                Result = _typeOfServicesService.Add(typeOfServices);
            }

            return Json(Result);
        }


        /// <summary>
        /// Update Exisiting Record in database
        /// </summary>
        /// <param name="typeOfServices"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult EditTypeOfService(TypeOfServices typeOfServices)
        {

            typeOfServices.UpdatedBy = HttpContext.Session.GetString("UserId");
            typeOfServices.UpdatedDate = DateTime.Now;
            return Json(_typeOfServicesService.Update(typeOfServices));
         
        }

        /// <summary>
        /// Delete Existing record by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteTypeOfServices(int Id)
        {
            return Json(_typeOfServicesService.Delete(Id));
        }

        /// <summary>
        /// get type of service detail by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        
        public JsonResult GetTypeOfServicesById(int Id)
        {
            return Json(_typeOfServicesService.GetById(Id));
        }

        /// <summary>
        /// Get All Record from database
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        private JsonResult GetAllTypesOfServices()
        {
            return Json(_typeOfServicesService.GetAll().ToList());
        }

        #endregion


    }
}