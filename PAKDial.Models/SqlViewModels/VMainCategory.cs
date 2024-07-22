using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.SqlViewModels
{
    public class VMainCategory
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string WebDir { get; set; }
        public string MobileDir { get; set; }
        public string CatBannerImage { get; set; }
        public string CatFeatureImage { get; set; }
        public decimal? CatViewCounts { get; set; }
        public int? OrderByNo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public decimal CategoryTypeId { get; set; }
        public string CategoryTypeName { get; set; }
    }
}
