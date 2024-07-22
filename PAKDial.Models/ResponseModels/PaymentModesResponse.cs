using PAKDial.Domains.DomainModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class PaymentModesResponse
    {
        public int RowCount { get; set; }
        public IEnumerable<PaymentModes> paymentModes { get; set; }
    }
}
