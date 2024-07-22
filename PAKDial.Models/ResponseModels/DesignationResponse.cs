using PAKDial.Domains.DomainModels;
using PAKDial.Domains.SqlViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class DesignationResponse
    {
        public int RowCount { get; set; }

        public IEnumerable<VDesignation> designations { get; set; }
    }
}
