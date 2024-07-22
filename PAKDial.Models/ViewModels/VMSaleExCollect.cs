using System;

namespace PAKDial.Domains.ViewModels
{
    public class VMSaleExCollect
    {
        public decimal Id { get; set; }
        public string BankName { get; set; }
        public string ChequeNo { get; set; }
        public string PackageName { get; set; }
        public string CombineAreas { get; set; }
        public string CompanyName { get; set; }
        public decimal ModeId { get; set; }
        public string ModeName { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
