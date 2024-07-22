using PAKDial.Domains.SqlViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class AssignListingPackageResponse
    {
        public int RowCount { get; set; }

        public IEnumerable<VAssignListingPackages> AssignListingPackages { get; set; }
    }
}
