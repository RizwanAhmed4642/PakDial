using PAKDial.Domains.DomainModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class OutSourceAdvertismentResponse
    {
        public int RowCount { get; set; }
        public IEnumerable<OutSourceAdvertisment> outSourceAdvertisments { get; set; }
    }
}


