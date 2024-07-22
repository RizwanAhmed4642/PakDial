using PAKDial.Domains.DomainModels;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.CompaniesListingsService
{
    public class ListingLandlineNoService : IListingLandlineNoService
    {
        private readonly IListingLandlineNoRepository listingLandlineNoRepository;
        public ListingLandlineNoService(IListingLandlineNoRepository listingLandlineNoRepository)
        {
            this.listingLandlineNoRepository = listingLandlineNoRepository;
        }
        public IEnumerable<ListingLandlineNo> GetAll()
        {
            return listingLandlineNoRepository.GetAll();
        }

        public ListingLandlineNo FindById(decimal Id)
        {
            return listingLandlineNoRepository.Find(Id);
        }

        public List<ListingLandlineNo> GetByListingId(decimal ListingId)
        {
            return listingLandlineNoRepository.GetByListingId(ListingId);
        }
    }
}
