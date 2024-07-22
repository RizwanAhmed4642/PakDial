using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class ListingPremium
    {
        public ListingPremium()
        {
            PackageTransfer = new HashSet<PackageTransfer>();
            PremiumManageStatus = new HashSet<PremiumManageStatus>();
        }

        public decimal Id { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string ChequeNo { get; set; }
        public decimal ListingId { get; set; }
        public decimal PackageId { get; set; }
        public decimal ModeId { get; set; }
        public DateTime? ListingFrom { get; set; }
        public DateTime? ListingTo { get; set; }
        public bool IsActive { get; set; }
        public decimal StatusId { get; set; }
        public bool Deposited { get; set; }
        public decimal Price { get; set; }
        public string Discount { get; set; }
        public bool? IsDiscount { get; set; }

        public CompanyListings Listing { get; set; }
        public PaymentModes Mode { get; set; }
        public ListingPackages Package { get; set; }
        public PremiumStatus Status { get; set; }
        public ICollection<PackageTransfer> PackageTransfer { get; set; }
        public ICollection<PremiumManageStatus> PremiumManageStatus { get; set; }
    }
}
