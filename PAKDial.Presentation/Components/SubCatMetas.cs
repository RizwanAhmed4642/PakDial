using Microsoft.AspNetCore.Mvc;
using PAKDial.Interfaces.PakDialServices.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class SubCatMetas : ViewComponent
    {
        private readonly IHomeListingService _Repo;
        public SubCatMetas(IHomeListingService Repo)
        {
            _Repo = Repo;
        }
        public async Task<IViewComponentResult> InvokeAsync(string Location, decimal SubCatId)
        {
            return View(_Repo.GetSubMetaDetail(Location, SubCatId));
        }

    }
}
