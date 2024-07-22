using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class SubMenuCategory
    {
        public SubMenuCategory()
        {
            ListingCategory = new HashSet<ListingCategory>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string WebDir { get; set; }
        public string WebUrl { get; set; }
        public string MobileDir { get; set; }
        public string MobileUrl { get; set; }
        public string SubBannerImage { get; set; }
        public string SubFeatureImage { get; set; }
        public decimal? SubViewCount { get; set; }
        public decimal? ParentSubCategoryId { get; set; }
        public decimal MainCategoryId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsLastNode { get; set; }
        public string TrackIds { get; set; }
        public string TrackNames { get; set; }
        public decimal CategoryTypeId { get; set; }
        public string SubBannerImageUrl { get; set; }
        public string SubFeatureImageUrl { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public bool? IsPopular { get; set; }

        public MainMenuCategory MainCategory { get; set; }
        public ICollection<ListingCategory> ListingCategory { get; set; }
    }
}
