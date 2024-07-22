using System;
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
using PAKDial.Presentation.Filters;
using PAKDial.Repository.IdentityContext;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminCustomerController : Controller
    {
        private readonly ICustomerService customerService;
        private readonly ISystemUserService systemUserService;
        private readonly ApplicationDbContext db;
        private readonly ISystemRoleService systemRoleService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IHostingEnvironment hostingEnvironment;

        public AdminCustomerController(ICustomerService customerService, ApplicationDbContext db,
            IHostingEnvironment hostingEnvironment, UserManager<ApplicationUser> _userManager,
            RoleManager<ApplicationRole> _roleManager, ISystemRoleService systemRoleService
            , ISystemUserService systemUserService)
        {
            this.customerService = customerService;
            this.db = db;
            this.systemRoleService = systemRoleService;
            this.systemUserService = systemUserService;
            this._userManager = _userManager;
            this._roleManager = _roleManager;
            this.hostingEnvironment = hostingEnvironment;
        }

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
        public JsonResult LoadCustomer()

        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            CustomerRequestModel requestModel = new CustomerRequestModel
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false
            };
            //requestModel.SortColumnName = sortColumnName;
            var results = customerService.GetCustomer(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.Customers });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]

        public async Task<JsonResult> AddCustomer(CreateUpdateCustomer Customer)
        {
            var Cnic = customerService.FindByCnic(Customer.Cnic, 0);
            var Email = systemUserService.FindByEmail(Customer.Email);

            Customer.CreatedBy = HttpContext.Session.GetString("UserId");
            Customer.CreatedDate = DateTime.Now;
            Customer.UpdatedBy = HttpContext.Session.GetString("UserId");
            Customer.UpdatedDate = DateTime.Now;
            decimal Result = 0;
            if (Cnic == null && Email == null)
            {
                using (var Transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var user = new ApplicationUser { UserName = Customer.Email, Email = Customer.Email, CreatedBy = Customer.CreatedBy, UpdatedBy = Customer.UpdatedBy, CreatedDate = Customer.CreatedDate, UpdatedDate = Customer.UpdatedDate, UserTypeId = 2,EmailConfirmed= true };
                        var result = await _userManager.CreateAsync(user, Customer.Password);
                        if (result.Succeeded)
                        {
                            IdentityRole res = await _roleManager.FindByIdAsync(Customer.RoleId);
                            var userResult = await _userManager.AddToRoleAsync(user, res.Name);
                            if (userResult.Succeeded)
                            {
                                var UserId = _userManager.Users.Where(c => c.Email == Customer.Email).Select(c => c.Id).FirstOrDefault();
                                Customers lObjCustomer = new Customers
                                {
                                    Id = Customer.Id,
                                    Cnic = Customer.Cnic,
                                    FirstName = Customer.FirstName,
                                    LastName = Customer.LastName,
                                    ImagePath = Customer.ImagePath,
                                    DateOfBirth = Customer.DateOfBirth,
                                    PhoneNumber = Customer.PhoneNumber,
                                    CreatedBy = Customer.CreatedBy,
                                    CreatedDate = Customer.CreatedDate,
                                    UpdatedBy = Customer.UpdatedBy,
                                    UpdatedDate = Customer.UpdatedDate,
                                    UserId = UserId,
                                    IsActive = true,
                                };
                                db.Customers.Add(lObjCustomer);
                                db.SaveChanges();
                                Transaction.Commit();
                                Result = lObjCustomer.Id;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Transaction.Rollback();
                        Result = -2;
                        throw;
                    }
                }

            }
            else
            {

                Result = -2;
            }

            return Json(Result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public JsonResult GetCustomerById(decimal Id)
        {
            var GetCustomer = customerService.GetCustomerById(Id);
            return Json(new
            {
                GetCustomer.Customers
            });
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateCustomer(CreateUpdateCustomer customer)
        {
            int Result = 0;
           
            if (customer != null)
            {
                customer.UpdatedBy = HttpContext.Session.GetString("UserId");
                customer.UpdatedDate = DateTime.Now;
                Result = customerService.Update(customer);
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
            Results = customerService.ImageUpdate(Id, path);
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

        [ValidateAntiForgeryToken]
        public JsonResult GetRole()
        {
            var Roles = systemRoleService.GetAll().Where(c => c.Name.ToLower() == "guest");
            return Json(Roles);
        }

        [ValidateAntiForgeryToken]
        public JsonResult CheckMobileExiting(string phone ,decimal id)
        {
            
            return Json(customerService.CheckExiting(phone,id));
        }
    }
}
