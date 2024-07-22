using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.SqlViewModels
{
    public class VHomeListingSearch
    {
        public decimal ListingId { get; set; }
        public decimal CatId { get; set; }
        public decimal SubCatId { get; set; }
        public string CompanyName { get; set; }
        public string BannerImage { get; set; }
        public string ContactNo { get; set; }
        public string ListingAddress { get; set; }
        public string CityArea { get; set; }
        public string CityName { get; set; }
        public string SpaceCityArea { get; set; }
        public string SpaceCityName { get; set; }
        public int AvgRating { get; set; }
        public int TotalVotes { get; set; }
        public bool IsPremium { get; set; }
        public int IsTrusted { get; set; }
    }
}
