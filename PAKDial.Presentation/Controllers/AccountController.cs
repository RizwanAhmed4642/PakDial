using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Common;
using PAKDial.Domains.Common;
using PAKDial.Domains.IdentityManagement;
using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Presentation.Extensions;
using PAKDial.Presentation.Models.AccountViewModels;
using PAKDial.Presentation.Services;


namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISystemRoleService systemRoleService;
		private readonly IEmployeeService employeeService;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IEmployeeService employeeService,
            ISystemRoleService systemRoleService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            this.employeeService = employeeService;
            this.systemRoleService = systemRoleService;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            if(User.Identity.IsAuthenticated)
            {
                if(string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                {
                    var Users = _userManager.Users.Where(c => c.UserName == User.Identity.Name && c.UserTypeId == 1).FirstOrDefault();
                    if(Users == null)
                    {
                        HttpContext.Session.Clear();
                        await _signInManager.SignOutAsync();
                        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
                        ViewData["ReturnUrl"] = returnUrl;
                        return View();
                    }
                    var RoleId = systemRoleService.GetRoleByUserId(Users.Id.ToString());
                    var EmployeeId = employeeService.FindByUserId(Users.Id.ToString()).Id.ToString();
                    HttpContext.Session.SetString("UserId", Users.Id.ToString());
                    HttpContext.Session.SetString("RoleId", RoleId);
                    HttpContext.Session.SetString("EmployeeId", EmployeeId);
                    HttpContext.Session.SetString("UserTypeId", Users.UserTypeId.ToString());
                }
                var user = User.Identity.Name;
                var RoleName = _roleManager.Roles.Where(c => c.Id == systemRoleService.GetRoleByUserId(HttpContext.Session.GetString("UserId"))).FirstOrDefault();
                if (CommonSpacing.RemoveSpacestoTrim(RoleName.Name) == DesignationNames.SalesExecutive.ToString().ToLower()
                         || CommonSpacing.RemoveSpacestoTrim(RoleName.Name) == DesignationNames.SalesManager.ToString().ToLower()
                         || CommonSpacing.RemoveSpacestoTrim(RoleName.Name) == DesignationNames.CallCenter.ToString().ToLower())
                {
                    return RedirectToLocal("/AdminDashBoard/Analytics");
                }
                else if (CommonSpacing.RemoveSpacestoTrim(RoleName.Name) == DesignationNames.Teller.ToString().ToLower())
                {
                    return RedirectToLocal("/AdminDashBoard/Teller");
                }
                else if (CommonSpacing.RemoveSpacestoTrim(RoleName.Name) == DesignationNames.ZoneManager.ToString().ToLower())
                {
                    return RedirectToLocal("/AdminDashBoard/ZoneManagerAnalytics");
                }
                else if (CommonSpacing.RemoveSpacestoTrim(RoleName.Name) == DesignationNames.RegionalManager.ToString().ToLower())
                {
                    return RedirectToLocal("/AdminDashBoard/RegionalManagerAnalytic");
                }
                else if (CommonSpacing.RemoveSpacestoTrim(RoleName.Name) == DesignationNames.CRO.ToString().ToLower())
                {
                    return RedirectToLocal("/AdminDashBoard/CEOAnalytic");
                }
                else if (CommonSpacing.RemoveSpacestoTrim(RoleName.Name) == DesignationNames.CEO.ToString().ToLower())
                {
                    return RedirectToLocal("/AdminDashBoard/CEOAnalytic");
                }
                else if (CommonSpacing.RemoveSpacestoTrim(RoleName.Name) == DesignationNames.SuperAdmin.ToString().ToLower())
                {
                    return RedirectToLocal("/AdminDashBoard/AdminAnalytics");
                }
                else
                {
                    return RedirectToLocal("/AdminDashBoard/GeneralAnalytics");
                }
            }
            // Clear the existing external cookie to ensure a clean login process
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                //User Type One Means Employee..
                var lookupUser = _userManager.Users.Where(c => c.UserName == model.Email && c.UserTypeId == 1).FirstOrDefault();
                if (lookupUser != null)
                {
                    var RoleName = _roleManager.Roles.Where(c => c.Id == systemRoleService.GetRoleByUserId(lookupUser.Id)).FirstOrDefault();
                    var Employees = employeeService.FindByUserId(lookupUser.Id);
                    if (Employees != null && Employees.IsActive == true)
                    {
                        // This doesn't count login failures towards account lockout
                        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                        if (result.Succeeded)
                        {
                            HttpContext.Session.SetString("UserId", lookupUser.Id);
                            HttpContext.Session.SetString("RoleId", systemRoleService.GetRoleByUserId(lookupUser.Id));
                            HttpContext.Session.SetString("EmployeeId", Employees.Id.ToString());
                            HttpContext.Session.SetString("UserTypeId", lookupUser.UserTypeId.ToString());

                            if (returnUrl != null)
                            {
                                return RedirectToLocal(returnUrl);
                            }
                            else
                            {
                                if (CommonSpacing.RemoveSpacestoTrim(RoleName.Name) == DesignationNames.SalesExecutive.ToString().ToLower()
                                    || CommonSpacing.RemoveSpacestoTrim(RoleName.Name) == DesignationNames.SalesManager.ToString().ToLower()
                                    || CommonSpacing.RemoveSpacestoTrim(RoleName.Name) == DesignationNames.CallCenter.ToString().ToLower())
                                {
                                    return RedirectToLocal("/AdminDashBoard/Analytics");
                                }
                                else if(CommonSpacing.RemoveSpacestoTrim(RoleName.Name) == DesignationNames.Teller.ToString().ToLower())
                                {
                                    return RedirectToLocal("/AdminDashBoard/Teller");
                                }
                                else if (CommonSpacing.RemoveSpacestoTrim(RoleName.Name) == DesignationNames.ZoneManager.ToString().ToLower())
                                {
                                    return RedirectToLocal("/AdminDashBoard/ZoneManagerAnalytics");
                                }
                                else if (CommonSpacing.RemoveSpacestoTrim(RoleName.Name) == DesignationNames.RegionalManager.ToString().ToLower())
                                {
                                    return RedirectToLocal("/AdminDashBoard/RegionalManagerAnalytic");
                                }
                                else if (CommonSpacing.RemoveSpacestoTrim(RoleName.Name) == DesignationNames.CRO.ToString().ToLower())
                                {
                                    return RedirectToLocal("/AdminDashBoard/CEOAnalytic");
                                }
                                else if (CommonSpacing.RemoveSpacestoTrim(RoleName.Name) == DesignationNames.CEO.ToString().ToLower())
                                {
                                    return RedirectToLocal("/AdminDashBoard/CEOAnalytic");
                                }
                                else if (CommonSpacing.RemoveSpacestoTrim(RoleName.Name) == DesignationNames.SuperAdmin.ToString().ToLower())
                                {
                                    return RedirectToLocal("/AdminDashBoard/AdminAnalytics");
                                }
                                else
                                {
                                    return RedirectToLocal("/AdminDashBoard/GeneralAnalytics");

                                }
                            }
                        }
                        if (result.RequiresTwoFactor)
                        {
                            HttpContext.Session.SetString("UserId", lookupUser.Id);
                            HttpContext.Session.SetString("RoleId", systemRoleService.GetRoleByUserId(lookupUser.Id));
                            HttpContext.Session.SetString("EmployeeId", Employees.Id.ToString());
                            HttpContext.Session.SetString("UserTypeId", lookupUser.UserTypeId.ToString());

                            return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
                        }
                        if (result.IsLockedOut)
                        {
                            return RedirectToAction(nameof(Lockout));
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                            return View(model);
                        }
                    }
                    else
                    {
                        return RedirectToAction("Restricted", "Account");
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

        [AllowAnonymous]
        public IActionResult Restricted()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        [AllowAnonymous]
        public async Task<IActionResult> SignOut()
        {
            HttpContext.Session.Clear();
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLogin", new ExternalLoginViewModel { Email = email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    throw new ApplicationException("Error loading external login information during confirmation.");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                   $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
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
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2fa(bool rememberMe, string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var model = new LoginWith2faViewModel { RememberMe = rememberMe };
            ViewData["ReturnUrl"] = returnUrl;

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWith2fa(LoginWith2faViewModel model, bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var authenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.RememberMachine);

            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return View();
            }
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
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
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }
        }

        #endregion
    }
}