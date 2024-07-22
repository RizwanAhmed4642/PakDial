using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ViewModels
{
    public class ListingPackageViewModel
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Months { get; set; }
        public decimal PriceId { get; set; }
        public decimal? ActivePrice { get; set; }
        public decimal? NewPrice { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
