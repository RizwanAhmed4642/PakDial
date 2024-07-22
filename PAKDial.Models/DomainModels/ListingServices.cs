using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class ListingServices
    {
        public decimal Id { get; set; }
        public decimal ListingId { get; set; }
        public decimal ServiceTypeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public CompanyListings Listing { get; set; }
        public TypeOfServices ServiceType { get; set; }
    }
}
