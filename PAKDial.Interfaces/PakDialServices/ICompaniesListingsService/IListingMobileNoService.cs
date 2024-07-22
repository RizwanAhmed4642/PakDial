using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.ICompaniesListingsService
{
    public interface IListingMobileNoService
    {
        ListingMobileNo FindById(decimal Id);
        IEnumerable<ListingMobileNo> GetAll();
        List<ListingMobileNo> GetByListingId(decimal ListingId);
    }
}
