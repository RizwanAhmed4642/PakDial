using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.SqlViewModels
{
    public class VLoadHomePopularService
    {
        public decimal HomeSecId { get; set; }
        public string HomeSection { get; set; }
        public bool IsNested { get; set; }
        public decimal CatId { get; set; }
        public string CatName { get; set; }
        public string CatDescription { get; set; }
        public string CatFeatureImage { get; set; }
        public string Web_Dir { get; set; }

    }
}
