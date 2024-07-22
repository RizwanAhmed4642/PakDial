using PAKDial.Domains.ViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class CityAreabyCityResponse
    {
        public int RowCount { get; set; }

        public IEnumerable<VMKeyValuePair> cityAreas { get; set; }
    }
}
