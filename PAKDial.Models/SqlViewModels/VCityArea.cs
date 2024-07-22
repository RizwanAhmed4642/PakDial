using System;

namespace PAKDial.Domains.SqlViewModels
{
    public class VCityArea
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string GoogleLoc { get; set; }
        public string AreaLat { get; set; }
        public string AreaLong { get; set; }
        public int? AreaPopular { get; set; }
        public int? AreaStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public decimal? CityId { get; set; }
        public decimal CountryId { get; set; }
        public decimal? StateId { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }


    }
}
