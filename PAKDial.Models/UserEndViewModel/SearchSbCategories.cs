using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.UserEndViewModel
{
    public class SearchSbCategories
    {
        public decimal? Id { get; set; }
        public string SbCatName { get; set; }
        public string CtName { get; set; }
        public string ArName { get; set; }
        public decimal? ListingId { get; set; }
        public string CompanyName { get; set; }
        public int AvgRating { get; set; }
    }
}
