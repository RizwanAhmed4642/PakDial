using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ResponseModels
{
    public class StateCityResponse
    {
        public IEnumerable<StateProvince> StateProvinces { get; set; }
        public IEnumerable<City> Cities { get; set; }
    }
}
