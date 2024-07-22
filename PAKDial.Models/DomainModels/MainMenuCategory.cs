using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class MainMenuCategory
    {
        public MainMenuCategory()
        {
            HomeSecMainMenuCat = new HashSet<HomeSecMainMenuCat>();
            SubMenuCategory = new HashSet<SubMenuCategory>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string WebDir { get; set; }
        public string WebUrl { get; set; }
        public string MobileDir { get; set; }
        public string MobileUrl { get; set; }
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
        public string CatBannerImageUrl { get; set; }
        public string CatFeatureImageUrl { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }

        public CategoryTypes CategoryType { get; set; }
        public ICollection<HomeSecMainMenuCat> HomeSecMainMenuCat { get; set; }
        public ICollection<SubMenuCategory> SubMenuCategory { get; set; }
    }
}
