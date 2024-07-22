using PAKDial.Domains.DomainModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class BusinessTypesResponse
    {
        public int RowCount { get; set; }
        public IEnumerable<BusinessTypes> Businesstypes { get; set; }
    }

}
