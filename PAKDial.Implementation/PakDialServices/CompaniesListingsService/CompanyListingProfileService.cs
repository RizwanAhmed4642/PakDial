using PAKDial.Domains.DomainModels;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.CompaniesListingsService
{
    public class CompanyListingProfileService : ICompanyListingProfileService
    {
        private readonly ICompanyListingProfileRepository companyListingProfileRepository;
        public CompanyListingProfileService(ICompanyListingProfileRepository companyListingProfileRepository)
        {
            this.companyListingProfileRepository = companyListingProfileRepository;
        }
        public CompanyListingProfile FindById(decimal Id)
        {
            return companyListingProfileRepository.Find(Id);
        }

        public IEnumerable<CompanyListingProfile> GetAll()
        {
            return companyListingProfileRepository.GetAll();
        }

        public CompanyListingProfile GetByListingId(decimal ListingId)
        {
            return companyListingProfileRepository.GetByListingId(ListingId);
        }
    }
}
