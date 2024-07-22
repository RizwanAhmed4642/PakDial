using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class PackagePrices
    {
        public decimal Id { get; set; }
        public decimal? Price { get; set; }
        public decimal ListingPackageId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }

        public ListingPackages ListingPackage { get; set; }
    }
}
