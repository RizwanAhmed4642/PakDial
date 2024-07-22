using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels.Configuration;
using PAKDial.Domains.ResponseModels.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.Configuration
{
    public interface IListingTypesRepository : IBaseRepository<ListingTypes, decimal>
    {
        decimal UpdateListingTypes(ListingTypes Instance);
        decimal AddListingTypes(ListingTypes Instance);
        decimal DeleteListingTypes(decimal Id);
        ListingTypesResponse GetListingTypes(ListingTypesRequestModel request);
        ListingTypes GetByName(string Name);
        bool CheckExistance(ListingTypes instance);
    }
}
