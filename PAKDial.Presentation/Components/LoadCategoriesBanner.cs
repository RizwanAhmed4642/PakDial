using Microsoft.AspNetCore.Mvc;
using PAKDial.Interfaces.PakDialServices.IHomeLandingPageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class LoadCategoriesBanner : ViewComponent
    {
        private readonly IMainMenuCategoryService _categoryService;
        public LoadCategoriesBanner(IMainMenuCategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync(decimal CatId)
        {
            return View(_categoryService.GetBannerModel(CatId));
        }
    }
}
