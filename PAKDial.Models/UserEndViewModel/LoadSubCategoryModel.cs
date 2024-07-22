using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.UserEndViewModel
{
    public class LoadSubCategoryModel
    {
        public decimal SubCatId { get; set; }
        public string Name { get; set; }
        public string Web_Url { get; set; }
        public decimal? ParentSubCategoryId { get; set; }
        public decimal MainCategoryId { get; set; }
        public bool IsLastNode { get; set; }
        public bool? IsPopular { get; set; }

    }
}
