using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ResponseModels.Configuration
{
  public  class TypeOfServicesResponse
    {
        public int RowCount { get; set; }
        public IEnumerable<TypeOfServices> TypeOfServices { get; set; }
        //TypeOfServices
    }
}
