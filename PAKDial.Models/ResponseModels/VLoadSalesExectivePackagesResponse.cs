using PAKDial.Domains.SqlViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class VLoadSalesExectivePackagesResponse
    {
        public int RowCount { get; set; }

        public IEnumerable<VLoadSalesExectivePackages> Vloadsalesexectivepackages { get; set; }
    }
}
