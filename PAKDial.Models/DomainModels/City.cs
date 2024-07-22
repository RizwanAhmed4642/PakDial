using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class City
    {
        public City()
        {
            CityArea = new HashSet<CityArea>();
            ListingAddress = new HashSet<ListingAddress>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string CityLat { get; set; }
        public string CityLog { get; set; }
        public int? CityPopular { get; set; }
        public int? CityStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public decimal StateId { get; set; }
        public decimal? CountryId { get; set; }

        public StateProvince State { get; set; }
        public ICollection<CityArea> CityArea { get; set; }
        public ICollection<ListingAddress> ListingAddress { get; set; }
    }
}
