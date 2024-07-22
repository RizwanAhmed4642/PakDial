using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ResponseModels
{
    public class ModulesResponse
    {
        public int RowCount { get; set; }

        public IEnumerable<Modules> Modules { get; set; }
    }
}
