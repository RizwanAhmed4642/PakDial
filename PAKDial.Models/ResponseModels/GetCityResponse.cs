using PAKDial.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ResponseModels
{
    public class GetCityResponse
    {
        public int PageNo { get; set; }
        public int RowCount { get; set; }
        public IEnumerable<VMGenericKeyValuePair<decimal>> CityList { get; set; }
    }
}
