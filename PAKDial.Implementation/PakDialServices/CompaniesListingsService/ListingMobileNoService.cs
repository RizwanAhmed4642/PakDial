using PAKDial.Domains.DomainModels;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using System.Collections.Generic;

namespace PAKDial.Implementation.PakDialServices.CompaniesListingsService
{
    public class ListingMobileNoService : IListingMobileNoService
    {
        private readonly IListingMobileNoRepository listingMobileNoRepository;
        public ListingMobileNoService(IListingMobileNoRepository listingMobileNoRepository)
        {
            this.listingMobileNoRepository = listingMobileNoRepository;
        }
        public ListingMobileNo FindById(decimal Id)
        {
            return listingMobileNoRepository.Find(Id);
        }

        public IEnumerable<ListingMobileNo> GetAll()
        {
            return listingMobileNoRepository.GetAll();
        }

        public List<ListingMobileNo> GetByListingId(decimal ListingId)
        {
            return listingMobileNoRepository.GetByListingId(ListingId);
        }
    }
}
