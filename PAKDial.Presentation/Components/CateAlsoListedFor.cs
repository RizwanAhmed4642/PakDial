using Microsoft.AspNetCore.Mvc;
using PAKDial.Interfaces.PakDialServices.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class CateAlsoListedFor :ViewComponent
    {
        #region Prop

        public readonly IHomeListingService _IHomeListingService;
        #endregion

        #region Ctor
        public CateAlsoListedFor(IHomeListingService IHomeListingService)
        {
            _IHomeListingService = IHomeListingService;
        }

        #endregion

        #region Method
         public async Task<IViewComponentResult> InvokeAsync(decimal ListingId,string Location)
        {
            return View(_IHomeListingService.GetCompanyListingSubCate(ListingId, Location));
        }
        #endregion
    }
}
