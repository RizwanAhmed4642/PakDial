using PAKDial.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ResponseModels
{
    public class GetPackagesandModesResponse
    {
        public GetPackagesandModesResponse()
        {
            Packages = new List<VMKeyValuePair>();
            PaymentsModes = new List<VMKeyValuePair>();
        }
        public List<VMKeyValuePair> Packages { get; set; }
        public List<VMKeyValuePair> PaymentsModes { get; set; }

    }
}
