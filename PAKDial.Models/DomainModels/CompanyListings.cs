using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class CompanyListings
    {
        public CompanyListings()
        {
            CompanyListingProfile = new HashSet<CompanyListingProfile>();
            CompanyListingRating = new HashSet<CompanyListingRating>();
            CompanyListingTimming = new HashSet<CompanyListingTimming>();
            ListingAddress = new HashSet<ListingAddress>();
            ListingCategory = new HashSet<ListingCategory>();
            ListingGallery = new HashSet<ListingGallery>();
            ListingLandlineNo = new HashSet<ListingLandlineNo>();
            ListingMobileNo = new HashSet<ListingMobileNo>();
            ListingPaymentsMode = new HashSet<ListingPaymentsMode>();
            ListingPremium = new HashSet<ListingPremium>();
            ListingServices = new HashSet<ListingServices>();
            ListingSocialMedia = new HashSet<ListingSocialMedia>();
            ListingsBusinessTypes = new HashSet<ListingsBusinessTypes>();
            VerifiedListing = new HashSet<VerifiedListing>();
        }

        public decimal Id { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public bool ListingStatus { get; set; }
        public string BannerImage { get; set; }
        public string BannerImageUrl { get; set; }
        public decimal CustomerId { get; set; }
        public decimal ListingTypeId { get; set; }
        public int? OtpCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public decimal? RequestCounter { get; set; }

        public ListingTypes ListingType { get; set; }
        public ICollection<CompanyListingProfile> CompanyListingProfile { get; set; }
        public ICollection<CompanyListingRating> CompanyListingRating { get; set; }
        public ICollection<CompanyListingTimming> CompanyListingTimming { get; set; }
        public ICollection<ListingAddress> ListingAddress { get; set; }
        public ICollection<ListingCategory> ListingCategory { get; set; }
        public ICollection<ListingGallery> ListingGallery { get; set; }
        public ICollection<ListingLandlineNo> ListingLandlineNo { get; set; }
        public ICollection<ListingMobileNo> ListingMobileNo { get; set; }
        public ICollection<ListingPaymentsMode> ListingPaymentsMode { get; set; }
        public ICollection<ListingPremium> ListingPremium { get; set; }
        public ICollection<ListingServices> ListingServices { get; set; }
        public ICollection<ListingSocialMedia> ListingSocialMedia { get; set; }
        public ICollection<ListingsBusinessTypes> ListingsBusinessTypes { get; set; }
        public ICollection<VerifiedListing> VerifiedListing { get; set; }
    }
}
