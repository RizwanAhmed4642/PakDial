using PAKDial.Domains.ViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class GetSubMenuCategoryResponse
    {
        public int PageNo { get; set; }
        public int RowCount { get; set; }
        public IEnumerable<VMGenericKeyValuePair<decimal>> SubMenuCategoryList { get; set; }
    }
}
