using PAKDial.Domains.DomainModels;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.CompaniesListingsService
{
    public class ListingPaymentsModeService : IListingPaymentsModeService
    {
        private readonly IListingPaymentsModeRepository listingPaymentsModeRepository;
        public ListingPaymentsModeService(IListingPaymentsModeRepository listingPaymentsModeRepository)
        {
            this.listingPaymentsModeRepository = listingPaymentsModeRepository;
        }
        public ListingPaymentsMode FindById(decimal Id)
        {
            return listingPaymentsModeRepository.Find(Id);
        }

        public IEnumerable<ListingPaymentsMode> GetAll()
        {
            return listingPaymentsModeRepository.GetAll();
        }

        public List<ListingPaymentsMode> GetByListingId(decimal ListingId)
        {
            return listingPaymentsModeRepository.GetByListingId(ListingId);
        }

        public ListingPaymentsMode GetListingPaymentMode(decimal ModeId, decimal ListingId)
        {
            return listingPaymentsModeRepository.GetListingPaymentMode(ModeId,ListingId);
        }

        public List<ListingPaymentsMode> GetListingPaymentModeId(decimal ModeId)
        {
            return listingPaymentsModeRepository.GetListingPaymentModeId(ModeId);
        }
    }
}
