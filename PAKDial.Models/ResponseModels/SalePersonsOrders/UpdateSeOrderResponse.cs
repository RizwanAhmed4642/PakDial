namespace PAKDial.Domains.ResponseModels.SalePersonsOrders
{
    public class UpdateSeOrderResponse
    {
        public UpdateSeOrderResponse()
        {
            InvoiceNo = 0;
        }
        public decimal InvoiceNo { get; set; }
        public string BankName { get; set; }
        public string ChequeNo { get; set; }
        public string PaymentMode { get; set; }
        public string AssignedPackage { get; set; }
        public int? PackageMonths { get; set; }
        public decimal? PackageCost { get; set; }
        public string ProcessMessage { get; set; }

        public decimal ListingId { get; set; }
        public string CompanyName { get; set; }
        public string SalePersonNo { get; set; }
        public string CompanyMobileNo { get; set; }
    }
}
