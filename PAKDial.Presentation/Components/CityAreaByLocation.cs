using Microsoft.AspNetCore.Mvc;
using PAKDial.Interfaces.PakDialServices.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class CityAreaByLocation : ViewComponent
    {
        private readonly IHomeListingService _IHomeListingService;
        public CityAreaByLocation(IHomeListingService IHomeListingService)
        {
            _IHomeListingService = IHomeListingService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string location)
        {
            return View(_IHomeListingService.GetCityNameByArea(location));
        }
    }
}
