using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.ICompaniesListingsService
{
    public interface IListingServicesService
    {
        ListingServices FindById(decimal Id);
        IEnumerable<ListingServices> GetAll();
        List<ListingServices> GetByListingId(decimal ListingId);
        List<ListingServices> GetListingByServiceId(decimal ServiceTypeId);
        ListingServices GetListingByService(decimal ServiceTypeId, decimal ListingId);
    }
}
