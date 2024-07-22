using PAKDial.Domains.SqlViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class VLoadZoneManagerTransferResponse
    {
        public int RowCount { get; set; }
        public IEnumerable<VLoadZoneManagerOrdersTransfer> vLoadZoneManagers { get; set; }
    }
}
