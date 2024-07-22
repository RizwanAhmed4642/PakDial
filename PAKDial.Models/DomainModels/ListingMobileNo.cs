using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class ListingMobileNo
    {
        public decimal Id { get; set; }
        public string MobileNo { get; set; }
        public int? OptCode { get; set; }
        public decimal ListingId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public CompanyListings Listing { get; set; }
    }
}
