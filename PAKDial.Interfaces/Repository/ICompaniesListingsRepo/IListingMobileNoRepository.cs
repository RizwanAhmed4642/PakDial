using PAKDial.Domains.DomainModels;
using System.Collections.Generic;

namespace PAKDial.Interfaces.Repository.ICompaniesListingsRepo
{
    public interface IListingMobileNoRepository : IBaseRepository<ListingMobileNo, decimal>
    {
        List<ListingMobileNo> GetByListingId(decimal ListingId);

        List<ListingMobileNo> GetListingMobileNoByName(string MobileNo, int Id);
    }
}
