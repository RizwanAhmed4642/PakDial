using PAKDial.Domains.SqlViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class ActiveZonesResponse
    {
        public int RowCount { get; set; }
        public IEnumerable<VActiveZones> VActiveZones { get; set; }
    }

}
