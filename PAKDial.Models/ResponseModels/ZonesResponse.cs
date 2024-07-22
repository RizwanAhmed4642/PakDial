using PAKDial.Domains.DomainModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class ZonesResponse
    {
        public int RowCount { get; set; }
        public IEnumerable<Zones> Zones { get; set; }
    }
}
