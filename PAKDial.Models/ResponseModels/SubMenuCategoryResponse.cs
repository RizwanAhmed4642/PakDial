using PAKDial.Domains.SqlViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class SubMenuCategoryResponse
    {
        public int RowCount { get; set; }
        public IEnumerable<VSubMenuCategory> SubMenuCategories { get; set; }
    }

}
