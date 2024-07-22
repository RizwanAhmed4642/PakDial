using PAKDial.Domains.DomainModels;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.CompaniesListingsService
{
    public class VerifiedListingService : IVerifiedListingService
    {
        private readonly IVerifiedListingRepository verifiedListingRepository;
        public VerifiedListingService(IVerifiedListingRepository verifiedListingRepository)
        {
            this.verifiedListingRepository = verifiedListingRepository;
        }
        public VerifiedListing FindById(decimal Id)
        {
            return verifiedListingRepository.Find(Id);
        }

        public IEnumerable<VerifiedListing> GetAll()
        {
            return verifiedListingRepository.GetAll();
        }

        public List<VerifiedListing> GetByListingId(decimal ListingId)
        {
            return verifiedListingRepository.GetByListingId(ListingId);
        }

        public VerifiedListing GetListingByService(decimal VerificationId, decimal ListingId)
        {
            return verifiedListingRepository.GetListingByService(VerificationId, ListingId);
        }

        public List<VerifiedListing> GetListingByServiceId(decimal VerificationId)
        {
            return verifiedListingRepository.GetListingByServiceId(VerificationId);
        }
    }
}
