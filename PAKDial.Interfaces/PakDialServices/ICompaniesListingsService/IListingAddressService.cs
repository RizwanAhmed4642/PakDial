using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.ICompaniesListingsService
{
    public interface IListingAddressService
    {
        ListingAddress FindById(decimal Id);
        IEnumerable<ListingAddress> GetAll();
        List<ListingAddress> GetByListingId(decimal ListingId);

    }
}
