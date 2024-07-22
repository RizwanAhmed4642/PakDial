using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.SqlViewModels
{
    public class VCompanyListings
    {
        public decimal Id { get; set; }
        public string CompanyName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ListingType { get; set; }
        public bool ListingStatus { get; set; }
        public int VerifiedList { get; set; }
        public decimal CityAreaId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool DefaultCustomer { get; set; }
        public decimal CustomerId { get; set; }
        public decimal? RequestCounter { get; set; }
        public string MobileNumber { get; set; }
    }
}
