using System;

namespace PAKDial.Domains.ViewModels
{
    public class VMZoneManagerTransfer
    {
        public decimal Id { get; set; }
        public string BankName { get; set; }
        public string ChequeNo { get; set; }
        public string PackageName { get; set; }
        public string ModeName { get; set; }
        public string CombineAreas { get; set; }
        public string CompanyName { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedFrom { get; set; }
        public DateTime AssignedDate { get; set; }
        public string Notes { get; set; }
    }
}
