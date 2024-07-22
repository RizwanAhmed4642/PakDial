using Microsoft.AspNetCore.Mvc;
using PAKDial.Interfaces.PakDialServices.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class CategoryPopularArea : ViewComponent
    {
        private readonly IHomeListingService _Repo;
        public CategoryPopularArea(IHomeListingService Repo)
        {
            _Repo = Repo;
        }
        public async Task<IViewComponentResult> InvokeAsync(string CtName, decimal SbCId, string SbCName, string ArName)
        {
            return View(_Repo.GetPopularCatByArea(CtName,SbCId,SbCName,ArName));
        }

    }
}
