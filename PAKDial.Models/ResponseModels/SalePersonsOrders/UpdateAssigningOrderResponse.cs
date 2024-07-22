namespace PAKDial.Domains.ResponseModels.SalePersonsOrders
{
    public class UpdateAssigningOrderResponse
    {
        public UpdateAssigningOrderResponse()
        {
            InvoiceNo = 0;
        }
        public decimal InvoiceNo { get; set; }
        public string ProcessMessage { get; set; }
    }
}
