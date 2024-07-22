using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class HomeSectionCategory
    {
        public HomeSectionCategory()
        {
            HomeSecMainMenuCat = new HashSet<HomeSecMainMenuCat>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? OrderByNo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool? IsNested { get; set; }

        public ICollection<HomeSecMainMenuCat> HomeSecMainMenuCat { get; set; }
    }
}
