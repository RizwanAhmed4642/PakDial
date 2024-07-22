using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ViewModels
{
    public class VCatAndCompanyListingsSearch
    {
        public VCatAndCompanyListingsSearch()
        {
            CatId = 0;
            ListingId = 0;
            CityAreaId = 0;
            CityId = 0;
            AvgRating = 0;
        }
        public decimal CatId { get; set; }
        public string CatName { get; set; }
        public decimal ListingId { get; set; }
        public string CompanyName { get; set; }
        public string ListingType { get; set; }
        public decimal CityAreaId { get; set; }
        public string CityAreaName { get; set; }
        public decimal CityId { get; set; }
        public string CityName { get; set; }
        public int AvgRating { get; set; }
    }
}
