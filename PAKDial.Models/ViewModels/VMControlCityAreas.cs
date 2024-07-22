using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ViewModels
{
    public class VMControlCityAreas
    {
        public VMControlCityAreas()
        {
            CityAreaKeyValue = new List<VMGenericKeyValuePair<decimal>>();
        }
        public decimal? CityId { get; set; }
        public decimal? StateId { get; set; }
        public List<VMGenericKeyValuePair<decimal>> CityAreaKeyValue { get; set; }
    }
}
