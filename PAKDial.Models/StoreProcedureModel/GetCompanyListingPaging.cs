using PAKDial.Domains.SqlViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.StoreProcedureModel
{
    public class GetCompanyListingPaging
    {
        public GetCompanyListingPaging()
        {
            HomeListingSearch = new List<VHomeListingSearch>();
        }
        public int RowCount { get; set; }
        public List<VHomeListingSearch> HomeListingSearch { get; set; }
    }
}
