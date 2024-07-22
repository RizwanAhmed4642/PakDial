namespace PAKDial.Domains.ResponseModels.SalePersonsOrders
{
    public class UpdateTellerOrderResponse
    {
        public UpdateTellerOrderResponse()
        {
            InvoiceNo = 0;
        }
        public decimal InvoiceNo { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string ChequeNo { get; set; }
        public string PaymentMode { get; set; }
        public string AssignedPackage { get; set; }
        public int? PackageMonths { get; set; }
        public decimal? PackageCost { get; set; }
        public string ProcessMessage { get; set; }
    }
}
