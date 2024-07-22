using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.ICompaniesListingsRepo
{
    public interface IListingSocialMediaRepository : IBaseRepository<ListingSocialMedia,decimal>
    {
        List<ListingSocialMedia> GetByListingId(decimal ListingId);
        List<ListingSocialMedia> GetListingSocialMediaId(decimal MediaId);
        ListingSocialMedia GetListingSocialMedia(decimal MediaId, decimal ListingId);
        SocialMediaModes GetSocialMediaModes(decimal MediaId);
        List<SocialMediaModes> SocialMediaModesList();
    }
}
