using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.ICompaniesListingsService
{
    public interface IListingLandlineNoService
    {
        ListingLandlineNo FindById(decimal Id);
        IEnumerable<ListingLandlineNo> GetAll();
        List<ListingLandlineNo> GetByListingId(decimal ListingId);

    }
}
