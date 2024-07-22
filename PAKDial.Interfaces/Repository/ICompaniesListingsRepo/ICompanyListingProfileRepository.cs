using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.ICompaniesListingsRepo
{
    public interface ICompanyListingProfileRepository : IBaseRepository<CompanyListingProfile,decimal>
    {
        CompanyListingProfile GetByListingId(decimal ListingId);
    }
}
