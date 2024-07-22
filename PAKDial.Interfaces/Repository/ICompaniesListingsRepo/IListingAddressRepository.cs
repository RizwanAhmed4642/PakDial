using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.ICompaniesListingsRepo
{
    public interface IListingAddressRepository : IBaseRepository<ListingAddress,decimal>
    {
        List<ListingAddress> GetByListingId(decimal ListingId);
        string GetAddressByListingId(decimal ListingId);
    }
}
