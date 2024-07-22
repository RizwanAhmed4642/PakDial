using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.IdentityManagement;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Interfaces.PakDialServices.IHomeLandingPageService;
using PAKDial.Presentation.Filters;
using PAKDial.Repository.BaseRepository;
using PAKDial.Repository.IdentityContext;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminEmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly ICountryService countryService;
        private readonly IDesignationService designationService;
        private readonly ISystemRoleService systemRoleService;
        private readonly ISystemUserService systemUserService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext db;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ICityService _cityService;
        private readonly IMainMenuCategoryService mainMenuCategoryService;
        private readonly IZonesService zonesService;


        public AdminEmployeeController(IEmployeeService employeeService, ICountryService countryService
            , IDesignationService designationService, ISystemRoleService systemRoleService
            , ISystemUserService systemUserService, UserManager<ApplicationUser> _userManager,
            RoleManager<ApplicationRole> _roleManager,
            ApplicationDbContext db,
            IHostingEnvironment hostingEnvironment, ICityService cityService
            , IMainMenuCategoryService mainMenuCategoryService
            , IZonesService zonesService)
        {
            this.employeeService = employeeService;
            this.countryService = countryService;
            this.designationService = designationService;
            this.systemRoleService = systemRoleService;
            this.systemUserService = systemUserService;
            this._userManager = _userManager;
            this._roleManager = _roleManager;
            this.db = db;
            _cityService = cityService;
            this.hostingEnvironment = hostingEnvironment;
            this.mainMenuCategoryService = mainMenuCategoryService;
            this.zonesService = zonesService;
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
            //string sortDirection = Request.Form["order[0][dir]"];

            EmployeeRequestModel requestModel = new EmployeeRequestModel
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false
            };
            //requestModel.SortColumnName = sortColumnName;
            var results = employeeService.GetEmployees(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.Employees });
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetCuntryRoleDesig()
        {
            var Country = countryService.GetAll();
            var Designation = designationService.GetAll();
            var Roles = systemRoleService.GetAll();

            return Json(new { Country, Designation, Roles });
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AddEmployee(CreateUpdateEmployee employee)
        {
            decimal Result = 0;
            var Cnic = employeeService.FindByCnic(employee.Cnic, 0);
            var Email = systemUserService.FindByEmail(employee.Email);
            employee.CreatedBy = HttpContext.Session.GetString("UserId");
            employee.CreatedDate = DateTime.Now;
            employee.UpdatedBy = HttpContext.Session.GetString("UserId");
            employee.UpdatedDate = DateTime.Now;
            if (Cnic == null && Email == null)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //UsertypeId 1 means Employee
                        var user = new ApplicationUser { UserName = employee.Email, Email = employee.Email, CreatedBy = employee.CreatedBy, CreatedDate = DateTime.Now, UpdatedBy = employee.UpdatedBy, UpdatedDate = DateTime.Now, UserTypeId = 1,EmailConfirmed = true };
                        var result = await _userManager.CreateAsync(user, employee.Password);
                        if (result.Succeeded)
                        {
                            IdentityRole res = await _roleManager.FindByIdAsync(employee.RoleId);
                            var userResult = await _userManager.AddToRoleAsync(user, res.Name);
                            if (userResult.Succeeded)
                            {
                                var UserId = _userManager.Users.Where(c => c.Email == employee.Email).Select(c => c.Id).FirstOrDefault();
                                var designations = db.Designation.Where(c => c.Name.Trim().ToLower() == res.Name.Trim().ToLower()).FirstOrDefault();
                                Employee instance = new Employee
                                {
                                    FirstName = employee.FirstName,
                                    LastName = employee.LastName,
                                    Cnic = employee.Cnic,
                                    CreatedBy = employee.CreatedBy,
                                    CreatedDate = employee.CreatedDate,
                                    UpdatedBy = employee.UpdatedBy,
                                    UpdatedDate = employee.UpdatedDate,
                                    DateOfBirth = employee.DateOfBirth,
                                    DesignationId = designations.Id,
                                    PassportNo = employee.PassportNo,
                                    UserId = UserId,
                                    IsActive = true,
                                };
                                db.Employee.Add(instance);
                                db.SaveChanges();
                                db.EmployeeAddress.Add(new EmployeeAddress
                                {
                                    EmployeeId = instance.Id,
                                    EmpAddress = employee.EmpAddress,
                                    CityAreaId = employee.CityAreaId,
                                    ProvinceId = employee.ProvinceId,
                                    CountryId = employee.CountryId,
                                    CityId = employee.CityId,
                                    CreatedBy = employee.CreatedBy,
                                    CreatedDate = employee.CreatedDate,
                                    UpdatedBy = employee.UpdatedBy,
                                    UpdatedDate = employee.UpdatedDate
                                });
                                db.EmployeeContact.Add(new EmployeeContact
                                {
                                    EmployeeId = instance.Id,
                                    ContactNo = employee.ContactNo,
                                    PhoneNo = employee.PhoneNo,
                                    CreatedBy = employee.CreatedBy,
                                    CreatedDate = employee.CreatedDate,
                                    UpdatedBy = employee.UpdatedBy,
                                    UpdatedDate = employee.UpdatedDate
                                });
                                db.SaveChanges();
                                transaction.Commit();
                                Result = instance.Id;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Result = 0;
                    }
                }
            }
            else
            {
                Result = -2;
            }
            return Json(Result);
        }

        public JsonResult GetEmployeeById(decimal Id)
        {
            var GetAllEmployees = employeeService.GetEmployeeDetails(Id);
            return Json(new
            {
                GetAllEmployees.Employees,
                GetAllEmployees.Addresses,
                GetAllEmployees.Contacts,
                GetAllEmployees.States,
                GetAllEmployees.Cities,
                GetAllEmployees.CityAreas
            });
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateEmployee(CreateUpdateEmployee employee)
        {
            int Result = 0;
            if (employee != null)
            {
                employee.UpdatedBy = HttpContext.Session.GetString("UserId");
                employee.UpdatedDate = DateTime.Now;
                Result = employeeService.Update(employee);
            }
            return Json(Result);
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
                           "SystemImages","EmployeeProfile",Id.ToString()
                 });
            if (Directory.Exists(Emppath))
            {
                Directory.Delete(Emppath, true);
            }
            var path = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","EmployeeProfile",Id.ToString(),
                           Guid.NewGuid() + filename
                    });

            Directory.CreateDirectory(Path.GetDirectoryName(path));
            ImageCodecInfo codecInfo = ImageCodecInfo.GetImageEncoders()
               .Where(r => r.CodecName.ToUpperInvariant().Contains("JPEG") || r.CodecName.ToUpperInvariant().Contains("PNG"))
               .Select(r => r).FirstOrDefault();
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            var encoder = Encoder.Quality;
            var parameters = new EncoderParameters(1);
            var parameter = new EncoderParameter(encoder, 40L);
            parameters.Param[0] = parameter;
            using (FileStream fs = System.IO.File.Create(path))
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyToAsync(memoryStream);
                    using (var img = Image.FromStream(memoryStream))
                    {
                        img.Save(fs, codecInfo, parameters);

                        fs.Flush();
                    }
                }
                //file.CopyTo(fs);
                //fs.Flush();
            }
            path = path.Replace(hostingEnvironment.WebRootPath, "").Replace("\\", "/");
            Results = employeeService.ImageUpdate(Id, path);
            return Json(Results);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public async Task<JsonResult> ChangesPassword(string UserId, string newPassword)
        {
            int Result = 0;
            ApplicationUser user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return Json(NotFound());
            }
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                Result = 1;
            }
            return Json(Result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="StateId"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult UpdateRegionalManager(decimal Id)
        {
            return Json(employeeService.GetRegionalManager(Id));
        }
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult AddUpdateRegionalManager(VMAddUpdateRegionalManager response)
        {
            response.UpdatedBy = HttpContext.Session.GetString("UserId");
            response.UpdatedDate = DateTime.Now;
            return Json(employeeService.AddUpdateRegionalManager(response));
        }
        [ValidateAntiForgeryToken]
        public JsonResult LoadCategoryList(string search, string pageNo, string pageSize)
        {
            MainMenuCategoryRequestModel request = new MainMenuCategoryRequestModel
            {
                SearchString = search,
                PageNo = Convert.ToInt32(pageNo),
                PageSize = Convert.ToInt32(pageSize),
            };
            var results = mainMenuCategoryService.GetMainMenuSearchList(request);
            return Json(results);
        }
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult UpdateCategoryManager(decimal Id)
        {
            return Json(employeeService.GetCategoryManager(Id));
        }
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult AddUpdateCategoryManager(VMAddUpdateCategoryManager response)
        {
            response.UpdatedBy = HttpContext.Session.GetString("UserId");
            response.UpdatedDate = DateTime.Now;
            return Json(employeeService.AddUpdateCategoryManager(response));
        }
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult UpdateZoneManager(decimal Id)
        {
            return Json(employeeService.GetZoneManager(Id));
        }
        [ValidateAntiForgeryToken]
        public JsonResult GetAssignedCities(decimal ManagerId)
        {
            return Json(_cityService.GetAssignedCity(ManagerId));
        }
        [ValidateAntiForgeryToken]
        public JsonResult LoadZoneList(string search, string pageNo, string pageSize ,decimal CityId)
        {
            ZonesRequestModel request = new ZonesRequestModel
            {
                SearchString = search,
                PageNo = Convert.ToInt32(pageNo),
                PageSize = Convert.ToInt32(pageSize),
                CityId = CityId,
            };
            var results = zonesService.GetSearchZones(request);
            return Json(results);
        }
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult AddUpdateZoneManager(VMAddUpdateZoneManager response)
        {
            response.UpdatedBy = HttpContext.Session.GetString("UserId");
            response.UpdatedDate = DateTime.Now;
            return Json(employeeService.AddUpdateZoneManager(response));
        }
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult UpdateOtherManager(decimal Id)
        {
            return Json(employeeService.GetOtherManager(Id));
        }
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult AddUpdateOtherManager(decimal EmployeeId,decimal ManagerId)
        {
            return Json(employeeService.AddUpdateOtherManager(EmployeeId,ManagerId, HttpContext.Session.GetString("UserId"),DateTime.Now));
        }

    }
}