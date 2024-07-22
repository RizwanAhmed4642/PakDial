using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.Mapper.CompanyListingMappers
{
  public static class ListingAddressMapper
    {
        #region ListingAddress

        public static ListingAddress MapListingAddress(ListingAddress source, ListingAddress dest)
        {
            dest.Id = source.Id;
            dest.BuildingAddress = source.BuildingAddress;
            dest.StreetAddress = source.StreetAddress;
            dest.LandMark = source.LandMark;
            dest.Latitude = source.Latitude;
            dest.Longitude = source.Longitude;
            dest.LatLogAddress = source.LatLogAddress;
            dest.CityId = source.CityId;
            dest.StateId = source.StateId;
            dest.CityAreaId = source.CityAreaId;
            dest.CountryId = source.CountryId;
            dest.UpdateBy = source.UpdateBy;
            dest.UpdatedDate = source.UpdatedDate;
            return dest;
        }

        #endregion
    }
}
