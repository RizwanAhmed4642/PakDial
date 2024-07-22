using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.SqlViewModels
{
    public class VSubMenuCategory
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string SubBannerImage { get; set; }
        public string SubFeatureImage { get; set; }
        public decimal? SubViewCount { get; set; }
        public decimal? ParentSubCategoryId { get; set; }
        public string ParentCategoryName { get; set; }
        public decimal MainCategoryId { get; set; }
        public string MainCategoryName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsLastNode { get; set; }
        public decimal CategoryTypeId { get; set; }
        public string CategoryTypeName { get; set; }
        public string TrackIds { get; set; }
        public string TrackNames { get; set; }
        public bool? IsPopular { get; set; }

    }


}
