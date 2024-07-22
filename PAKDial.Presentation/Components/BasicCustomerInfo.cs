using Microsoft.AspNetCore.Mvc;
using PAKDial.Interfaces.PakDialServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class BasicCustomerInfo :ViewComponent
    {
        #region Prop
        private readonly ICustomerService _ICustomerService;
        #endregion

        #region Cotr


        public BasicCustomerInfo(ICustomerService ICustomerService)
        {
            _ICustomerService = ICustomerService;
        }


        #endregion

        #region Method

        public async Task<IViewComponentResult> InvokeAsync(decimal CustomerId)
        {
            
            return View(_ICustomerService.FindById(CustomerId));
        }
        #endregion
    }
}
