using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.UserEndViewModel;
using System;
using System.Collections.Generic;

namespace PAKDial.Interfaces.Repository
{
    public interface ISubMenuCategoryRepository : IBaseRepository<SubMenuCategory, Decimal>
    {
        List<SubMenuCategory> FindByMainCategory(decimal MainCategoryId);
        SubMenuCategory GetSubCateById(decimal SubCategoryId);
        List<SubMenuCategory> FindByMPSubCategory(decimal MainCategoryId , decimal ParentSubCategoryId);
        List<SubMenuCategory> FindByPSubCategory(decimal ParentSubCategoryId);
        SubMenuCategoryResponse GetSubMenus(SubMenuCategoryRequestModel request);
        GetSubCategoryResponse GetSubMenusList(SubCategoryRequestModel request);
        GetSubMenuCategoryResponse GetSubMenusSearchList(SubCategoryRequestModel request);
        bool CheckExistance(SubMenuCategory instance);
        decimal IsPopularCategory(decimal Id, string name);
        bool SubCategoryExistsAgainstMain(decimal MainCategoryId);
        void UploadImages(decimal Id, string ImagePath, int Type, string AbsolutePath);
        SubMenuCategory GetByTrackIds(string TrackIds);
        SubMenuCategory GetByTrackName(string TrackNames);
        decimal AddSubCategory(SubMenuCategory instance);
        int UpdateSubCategory(SubMenuCategory instance);

        List<LoadSubCategoryModel> GetLoadSubCategories(decimal CatId, decimal? SubCatId, bool IsSubCategory);
        List<SearchSbCategories> SearchFrontEndSubCategory(string SbCategoryName,string Location);
        CategoryMetasKeywords GetSubMetaDetail(string Location, decimal CatId);
        List<MainMenuSubMenu> MainMenuSubMenuData();

    }
}
