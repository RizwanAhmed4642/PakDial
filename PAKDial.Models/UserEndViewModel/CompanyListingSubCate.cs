using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.UserEndViewModel
{
   public class CompanyListingSubCate
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public decimal MainCategoryId { get; set; }
        public string MainCategoryName { get; set; }
        public decimal ListingId { get; set; }
        public string Location { get; set; }
    }
}
