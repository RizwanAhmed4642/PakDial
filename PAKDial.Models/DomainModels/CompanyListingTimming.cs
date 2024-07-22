using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class CompanyListingTimming
    {
        public decimal Id { get; set; }
        public int? WeekDayNo { get; set; }
        public string DaysName { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public bool IsClosed { get; set; }
        public decimal ListingId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public CompanyListings Listing { get; set; }
    }
}
