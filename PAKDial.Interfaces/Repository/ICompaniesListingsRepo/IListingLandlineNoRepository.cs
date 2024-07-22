using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.ICompaniesListingsRepo
{
    public interface IListingLandlineNoRepository : IBaseRepository<ListingLandlineNo,decimal>
    {
        List<ListingLandlineNo> GetByListingId(decimal ListingId);
       List<ListingLandlineNo> GetListingLandlineNoByName(string LandLineNo, int Id);
    }
}
