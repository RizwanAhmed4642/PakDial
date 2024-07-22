using PAKDial.Domains.SqlViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class VLoadTellerPackagesResponse
    {
        public int RowCount { get; set; }

        public IEnumerable<VLoadTellerOrdersDeposited> VloadTellerPackages { get; set; }
    }
}
