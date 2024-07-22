using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.ICompaniesListingsRepo
{
    public interface IVerifiedListingRepository : IBaseRepository<VerifiedListing,decimal>
    {
        List<VerifiedListing> GetByListingId(decimal ListingId);
        List<VerifiedListing> GetListingByServiceId(decimal VerificationId);
        VerifiedListing GetListingByService(decimal VerificationId, decimal ListingId);
    }
}
