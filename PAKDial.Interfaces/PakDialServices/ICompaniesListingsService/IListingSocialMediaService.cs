using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.ICompaniesListingsService
{
    public interface IListingSocialMediaService
    {
        ListingSocialMedia FindById(decimal Id);
        IEnumerable<ListingSocialMedia> GetAll();
        List<ListingSocialMedia> GetByListingId(decimal ListingId);
        List<ListingSocialMedia> GetListingSocialMediaId(decimal MediaId);
        ListingSocialMedia GetListingSocialMedia(decimal MediaId, decimal ListingId);
    }
}
