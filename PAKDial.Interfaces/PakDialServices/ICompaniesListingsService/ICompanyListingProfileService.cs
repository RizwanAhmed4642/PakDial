using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.ICompaniesListingsService
{
    public interface ICompanyListingProfileService
    {
        CompanyListingProfile FindById(decimal Id);
        IEnumerable<CompanyListingProfile> GetAll();
        CompanyListingProfile GetByListingId(decimal ListingId);
    }
}
