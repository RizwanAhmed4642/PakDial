using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class ListingPaymentsMode
    {
        public decimal Id { get; set; }
        public decimal ListingId { get; set; }
        public decimal ModeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        public CompanyListings Listing { get; set; }
        public PaymentModes Mode { get; set; }
    }
}
