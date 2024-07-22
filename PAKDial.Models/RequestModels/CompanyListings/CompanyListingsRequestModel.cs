using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.RequestModels.CompanyListings
{
    public class CompanyListingsRequestModel : GetPagedListRequest
    {
        public CompanyListingsRequestModel()
        {
            CityAreaIds = new List<decimal>();
        }
        public string UserId { get; set; }

        public List<decimal> CityAreaIds { get; set; }
        public decimal CustomerId { get; set; }
    }
}
