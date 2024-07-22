using PAKDial.Domains.DomainModels;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.CompaniesListingsService
{
    public class ListingAddressService : IListingAddressService
    {
        private readonly IListingAddressRepository listingAddressRepository;
        public ListingAddressService(IListingAddressRepository listingAddressRepository)
        {
            this.listingAddressRepository = listingAddressRepository;
        }
      
        public ListingAddress FindById(decimal Id)
        {
            return listingAddressRepository.Find(Id);
        }

        public IEnumerable<ListingAddress> GetAll()
        {
            return listingAddressRepository.GetAll();
        }

        public List<ListingAddress> GetByListingId(decimal ListingId)
        {
            return listingAddressRepository.GetByListingId(ListingId);
        }
    }
}
