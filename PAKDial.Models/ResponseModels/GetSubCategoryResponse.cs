using PAKDial.Domains.ViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class GetSubCategoryResponse
    {
        public int PageNo { get; set; }
        public int RowCount { get; set; }
        public IEnumerable<VMSubCategoryValuePair> SubCategoy { get; set; }
    }

}
