using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.ICompaniesListingsService
{
    public interface ICompanyListingTimmingService
    {
        CompanyListingTimming FindById(decimal Id);
        IEnumerable<CompanyListingTimming> GetAll();
        List<CompanyListingTimming> GetTimmingByListingId(decimal ListingId);
    }
}
