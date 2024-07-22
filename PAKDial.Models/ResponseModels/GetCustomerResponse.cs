using PAKDial.Domains.SqlViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ResponseModels
{
   public class GetCustomerResponse
    {
        public GetCustomerResponse()
        {
            Customers = new VCustomers ();
        }
        public VCustomers Customers { get; set; }
    }
}
