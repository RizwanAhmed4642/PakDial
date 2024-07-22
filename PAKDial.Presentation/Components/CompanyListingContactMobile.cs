using Microsoft.AspNetCore.Mvc;
using PAKDial.Interfaces.PakDialServices.Home;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class CompanyListingContactMobile : ViewComponent
    {
        #region Prop
        public readonly IHomeListingService _IHomeListingService;
        #endregion

        #region Ctor

        public CompanyListingContactMobile(IHomeListingService IHomeListingService)
        {
            _IHomeListingService = IHomeListingService;
        }

        #endregion

        #region Method

        public async Task<IViewComponentResult> InvokeAsync(decimal ListingId)
        {
            return View(_IHomeListingService.GetContactNoMobile(ListingId));
        }
        #endregion
    }
}
