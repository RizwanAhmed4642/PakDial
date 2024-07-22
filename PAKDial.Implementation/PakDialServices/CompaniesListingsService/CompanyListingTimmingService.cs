using PAKDial.Domains.DomainModels;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.CompaniesListingsService
{
    public class CompanyListingTimmingService : ICompanyListingTimmingService
    {
        private readonly ICompanyListingTimmingRepository companyListingTimmingRepository;
        public CompanyListingTimmingService(ICompanyListingTimmingRepository companyListingTimmingRepository)
        {
            this.companyListingTimmingRepository = companyListingTimmingRepository;
        }
 
        public CompanyListingTimming FindById(decimal Id)
        {
            return companyListingTimmingRepository.Find(Id);
        }

        public IEnumerable<CompanyListingTimming> GetAll()
        {
            return companyListingTimmingRepository.GetAll();
        }

        public List<CompanyListingTimming> GetTimmingByListingId(decimal ListingId)
        {
            return companyListingTimmingRepository.GetTimmingByListingId(ListingId);
        }
    }
}
