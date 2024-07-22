using Microsoft.AspNetCore.Mvc;
using PAKDial.Interfaces.PakDialServices.Home;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class CompListingModeOfPaymentsMobile : ViewComponent
    {
        #region Prop

        private readonly IHomeListingService _IHomeListingService;

        #endregion

        #region Ctor

        public CompListingModeOfPaymentsMobile(IHomeListingService IHomeListingService)
        {
            _IHomeListingService = IHomeListingService;
        }
        #endregion

        #region Method
        public async Task<IViewComponentResult> InvokeAsync(decimal ListingId)
        {
            return View(_IHomeListingService.GetCompListingPaymentMode(ListingId));
        }
        #endregion
    }
}
