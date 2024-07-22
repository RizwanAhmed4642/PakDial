using System;

namespace PAKDial.Domains.SqlViewModels
{
    public class VCity
    {
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
        public string StateName { get; set; }
        public string CountryName { get; set; }
    }
}
