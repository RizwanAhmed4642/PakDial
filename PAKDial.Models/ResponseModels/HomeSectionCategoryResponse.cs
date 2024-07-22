using PAKDial.Domains.DomainModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class HomeSectionCategoryResponse
    {
        public int RowCount { get; set; }
        public IEnumerable<HomeSectionCategory> HomeSectionCategories { get; set; }
    }
}


