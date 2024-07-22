using Microsoft.AspNetCore.Mvc;
using PAKDial.Interfaces.PakDialServices.IHomeLandingPageService;
using PAKDial.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class HomeSlider : ViewComponent
    {
        private readonly IOutSourceAdvertismentService outSourceAdvertismentService;
        public HomeSlider(IOutSourceAdvertismentService outSourceAdvertismentService)
        {
            this.outSourceAdvertismentService = outSourceAdvertismentService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(outSourceAdvertismentService.GetLoadHomeSlider());
        }
    }
}
