using PAKDial.Domains.SqlViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class ListingPackagesResponse
    {
        public int RowCount { get; set; }
        public IEnumerable<VListingPackages> ListingPackages { get; set; }
    }

}
