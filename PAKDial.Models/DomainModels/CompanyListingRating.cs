using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class CompanyListingRating
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public int? OptCode { get; set; }
        public decimal ListingId { get; set; }
        public int? Rating { get; set; }
        public string RatingDesc { get; set; }
        public string ImageRatingDir { get; set; }
        public string ImageRatingUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsApproved { get; set; }
        public bool IsVerified { get; set; }
        public string EmailAddress { get; set; }
        public int? TotalAttempts { get; set; }

        public CompanyListings Listing { get; set; }
    }
}
