using PAKDial.Domains.SqlViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ResponseModels
{
   public class CustomerResponse
    {
        public int RowCount { get; set; }

        public IEnumerable<VCustomers> Customers { get; set; }
    }
}
