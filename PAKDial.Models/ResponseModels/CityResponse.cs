using PAKDial.Domains.DomainModels;
using PAKDial.Domains.SqlViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class CityResponse
    {
        public int RowCount { get; set; }

        public IEnumerable<VCity> cities { get; set; }
    }
}
