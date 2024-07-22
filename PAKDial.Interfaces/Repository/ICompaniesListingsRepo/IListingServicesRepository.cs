using PAKDial.Domains.DomainModels;
using System.Collections.Generic;

namespace PAKDial.Interfaces.Repository.ICompaniesListingsRepo
{
    public interface IListingServicesRepository : IBaseRepository<ListingServices, decimal>
    {
        List<ListingServices> GetByListingId(decimal ListingId);
        List<ListingServices> GetListingByServiceId(decimal ServiceTypeId);
        ListingServices GetListingByService(decimal ServiceTypeId, decimal ListingId);
    }
}
