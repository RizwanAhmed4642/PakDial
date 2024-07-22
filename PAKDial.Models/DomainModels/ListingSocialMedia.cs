using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class ListingSocialMedia
    {
        public decimal Id { get; set; }
        public decimal MediaId { get; set; }
        public decimal ListingId { get; set; }
        public string SitePath { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        public CompanyListings Listing { get; set; }
        public SocialMediaModes Media { get; set; }
    }
}
