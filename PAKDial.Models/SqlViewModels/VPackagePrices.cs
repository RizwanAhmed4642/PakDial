using System;

namespace PAKDial.Domains.SqlViewModels
{
    public class VPackagePrices
    {
        public decimal Id { get; set; }
        public decimal PriceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool PriceActive { get; set; }
        public int? Months { get; set; }
    }
}
