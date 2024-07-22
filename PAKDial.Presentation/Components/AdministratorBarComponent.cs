using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.IdentityManagement;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.PakDialServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class AdministratorBarComponent : ViewComponent
    {
        private readonly ISystemUserService systemUserService;

        public AdministratorBarComponent(ISystemUserService systemUserService)
        {
            this.systemUserService = systemUserService;
        }

        public IViewComponentResult Invoke()
        {
            AdminBarModel model = new AdminBarModel();
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                var Users = systemUserService.FindByEmail(HttpContext.User.Identity.Name);
                if (Users != null)
                {
                    model.UserTypeId = (decimal)Users.UserTypeId;
                }
            }
            return View(model);
        }
    }
}
