using PAKDial.Domains.SqlViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class PackagePricesResponse
    {
        public int RowCount { get; set; }
        public IEnumerable<VPackagePrices> PackagePrices  { get; set; }
    }

}
