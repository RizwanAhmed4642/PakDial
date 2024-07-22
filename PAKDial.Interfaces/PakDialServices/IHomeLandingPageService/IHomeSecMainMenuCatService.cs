using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System.Collections.Generic;

namespace PAKDial.Interfaces.PakDialServices.IHomeLandingPageService
{
    public interface IHomeSecMainMenuCatService
    {
        int Update(HomeSecMainMenuCat instance);
        int Delete(HomeSecMainMenuCat instance);
        int Add(HomeSecMainMenuCat instance);
        HomeSecMainMenuCat FindById(decimal id);
        IEnumerable<HomeSecMainMenuCat> GetAll();
        HomeSecMainMenuCatResponse GetHomeSecMainMenu(HomeSecMainMenuCatRequestModel request);
        List<HomeSecMainMenuCat> GetHomeSecByMenuId(decimal MainMenuCatId);
        List<HomeSecMainMenuCat> GetHomeSecBySecId(decimal HomeSecCatId);
        List<HomeSecMainMenuCat> GetHomeSecByBothId(decimal MainMenuCatId, decimal HomeSecCatId);
    }
}
