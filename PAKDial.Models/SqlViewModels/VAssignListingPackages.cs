using System;

namespace PAKDial.Domains.SqlViewModels
{
    public class VAssignListingPackages
    {
        public decimal Id { get; set; }
        public string CompanyName { get; set; }
        public string PackageName { get; set; }
        public string StatusName { get; set; }
        public string ModeName { get; set; }
        public decimal Price { get; set; }
        public DateTime? ListingFrom { get; set; }
        public DateTime? ListingTo { get; set; }
        public bool IsActive { get; set; }
        public decimal ListingId { get; set; }
        public decimal CustomerId { get; set; }
    }
}
