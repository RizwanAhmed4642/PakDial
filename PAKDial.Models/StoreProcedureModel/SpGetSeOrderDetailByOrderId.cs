namespace PAKDial.Domains.StoreProcedureModel
{
    public class SpGetSeOrderDetailByOrderId
    {
        public decimal ListingId { get; set; }
        public string CompanyDetail { get; set; }
        public int? Months { get; set; }
        public decimal Price { get; set; }
        public string Discount { get; set; }
        public bool? isDiscount { get; set; }
    }
}
