using PAKDial.Domains.StoreProcedureModel;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels.SalePersonsOrders
{
    public class CallCenterOrderReportResponse
    {
        public CallCenterOrderReportResponse()
        {
            OrderTracking = new List<SpGetSeOrderTracking>();
            EmailPhone = new SpGetEmailAndPhoneByOrderId();
            OrderDetails = new List<SpGetSeOrderDetailByOrderId>();
        }
        public decimal InvoiceNo { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string ChequeNo { get; set; }
        public string PaymentMode { get; set; }
        public string BusinessPersonName { get; set; }
        public string ListedAddress { get; set; }
        public string CreatedDate { get; set; }
        public SpGetEmailAndPhoneByOrderId EmailPhone { get; set; }
        public List<SpGetSeOrderTracking> OrderTracking { get; set; }
        public List<SpGetSeOrderDetailByOrderId> OrderDetails { get; set; }
    }
}
