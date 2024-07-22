using System;

namespace PAKDial.Domains.ViewModels
{
    public class VMCallCenterExCreate
    {
        public decimal Id { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string ChequeNo { get; set; }
        public decimal ListingId { get; set; }
        public decimal PackageId { get; set; }
        public decimal ModeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Discount { get; set; }
        public string DiscountType { get; set; }
        public Boolean? IsDiscount { get; set; }
        
        
    }
}
