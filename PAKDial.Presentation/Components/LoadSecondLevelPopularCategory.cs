using Microsoft.AspNetCore.Mvc;
using PAKDial.Interfaces.PakDialServices.IHomeLandingPageService;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class LoadSecondLevelPopularCategory : ViewComponent
    {
        private readonly ISubMenuCategoryService _FirstLevelCategory;
        public LoadSecondLevelPopularCategory(ISubMenuCategoryService FirstLevelCategory)
        {
            _FirstLevelCategory = FirstLevelCategory;
        }
        public async Task<IViewComponentResult> InvokeAsync(decimal CatId, decimal SubCatId, bool IsSubCategory)
        {
            return View(_FirstLevelCategory.GetLoadSubCategories(CatId, SubCatId, IsSubCategory));
        }
    }
}
