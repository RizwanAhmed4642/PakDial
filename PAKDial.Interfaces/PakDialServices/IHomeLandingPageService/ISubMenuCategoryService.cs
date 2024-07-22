using Microsoft.AspNetCore.Http;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.UserEndViewModel;
using System.Collections.Generic;

namespace PAKDial.Interfaces.PakDialServices.IHomeLandingPageService
{
    public interface ISubMenuCategoryService
    {
        int Update(SubMenuCategory instance);
        int Delete(decimal Id);
        decimal Add(SubMenuCategory instance);
        SubMenuCategory FindById(decimal id);
        IEnumerable<SubMenuCategory> GetAll();
        int UploadCategoryBannerImage(decimal Id, IFormFile file, string AbsolutePath);
        int UploadCategoryFeatureImage(decimal Id, IFormFile file, string AbsolutePath);
        int UploadWebIcon(decimal Id, IFormFile file, string AbsolutePath);
        int UploadMobileIcon(decimal Id, IFormFile file, string AbsolutePath);
        List<SubMenuCategory> FindByMainCategory(decimal MainCategoryId);
        List<SubMenuCategory> FindByMPSubCategory(decimal MainCategoryId, decimal ParentSubCategoryId);
        List<SubMenuCategory> FindByPSubCategory(decimal ParentSubCategoryId);
        SubMenuCategoryResponse GetSubMenus(SubMenuCategoryRequestModel request);
        GetSubCategoryResponse GetSubMenusList(SubCategoryRequestModel request);
        GetSubMenuCategoryResponse GetSubMenusSearchList(SubCategoryRequestModel request);
        List<LoadSubCategoryModel> GetLoadSubCategories(decimal CatId, decimal? SubCatId, bool IsSubCategory);
        List<MainMenuSubMenu> MainMenuSubMenuData();
        decimal IsPopularCategory(decimal Id, string name);
    }
}
