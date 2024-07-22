using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels.Configuration;
using PAKDial.Domains.ResponseModels.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices
{
   public interface IListingTypesService
    {

        decimal Update(ListingTypes Instance);
        decimal Add(ListingTypes Instance);
        decimal Delete(decimal Id);
        ListingTypes GetById(decimal Id);
        ListingTypes GetByName(string Name);
        IEnumerable<ListingTypes> GetAll();
        ListingTypesResponse GetListingTypes(ListingTypesRequestModel request);
    }
}
