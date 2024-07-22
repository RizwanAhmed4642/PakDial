using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.ICompaniesListingsRepo
{
    public interface ICompanyListingTimmingRepository : IBaseRepository<CompanyListingTimming,decimal>
    {
        List<CompanyListingTimming> GetTimmingByListingId(decimal ListingId);
        PaymentModes GetPaymentModes(decimal ModId);

    }
}
