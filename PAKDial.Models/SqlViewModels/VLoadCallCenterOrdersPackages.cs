using System;

namespace PAKDial.Domains.SqlViewModels
{
    public class VLoadCallCenterOrdersPackages
    {
        public decimal Id { get; set; }
        public string SalePersonEmail { get; set; }
        public string BankName { get; set; }
        public string Account_No { get; set; }
        public string ChequeNo { get; set; }
        public string ListedAddress { get; set; }
        public decimal ListingId { get; set; }
        public string CompanyName { get; set; }
        public string FullName { get; set; }
        public decimal PackageId { get; set; }
        public string PackageName { get; set; }
        public decimal ModeId { get; set; }
        public string ModeName { get; set; }
        public DateTime? ListingFrom { get; set; }
        public DateTime? ListingTo { get; set; }
        public bool IsActive { get; set; }
        public bool Deposited { get; set; }
        public decimal StatusId { get; set; }
        public string StatusName { get; set; }
        public decimal Price { get; set; }
        public decimal CityAreaId { get; set; }
        public string CreatedBy { get; set; }
        public string AssignedTo { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
