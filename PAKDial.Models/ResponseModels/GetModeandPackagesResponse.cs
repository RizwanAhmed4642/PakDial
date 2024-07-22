using Microsoft.AspNetCore.Mvc.Rendering;
using PAKDial.Domains.ViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class GetModeandPackagesResponse
    {
        public GetModeandPackagesResponse()
        {
            PaymentModeList = new List<VMGenericKeyValuePair<decimal>>();
            PackagesList = new List<VMGenericKeyValuePair<decimal>>();
        }
        public List<VMGenericKeyValuePair<decimal>> PaymentModeList { get; set; }
        public List<VMGenericKeyValuePair<decimal>> PackagesList { get; set; }
    }
}
