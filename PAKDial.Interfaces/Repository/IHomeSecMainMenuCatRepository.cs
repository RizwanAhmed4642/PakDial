using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.SqlViewModels;
using System;
using System.Collections.Generic;

namespace PAKDial.Interfaces.Repository
{
    public interface IHomeSecMainMenuCatRepository : IBaseRepository<HomeSecMainMenuCat, Decimal>
    {
        HomeSecMainMenuCatResponse GetHomeSecMainMenu(HomeSecMainMenuCatRequestModel request);
        List<HomeSecMainMenuCat> GetHomeSecByMenuId(decimal MainMenuCatId);
        List<HomeSecMainMenuCat> GetHomeSecBySecId(decimal HomeSecCatId);
        List<HomeSecMainMenuCat> GetHomeSecByBothId(decimal MainMenuCatId, decimal HomeSecCatId);
    }
}
