using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.StoreProcedureModel
{
  public class SpGetGeneralResultantProc
    {

        public SpGetGeneralResultantProc()
        {
            TotalEmployeeCount = 0;
            TotalCustomerCount = 0;
            TotalListingCount = 0;
            
        }
        public int TotalEmployeeCount { get; set; }

        public int TotalCustomerCount { get; set; }

        public int TotalListingCount { get; set; }

    }
}
