using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.UserEndViewModel
{
    public class GenericLoadSbCategoryModel
    {
        public string Location { get; set; }
        public decimal CatId { get; set; }
        public string CatName { get; set; }
        public decimal SubCatId { get; set; }
        public string SubCatName { get; set; }
    }
}
