using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class PaymentModes
    {
        public PaymentModes()
        {
            ListingPaymentsMode = new HashSet<ListingPaymentsMode>();
            ListingPremium = new HashSet<ListingPremium>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageDir { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }

        public ICollection<ListingPaymentsMode> ListingPaymentsMode { get; set; }
        public ICollection<ListingPremium> ListingPremium { get; set; }
    }
}
