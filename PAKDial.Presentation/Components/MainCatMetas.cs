using Microsoft.AspNetCore.Mvc;
using PAKDial.Interfaces.PakDialServices.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class MainCatMetas : ViewComponent
    {
        private readonly IHomeListingService _Repo;
        public MainCatMetas(IHomeListingService Repo)
        {
            _Repo = Repo;
        }
        public async Task<IViewComponentResult> InvokeAsync(string Location, decimal CatId)
        {
            return View(_Repo.GetMetaDetail(Location, CatId));
        }
    }
}
