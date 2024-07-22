using PAKDial.Domains.DomainModels;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Domains.UserEndViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.Home
{
    public interface IVLoadHomePopularServiceRepository
    {
        List<VLoadHomePopularService> GetHomePopularServiceRepository();

        List<MainMenuSubMenu> GetSubMenu(decimal CatId);
        List<GetBulkQueryFormSubmittion> GetBulkQueryFormSubmittion(ListingQueryRequest request);
        List<ListingQueryTrack> GetLeadQueryTrack(DateTime FromDate , DateTime ToDate, decimal ListingId, ref string CName);
    }
}
