using PAKDial.Domains.DomainModels;
using PAKDial.Domains.SqlViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ResponseModels.CompanyListing
{
  
    public class CompanyListingsResponse
    {
        public int RowCount { get; set; }

       public IEnumerable<VCompanyListings> CompanyListings { get; set; }
    }
}
