using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.UserEndViewModel
{
    public class ListingQueryRequest
    {
        public string  RequestName { get; set; }
        public string  RequestMobNo { get; set; }
        public decimal SubCatId { get; set; }
        public string SubCatName { get; set; }
        public string  AreaName { get; set; }
        public decimal ListingId { get; set; }
        public string CityName { get; set; }

        public string ProductName { get; set; }

    }
}
