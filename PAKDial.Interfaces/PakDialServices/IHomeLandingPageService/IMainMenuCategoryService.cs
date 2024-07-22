using Microsoft.AspNetCore.Http;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.UserEndViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.IHomeLandingPageService
{
    public interface IMainMenuCategoryService
    {
        int Update(MainMenuCategory instance);
        int Delete(decimal Id);
        decimal Add(MainMenuCategory instance);
        int UploadCategoryBannerImage(decimal Id, IFormFile file, string AbsolutePath);
        int UploadCategoryFeatureImage(decimal Id, IFormFile file, string AbsolutePath);
        MainMenuCategory FindById(decimal id);
        IEnumerable<MainMenuCategory> GetAll();
        MainMenuCategoryResponse GetMenus(MainMenuCategoryRequestModel request);
        GetMainMenuCategoryResponse GetMainMenuSearchList(MainMenuCategoryRequestModel request);

        int UploadWebIcons(decimal Id, IFormFile file, string AbsolutePath);
        int UploadMobileIcons(decimal Id, IFormFile file, string AbsolutePath);
        List<MainSideBarMenu> GetSideMainMenu();

        CategoryBannerModel GetBannerModel(decimal Id);
        List<MainSideBarMenu> GetSideMainMenu(int RecordsRequireds);
        List<MainSideBarMenu> GetRegListMenu(int CatRequireds);
    }
}
