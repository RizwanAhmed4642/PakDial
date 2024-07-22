﻿using PAKDial.Domains.DomainModels;
using PAKDial.Domains.SqlViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class CityAreaResponse
    {
        public int RowCount { get; set; }

        public IEnumerable<VCityArea> cityAreas { get; set; }
    }
}