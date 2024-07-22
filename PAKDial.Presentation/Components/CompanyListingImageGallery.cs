using Microsoft.AspNetCore.Mvc;
using PAKDial.Interfaces.PakDialServices.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class CompanyListingImageGallery: ViewComponent
    {

        private readonly IHomeListingService _IHomeListingService;
        public CompanyListingImageGallery(IHomeListingService IHomeListingService)
        {
            _IHomeListingService =IHomeListingService; 
        }
         public async Task<IViewComponentResult> InvokeAsync(decimal ListingId)
        {
            return View(_IHomeListingService.GetCompLstImageGallery(ListingId));
        }
    }
}
