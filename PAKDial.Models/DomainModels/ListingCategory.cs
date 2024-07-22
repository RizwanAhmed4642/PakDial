using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class ListingCategory
    {
        public decimal Id { get; set; }
        public decimal ListingId { get; set; }
        public decimal MainCategoryId { get; set; }
        public decimal SubCategoryId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public CompanyListings Listing { get; set; }
        public SubMenuCategory SubCategory { get; set; }
    }
}
