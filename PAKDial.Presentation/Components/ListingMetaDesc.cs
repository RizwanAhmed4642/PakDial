using Microsoft.AspNetCore.Mvc;
using PAKDial.Interfaces.PakDialServices.Home;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class ListingMetaDesc : ViewComponent
    {
        private readonly IHomeListingService _Repo;
        public ListingMetaDesc(IHomeListingService Repo)
        {
            _Repo = Repo;
        }
        public async Task<IViewComponentResult> InvokeAsync(string Location, decimal ListingId)
        {
            return View(_Repo.GetListingMetaDetail(Location, ListingId));
        }
    }
}
