using PAKDial.Domains.DomainModels;
using PAKDial.Domains.SqlViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class MainMenuCategoryResponse
    {
        public int RowCount { get; set; }
        public IEnumerable<VMainCategory> MainMenuCategories { get; set; }
    }
}


