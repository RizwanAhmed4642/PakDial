using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Presentation.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class CustomerProfileShowComponent : ViewComponent
    {
        private readonly ISystemUserService systemUserService;
        private readonly ICustomerService _ICustomerService;

        public CustomerProfileShowComponent(ISystemUserService systemUserService, ICustomerService ICustomerService)
        {
            this.systemUserService = systemUserService;
            _ICustomerService = ICustomerService;
        }
        [ServiceFilter(typeof(ClientAuthorizationAttribute))]
        public IViewComponentResult Invoke()
        {
            AdminBarModel model = new AdminBarModel();
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                var Users = systemUserService.FindByEmail(HttpContext.User.Identity.Name);
                if(HttpContext.Session.GetString("CustomerId")!=null)
                {
                    var Url = _ICustomerService.FindById(Convert.ToDecimal(HttpContext.Session.GetString("CustomerId"))).ImagePath;
                    if(Url !=null && !string.IsNullOrEmpty(Url))
                    {
                        model.imageUrl = Url;
                    }
                }
               
                if (Users != null)
                {
                    model.UserTypeId = (decimal)Users.UserTypeId;
                    
                }
            }
            return View(model);
        }
    }
}
