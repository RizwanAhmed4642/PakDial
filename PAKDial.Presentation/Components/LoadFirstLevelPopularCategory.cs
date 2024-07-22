using Microsoft.AspNetCore.Mvc;
using PAKDial.Interfaces.PakDialServices.IHomeLandingPageService;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class LoadFirstLevelPopularCategory : ViewComponent
    {
        private readonly ISubMenuCategoryService _FirstLevelCategory;
        public LoadFirstLevelPopularCategory(ISubMenuCategoryService FirstLevelCategory)
        {
            _FirstLevelCategory = FirstLevelCategory;
        }
        public async Task<IViewComponentResult> InvokeAsync(decimal CatId)
        {
            return View(_FirstLevelCategory.GetLoadSubCategories(CatId, 0, false));
        }
    }
}
