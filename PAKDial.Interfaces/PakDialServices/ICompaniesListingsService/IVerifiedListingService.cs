using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.ICompaniesListingsService
{
    public interface IVerifiedListingService
    {
        VerifiedListing FindById(decimal Id);
        IEnumerable<VerifiedListing> GetAll();
        List<VerifiedListing> GetByListingId(decimal ListingId);
        List<VerifiedListing> GetListingByServiceId(decimal VerificationId);
        VerifiedListing GetListingByService(decimal VerificationId, decimal ListingId);
    }
}
