using PAKDial.Domains.SqlViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class VLoadCallCenterPackagesResponse
    {
        public int RowCount { get; set; }

        public IEnumerable<VLoadCallCenterOrdersPackages> LoadCallCenterOrdersPackages { get; set; }
    }
}
