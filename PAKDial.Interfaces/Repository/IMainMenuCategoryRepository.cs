using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.UserEndViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository
{
    public interface IMainMenuCategoryRepository : IBaseRepository<MainMenuCategory, Decimal>
    {
        MainMenuCategoryResponse GetMenus(MainMenuCategoryRequestModel request);
        GetMainMenuCategoryResponse GetMainMenuSearchList(MainMenuCategoryRequestModel request);
        bool CheckExistance(MainMenuCategory instance);
        void UploadImages(decimal Id, string ImagePath, int Type, string AbsolutePath);
        List<MainSideBarMenu> GetSideMainMenu();
        CategoryBannerModel GetBannerModel(decimal Id);
        CategoryMetasKeywords GetMetaDetail(string Location, decimal CatId);
        List<MainSideBarMenu> GetSideMainMenu(int RecordsRequireds);
        List<MainSideBarMenu> GetRegListMenu(int CatRequireds);


    }
}
