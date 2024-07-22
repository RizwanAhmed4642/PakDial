using PAKDial.Domains.DomainModels;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.CompaniesListingsService
{
    public class ListingServicesService : IListingServicesService
    {
        private readonly IListingServicesRepository listingServicesRepository;
        public ListingServicesService(IListingServicesRepository listingServicesRepository)
        {
            this.listingServicesRepository = listingServicesRepository;
        }
        public ListingServices FindById(decimal Id)
        {
            return listingServicesRepository.Find(Id);
        }

        public IEnumerable<ListingServices> GetAll()
        {
            return listingServicesRepository.GetAll();
        }

        public List<ListingServices> GetByListingId(decimal ListingId)
        {
            return listingServicesRepository.GetByListingId(ListingId);
        }

        public ListingServices GetListingByService(decimal ServiceTypeId, decimal ListingId)
        {
            return listingServicesRepository.GetListingByService(ServiceTypeId, ListingId);
        }

        public List<ListingServices> GetListingByServiceId(decimal ServiceTypeId)
        {
            return listingServicesRepository.GetListingByServiceId(ServiceTypeId);
        }
    }
}
