using PAKDial.Domains.ViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class GetMainMenuCategoryResponse
    {
        public int PageNo { get; set; }
        public int RowCount { get; set; }
        public IEnumerable<VMGenericKeyValuePair<decimal>> MainMenuCategories { get; set; }
    }
}


