using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class PackageTransfer
    {
        public decimal Id { get; set; }
        public decimal PremiumId { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedFrom { get; set; }
        public DateTime AssignedDate { get; set; }
        public string Notes { get; set; }

        public ListingPremium Premium { get; set; }
    }
}
