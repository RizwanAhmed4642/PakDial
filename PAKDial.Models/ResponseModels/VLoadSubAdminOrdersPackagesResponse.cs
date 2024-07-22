using PAKDial.Domains.SqlViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class VLoadSubAdminOrdersPackagesResponse
    {
        public int RowCount { get; set; }

        public IEnumerable<VLoadSubAdminOrdersPackages> LoadSubAdminOrdersPackages { get; set; }
    }
}
