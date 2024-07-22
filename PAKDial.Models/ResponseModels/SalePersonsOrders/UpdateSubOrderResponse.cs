namespace PAKDial.Domains.ResponseModels.SalePersonsOrders
{
    public class UpdateSubOrderResponse
    {
        public UpdateSubOrderResponse()
        {
            InvoiceNo = 0;
        }
        public decimal InvoiceNo { get; set; }
        public string ProcessMessage { get; set; }

        public string CompanyName { get; set; }
        public string CompanyMobileNo { get; set; }
    }
}
