using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class ListingAddress
    {
        public decimal Id { get; set; }
        public string BuildingAddress { get; set; }
        public string StreetAddress { get; set; }
        public string LandMark { get; set; }
        public string Area { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string LatLogAddress { get; set; }
        public decimal? CityAreaId { get; set; }
        public decimal CityId { get; set; }
        public decimal StateId { get; set; }
        public decimal CountryId { get; set; }
        public decimal ListingId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public City City { get; set; }
        public CompanyListings Listing { get; set; }
    }
}
