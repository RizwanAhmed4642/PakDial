using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class PremiumManageStatus
    {
        public decimal Id { get; set; }
        public decimal PremiumId { get; set; }
        public decimal StatusId { get; set; }
        public string RoleId { get; set; }
        public decimal DesignationId { get; set; }
        public bool Deposited { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ListingPremium Premium { get; set; }
    }
}
