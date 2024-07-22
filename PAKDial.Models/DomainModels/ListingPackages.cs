using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class ListingPackages
    {
        public ListingPackages()
        {
            ListingPremium = new HashSet<ListingPremium>();
            PackagePrices = new HashSet<PackagePrices>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public int? Months { get; set; }

        public ICollection<ListingPremium> ListingPremium { get; set; }
        public ICollection<PackagePrices> PackagePrices { get; set; }
    }
}
