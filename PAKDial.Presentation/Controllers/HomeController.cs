using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using cloudscribe.Pagination.Models;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PAKDial.Common;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.IdentityManagement;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Interfaces.PakDialServices.Home;
using PAKDial.Presentation.Extensions;
using PAKDial.Presentation.Models;
using PAKDial.Presentation.Models.AccountViewModels;
using PAKDial.Presentation.Services;
using PAKDial.ZongServices.SmsService;
using System.Security.Claims;

namespace PAKDial.Presentation.Controllers
{

    public class HomeController : Controller
    {
        private readonly IHomeListingService _homeListing;
        private readonly ICustomerService _customerService;
        private readonly ISystemUserService _systemUserService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISystemRoleService _systemRoleService;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly IService service;

        /// <summary>
        /// 
        /// </summary>
        public HomeController(IHomeListingService homeListing, ICustomerService customerService
            , UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager, ISystemRoleService systemRoleService,
            IEmailSender emailSender, ILogger<AccountController> logger, ISystemUserService systemUserService
            , IService service)
        {
            _homeListing = homeListing;
            _customerService = customerService;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _systemRoleService = systemRoleService;
            _emailSender = emailSender;
            _logger = logger;
            _systemUserService = systemUserService;
            this.service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //HttpContext.Session.SetString("Loc", "0");
            HttpContext.Session.SetString("Gen", "0");
            return View();
        }

        public IActionResult Info()
        {
            //HttpContext.Session.SetString("Loc", "0");
            HttpContext.Session.SetString("Gen", "0");
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Location"></param>
        /// <param name="CatId"></param>
        /// <param name="CatName"></param>
        /// <returns></returns>
        public IActionResult LoadSubCategory(string Location, decimal CatId, string CatName)
        {
            //HttpContext.Session.SetString("Loc", "0");
            HttpContext.Session.SetString("Gen", "0");
            GenericLoadSbCategoryModel model = new GenericLoadSbCategoryModel
            {
                Location = Location,
                CatId = CatId,
                CatName = CatName
            };
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Location"></param>
        /// <param name="CatId"></param>
        /// <param name="SubCatId"></param>
        /// <param name="SubCatName"></param>
        /// <returns></returns>
        public IActionResult LoadSubChildCategory(string Location, decimal CatId, decimal SubCatId, string SubCatName)
        {
            //HttpContext.Session.SetString("Loc", "0");
            HttpContext.Session.SetString("Gen", "0");
            GenericLoadSbCategoryModel model = new GenericLoadSbCategoryModel
            {
                Location = Location,
                CatId = CatId,
                SubCatId = SubCatId,
                SubCatName = SubCatName
            };
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CtName"></param>
        /// <param name="SbCId"></param>
        /// <param name="SbCName"></param>
        /// <param name="SortFilter"></param>
        /// <param name="ArName"></param>
        /// <param name="PageNo"></param>
        /// <returns></returns>
        public IActionResult LoadCategoryDescription(string CtName, decimal SbCId, string SbCName, string SortFilter, string Ratingstatus, string ArName, int PageNo = 1)
        {
            SbCName = SbCName.Replace("***", "&");
            //if (HttpContext.Session.GetString("Loc") != null)
            //{
            //    var Loc = HttpContext.Session.GetString("Loc");
            //    HttpContext.Session.SetString("Loc", Loc);
            //}
            //else
            //{
            //    HttpContext.Session.SetString("Loc", "0");
            //}
            if (HttpContext.Session.GetString("Gen") != null)
            {
                var Gen = HttpContext.Session.GetString("Gen");
                HttpContext.Session.SetString("Gen", Gen);
            }
            else
            {
                HttpContext.Session.SetString("Gen", "0");
            }
            HomeListingRequest request = new HomeListingRequest
            {
                CtName = CtName,
                SbCId = SbCId,
                SbCName = SbCName.ToString(),
                ArName = ArName,
                PageNo = PageNo,
                SortColumnName = SortFilter,
                Ratingstatus = Ratingstatus
            };
            var responseModel = _homeListing.GetListingSearchLastNode(request);
            ViewBag.CtName = responseModel.CtName;
            ViewBag.CatId = responseModel.CatId;
            ViewBag.SbCId = responseModel.SbCId;
            ViewBag.SbCName = responseModel.SbCName;
            ViewBag.SbCNameWithSpace = responseModel.SbCName.Replace("_", " ");
            ViewBag.ArName = responseModel.ArName;
            ViewBag.SortFilter = SortFilter;
            ViewBag.Ratingstatus = Ratingstatus == "Asc" ? "Desc" : "Asc";
            var response = new PagedResult<VHomeListingSearch>()
            {
                Data = responseModel.HomeListingSearch,
                PageNumber = responseModel.PageNo,
                PageSize = responseModel.PageSize,
                TotalItems = responseModel.RowCount,
            };
            return View(response);
        }
        //[HttpPost]
        //public void SetLocTempData(string Loc)
        //{
        //    HttpContext.Session.SetString("Loc", Loc);
        //}
        [HttpPost]
        public void SetGenTempData(string Gen)
        {
            HttpContext.Session.SetString("Gen", Gen);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="CityArea"></param>
        /// <param name="ListingId"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public IActionResult ListingDescription(string location, string CityArea, decimal ListingId, string Name)
        {
            ViewBag.location = location;
            ViewBag.ListingId = ListingId;
            ViewBag.CityArea = CityArea;
            ViewBag.Name = Name;
            if (ListingId > 0)
            {
                ViewBag.CompanyName = _homeListing.RequestCounters(ListingId);
            }
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SbCatName"></param>
        /// <returns></returns>
        public JsonResult GetAllSbCategory(string SbCatName, string Location)
        {
            return Json(_homeListing.SearchFrontEndSubCategory(SbCatName, Location));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ListingId"></param>
        /// <param name="ShowMore"></param>
        /// <returns></returns>
        public JsonResult GetMoreRatings(decimal ListingId, int ShowMore)
        {
            return Json(_homeListing.FindByListingIdFront(ListingId, ShowMore));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public JsonResult CompanyPostRating(CompanyListingRating instance)
        {
            instance.CreatedDate = DateTime.Now;
            instance.UpdatedDate = DateTime.Now;
            instance.IsApproved = false;
            instance.IsVerified = false;
            instance.TotalAttempts = 0;
            HttpContext.Session.SetInt32("RatingOptsAttempts", 0);
            var Results = _homeListing.CompanyPostRating(instance);
            if (Results.Id > 0)
            {
                var dd = CommonNoGen.GenerateFourDigitRandomNo();
                service.SendMessageAsync("92" + instance.MobileNo, "Dear User,  Your verification code;" + dd + ".Please do not reveal this code to anyone");

                HttpContext.Session.SetInt32("RatingOpts", dd);
            }
            return Json(Results);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public JsonResult PostRatingOtp(CompanyRatingsOTP instance)
        {
            AddListingRatingWrapperModel model = new AddListingRatingWrapperModel();
            if (Convert.ToInt32(HttpContext.Session.GetInt32("RatingOptsAttempts")) < 2)
            {
                if (Convert.ToInt32(HttpContext.Session.GetInt32("RatingOpts")) == instance.Otp)
                {
                    model = _homeListing.PostRatingOtp(instance);
                }
                else
                {
                    model.Id = 2;
                    model.Message = "Invalid Opt Code";
                    var Attempts = Convert.ToInt32(HttpContext.Session.GetInt32("RatingOptsAttempts"));
                    if (Attempts < 2)
                    {
                        Attempts++;
                        HttpContext.Session.SetInt32("RatingOptsAttempts", Attempts);
                    }
                }
            }
            else
            {
                instance.Delete = true;
                model = _homeListing.PostRatingOtp(instance);
            }
            return Json(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult InternalServerError()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetBulkQueryFormSubmittion(ListingQueryRequest request)
        {
            ListingQueryRequest listingQuery = new ListingQueryRequest()
            {
                RequestName = request.RequestName,
                RequestMobNo = request.RequestMobNo,
                SubCatId = request.SubCatId,
                AreaName = request.AreaName,
                ListingId = request.ListingId,
                CityName = request.CityName,
                ProductName = request.ProductName,
                SubCatName = request.SubCatName,
            };
            var count = _homeListing.GetBulkQueryFormSubmittion(listingQuery);
            if (count.Count > 0)
            {

                var Singleobj = count.FirstOrDefault();
                foreach (var item in count)
                {
                    Singleobj.RequestMessage = Singleobj.RequestMessage + System.Environment.NewLine +
                        item.CompanyName + ":-" + item.ReceiverNo;
                }
                await service.SendMessageAsync("92" + Singleobj.RequestNo, Singleobj.RequestSubject + "," + System.Environment.NewLine + Singleobj.RequestMessage);
                foreach (var item in count)
                {
                    item.ReceiverNo = item.ReceiverNo.Substring(1);
                    //item.ReceiverNo = "3124825387";
                    await service.SendMessageAsync("92" + item.ReceiverNo, item.ReceiverSubject + "," + System.Environment.NewLine + item.ReceiverMessage);
                }
            }
            return Json(count.Count > 0 ? count.Count : 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ToDate"></param>
        /// <param name="FromDate"></param>
        /// <param name="ListingId"></param>

        public ActionResult GetLeadQueryReport(string FromDate, string ToDate, string ListingId)
        {
            var CName = string.Empty;
            var Results = _homeListing.GetLeadQueryTrack(Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate), Convert.ToDecimal(ListingId), ref CName);

            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><body>");
            //sb.Append("<b style= 'text-decoration: underline; text-align:center'>Lead Query Report </b>"); /*HassanRaza*/
            sb.Append("<div style = 'width:100%;'>");
            sb.Append("<br /><br />");
            sb.Append("<div style = 'width:100%; text-align:center;'><h3><u>Lead Query Report</u></h3></div>");
            sb.Append("<br />");

            sb.Append("<div style = 'width:100%; text-align:center;'><h3>'" + FromDate + "--" + ToDate + "'</h3></div>");
            sb.Append("<br />");
            sb.Append("<br />");

            sb.Append("<table border='1' cellspacing='0' cellpadding='2' style =  'border-collapse:collapse;width:100%'>");
            // sb.Append("<colgroup>");
            // sb.Append("<col style='width:10%'>");
            // sb.Append("<col style='width:20%'>");
            // sb.Append("<col style='width:50%'>");
            // sb.Append("<col style='width:20%'>");

            //// sb.Append("</colgroup>");

            sb.Append("<tr style = 'background-color: #18B5F0; text-align: left;'>");
            sb.Append("<th style = 'font-size:8px;'><b>Sr#</b></th>");
            sb.Append("<th style = 'padding:2px; font-size:8px'><b>Company Name</b></th>");
            sb.Append("<th style = 'padding:2px; font-size:8px'><b>Message</b></th>");
            sb.Append("<th style = 'padding:2px; font-size:8px'><b>Created Date</b></th>");
            sb.Append("</tr>");
            var Couter = 1;
            foreach (var Result in Results)
            {


                sb.Append("<tr style = 'text-align: left;'>");
                sb.Append("<td style = 'width:auto;padding:2px; font-size:8px'>" + Couter + "</td>");
                sb.Append("<td style = 'width:auto;padding:2px; font-size:8px'>" + CName + "</td>");
                sb.Append("<td style = 'width:600px;padding:2px; font-size:8px'>" + Result.AuditName + "</td>");
                sb.Append("<td style = 'width:auto;padding:2px; font-size:8px'>" + Result.CreatedDate + "</td>");

                Couter += 1;
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            sb.Append("</body></html>");

            byte[] bytes;

            StringReader sr = new StringReader(sb.ToString());
            HtmlWorker htmlparser = new HtmlWorker(pdfDoc);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                bytes = memoryStream.ToArray();
                memoryStream.Close();
            }
            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, "ListingLeadReport_'" + DateTime.Now + "'.pdf");

        }

        public static byte[] concatAndAddContent(byte[] pdf)
        {
            byte[] all;

            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document();

                PdfWriter writer = PdfWriter.GetInstance(doc, ms);

                doc.SetPageSize(PageSize.Letter);
                doc.Open();
                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page;

                PdfReader reader;

                reader = new PdfReader(pdf);
                int pages = reader.NumberOfPages;

                // loop over document pages
                for (int i = 1; i <= pages; i++)
                {
                    doc.SetPageSize(PageSize.Letter);
                    doc.NewPage();
                    page = writer.GetImportedPage(reader, i);
                    cb.AddTemplate(page, 0, 0);
                }


                doc.Close();
                all = ms.GetBuffer();
                ms.Flush();
                ms.Dispose();
            }

            return all;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            //HttpContext.Session.SetString("Loc", "0");
            HttpContext.Session.SetString("Gen", "0");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistersViewModel model)
        {

            if (ModelState.IsValid)
            {
                RegisterViewModels register = new RegisterViewModels
                {
                    FirstName = model.FirstName,
                    //LastName = model.LastName,
                    //Cnic = model.Cnic,
                    Email = model.Email,
                    MobileNo = model.MobileNo,
                    Password = model.Password,
                    PasswordHash = _userManager.PasswordHasher.HashPassword(new ApplicationUser { UserName = model.Email, Email = model.Email }, model.Password),
                };
                var result = _systemUserService.RegisterCustomer(register);
                if (result.Result == 1)
                {
                    ApplicationUser user = _userManager.Users.Where(c => c.Id == result.UserId).FirstOrDefault();
                    _logger.LogInformation("User created a new account with password.");
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailClientConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);
                    return RedirectToAction("RegistrationConfirmation", "Home");
                }
                ModelState.AddModelError(string.Empty, result.Message);
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegistrationConfirmation()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> RegisterJson(RegisterViewModels model)
        {
         
            var lookupUser = _userManager.Users.Where(c => c.UserName == model.Email && c.UserTypeId == 2).FirstOrDefault();
            var Cus = _customerService.CheckExiting(model.MobileNo,0);
            if (lookupUser!=null || Cus>0)
            {
                return Json(new { results = 0, Message = "UserName or Mobile No Already Exist." });
            }
            
            model.PasswordHash = _userManager.PasswordHasher.HashPassword(new ApplicationUser { UserName = model.Email, Email = model.Email }, model.Password);
            var result = _systemUserService.RegisterCustomer(model);
            if (result.Result == 1)
            {
                ApplicationUser user = _userManager.Users.Where(c => c.Id == result.UserId).FirstOrDefault();
                _logger.LogInformation("User created a new account with password.");
                // var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                // var callbackUrl = Url.EmailClientConfirmationLink(user.Id, code, Request.Scheme);
                // await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);
           
                if (user != null)
                {
                    var Customer = _customerService.FindByUserId(user.Id);
                    if (Customer != null && Customer.IsActive == true)
                    {
                        await _signInManager.SignInAsync(user, true, authenticationMethod: null);
                      
                    }
                }
            }
            return Json(new { result.Result, result.Message });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Login), "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                ViewBag.Message = "Success";
            }
            else
            {
                ViewBag.Message = "Error";
            }
            return View();
        }
        
        [TempData]
        public string ErrorMessage { get; set; }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            //HttpContext.Session.SetString("Loc", "0");
            HttpContext.Session.SetString("Gen", "0");
            // Clear the existing external cookie to ensure a clean login process
            HttpContext.Session.Clear();
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var lookupUser = _userManager.Users.Where(c => c.UserName == model.Email.ToLower() && c.UserTypeId == 2).FirstOrDefault();
                if (lookupUser != null)
                {
                    var Customers = _customerService.FindByUserId(lookupUser.Id);
                    if (Customers != null && Customers.IsActive == true)
                    {
                        // This doesn't count login failures towards account lockout
                        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            _logger.LogInformation("User logged in.");
                            HttpContext.Session.SetString("UserId", lookupUser.Id);
                            HttpContext.Session.SetString("RoleId", _systemRoleService.GetRoleByUserId(lookupUser.Id));
                            HttpContext.Session.SetString("CustomerId", Customers.Id.ToString());
                            HttpContext.Session.SetString("UserTypeId", lookupUser.UserTypeId.ToString());
                            return RedirectToLocal(returnUrl);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> AccountsLogin(LoginViewModel model)
        {
            bool Results = false;
            var lookupUser = _userManager.Users.Where(c => c.UserName == model.Email.ToLower() && c.UserTypeId == 2).FirstOrDefault();
            if (lookupUser != null)
            {
                var Customers = _customerService.FindByUserId(lookupUser.Id);
                if (Customers != null && Customers.IsActive == true)
                {


                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User logged in.");
                        HttpContext.Session.SetString("UserId", lookupUser.Id);
                        HttpContext.Session.SetString("RoleId", _systemRoleService.GetRoleByUserId(lookupUser.Id));
                        HttpContext.Session.SetString("CustomerId", Customers.Id.ToString());
                        HttpContext.Session.SetString("UserTypeId", lookupUser.UserTypeId.ToString());
                        Results = true;
                    }
                }
            }

            return Json(new { Results });
        }

        //Forget Password For Client
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var Results = true;
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Json(Results);
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.ResetClientPasswordCallbackLink(user.Id, code, Request.Scheme);
            await _emailSender.SendEmailAsync(model.Email, "Reset Password",
               $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
            return Json(Results);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            //HttpContext.Session.SetString("Loc", "0");
            HttpContext.Session.SetString("Gen", "0");
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            AddErrors(result);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        [HttpGet]
        public JsonResult CategoryPopularArea(string CtName, decimal SbCId, string SbCName, string ArName, int TotalRecord)
        {
            return Json(_homeListing.GetPopularCatByArea(CtName, SbCId, SbCName, ArName, TotalRecord));
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AccountLogin(ClientLogin login)
        {
            bool results = false;
            HttpContext.Session.SetString("ClientUserName", "");
            HttpContext.Session.SetString("ClientMobile", "");
            HttpContext.Session.SetString("ClientOtp", "");
            HttpContext.Session.SetInt32("ClientOtpCounts", 0);

            if (_homeListing.CustomerClientLogin(login))
            {
                var dd = CommonNoGen.GenerateFourDigitRandomNo();
                //var dd = 1111;
                HttpContext.Session.SetString("ClientOtp", dd.ToString());
                HttpContext.Session.SetString("ClientUserName", login.UserName);
                HttpContext.Session.SetString("ClientMobile", login.Number);
                
                var phoneNo = login.Number.Substring(1);

                await service.SendMessageAsync("92" +phoneNo, "Your Otp Code : " + dd.ToString());
                
                results = true;
            }
            return Json(results);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ChangeNumber(ClientChangeNo login)
        {
            decimal results = 0;
            HttpContext.Session.SetString("ClientUserName", "");
            HttpContext.Session.SetString("ClientMobile", "");
            HttpContext.Session.SetString("ClientOtp", "");
            HttpContext.Session.SetInt32("ClientOtpCounts", 0);
            ClientLogin ent = new ClientLogin();
            ent.UserName = login.UserName;
            ent.Number = login.Number;
            if (_homeListing.CustomerClientLogin(ent))
            {
                var rs = _homeListing.changeNumber(login);
                if(rs==1)
                {
                    var dd = CommonNoGen.GenerateFourDigitRandomNo();
                    //var dd = 1111;
                    HttpContext.Session.SetString("ClientOtp", dd.ToString());
                    HttpContext.Session.SetString("ClientUserName", login.UserName);
                    HttpContext.Session.SetString("ClientMobile", login.Number);

                    var phoneNo = login.Number.Substring(1);

                    await service.SendMessageAsync("92" + phoneNo, "Your Otp Code : " + dd.ToString());

                    results = rs;
                }
                 else
                {
                    results = rs;
                }
                
            }
            return Json(results);
        }
        public async Task<JsonResult> ResendCode(string No)
        {
            bool results = false;
           
            HttpContext.Session.SetString("ClientOtp", "");
            HttpContext.Session.SetInt32("ClientOtpCounts", 0);
          
                var dd = CommonNoGen.GenerateFourDigitRandomNo();
                //var dd = 1111;
                HttpContext.Session.SetString("ClientOtp", dd.ToString());
               
                var phoneNo = No.Substring(1);

                await service.SendMessageAsync("92" + phoneNo, "Your Otp Code : " + dd.ToString());             
                results = true;
           
            return Json(results);
        }

        public async System.Threading.Tasks.Task<JsonResult> LoginOtp(string OtpCode)
        {
            bool results = false;
            var counter = HttpContext.Session.GetInt32("ClientOtpCounts");
            if (counter < 2)
            {
                if (HttpContext.Session.GetString("ClientOtp") == OtpCode)
                {
                    if (!string.IsNullOrEmpty(HttpContext.Session.GetString("ClientUserName")) && !string.IsNullOrEmpty(HttpContext.Session.GetString("ClientMobile")))
                    {
                        if (_homeListing.CustomerClientLogin(new ClientLogin { UserName = HttpContext.Session.GetString("ClientUserName"), Number = HttpContext.Session.GetString("ClientMobile") }))
                        {
                            var lookupUser = _userManager.Users.Where(c => c.UserName == HttpContext.Session.GetString("ClientUserName") && c.UserTypeId == 2).FirstOrDefault();
                            var user = await _userManager.FindByNameAsync(lookupUser.UserName);
                            if (lookupUser != null)
                            {
                                var Customer = _customerService.FindByUserId(lookupUser.Id);
                                if (Customer != null && Customer.IsActive == true)
                                {
                                    await _signInManager.SignInAsync(user, true, authenticationMethod: null);
                                    HttpContext.Session.SetString("ClientUserName", "");
                                    HttpContext.Session.SetString("ClientMobile", "");
                                    HttpContext.Session.SetString("ClientOtp", "");
                                    HttpContext.Session.SetInt32("ClientOtpCounts", 0);
                                    HttpContext.Session.SetString("UserId", lookupUser.Id);
                                    HttpContext.Session.SetString("CustomerId", Customer.Id.ToString());
                                    HttpContext.Session.SetString("RoleId", _systemRoleService.GetRoleByUserId(lookupUser.Id));
                                    HttpContext.Session.SetString("UserTypeId", lookupUser.UserTypeId.ToString());
                                    results = true;
                                }
                            }
                        }

                    }
                }
            }
            if (results == false)
            {
                counter++;
                HttpContext.Session.SetInt32("ClientOtpCounts", (int)counter);
            }
            return Json(new { results, wrongCount = counter });
        }
        #region Social Media
        /// <summary>
        /// /// function for ExternalLogin.
        /// Author :Rizwan Ahmed.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="returnUrl"></param>
        /// <returns>provider, properties</returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
         {
           
          
            var redirectUrl = Url.Action("ExternalLoginCallback", "Home", new { ReturnUrl = returnUrl });
        

             var properties =_signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
          



            return new ChallengeResult(provider, properties);
        }
        
        /// <summary>
        /// function for ExternalLoginCallback.
        /// Author :Rizwan Ahmed.
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="remoteError"></param>
        /// <returns> returnUrl</returns>
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            
            returnUrl = returnUrl ?? Url.Content("~/");
          



            LoginViewModel loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");

                return View("Login", loginViewModel);
            }
         

            // Get the login information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
        
        
            if (info == null)
            {
                ModelState
                .AddModelError(string.Empty, "Error loading external login information.");

                return View("Login", loginViewModel);
            }

            // If the user already has a login (i.e if there is a record in AspNetUserLogins
            // table) then sign-in the user with this external login provider
            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            // If there is no record in AspNetUserLogins table, the user may not have
            // a local account
            else
            {
                // Get the email claim value
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    // Create a new user without password if we do not have a user already
                    var user = await _userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            EmailConfirmed = true,
                            UserTypeId = 2

                        };

                        await _userManager.CreateAsync(user);
                    }

                    // Add a login (i.e insert a row for the user in AspNetUserLogins table)
                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }

                // If we cannot find the user email we cannot continue
                ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";


                return View("Error");
            }
        }
        /// <summary>
        /// function for ExternalLoginConfirmation.
        /// Author :Rizwan Ahmed.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                //Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    throw new ApplicationException("Error loading external login information during confirmation.");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, EmailConfirmed = true, UserTypeId = 2 };

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(ExternalLogin), model);
        }
    }
    #endregion

}

