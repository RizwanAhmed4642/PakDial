using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ResponseModels.Configuration
{
   public class VerificationTypesResponse
    {
        public int RowCount { get; set; }
        public IEnumerable<VerificationTypes> VerificationTypes { get; set; }

    }
}
