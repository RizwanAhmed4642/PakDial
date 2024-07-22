using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ResponseModels
{
    public class CountryResponse
    {
        public int RowCount { get; set; }
        public IEnumerable<Country> Countries { get; set; }
    }

}
