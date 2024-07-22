using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class CompanyListingProfile
    {
        public decimal Id { get; set; }
        public int? YearEstablished { get; set; }
        public string AnnualTurnOver { get; set; }
        public string NumberofEmployees { get; set; }
        public string ProfessionalAssociation { get; set; }
        public string Certification { get; set; }
        public string BriefAbout { get; set; }
        public string LocationOverview { get; set; }
        public string ProductAndServices { get; set; }
        public decimal ListingId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public CompanyListings Listing { get; set; }
    }
}
