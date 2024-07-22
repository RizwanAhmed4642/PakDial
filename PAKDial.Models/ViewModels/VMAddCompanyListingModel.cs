using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ViewModels
{
    public class VMAddCompanyListingModel
    {
        #region Ctor

        public VMAddCompanyListingModel()
        {
            CompanyListings = new CompanyListings();
            CompanyListingProfile = new CompanyListingProfile();
            CompanyListingTimming = new List<CompanyListingTimming>();
            ListingAddress = new ListingAddress();
            ListingCategory = new List<ListingCategory>();
            ListingGallery = new List<ListingGallery>();
            ListingLandlineNo = new List<ListingLandlineNo>();
            ListingMobileNo = new List<ListingMobileNo>();
            ListingPaymentsMode = new List<ListingPaymentsMode>();
            ListingPremium = new List<ListingPremium>();
            ListingServices = new List<ListingServices>();
            ListingSocialMedia = new List<ListingSocialMedia>();
            VerifiedListing = new List<VerifiedListing>();
            ListingsBusinessTypes = new List<ListingsBusinessTypes>();
            Registration = new AspNetUsers();
            CustomerRegistration = new VMRegistrationCustomer();
            CompanyListingRating = new List<CompanyListingRating>();
        }

        #endregion

        public int Id { get; set; }
        public CompanyListings CompanyListings { get; set; }
        public CompanyListingProfile CompanyListingProfile { get; set; }
        public ListingAddress ListingAddress { get; set; }
        public List<CompanyListingTimming> CompanyListingTimming { get; set; }
        public List<ListingCategory> ListingCategory { get; set; }
        public List<ListingGallery> ListingGallery { get; set; }
        public List<ListingLandlineNo> ListingLandlineNo { get; set; }
        public List<ListingMobileNo> ListingMobileNo { get; set; }
        public List<ListingPaymentsMode> ListingPaymentsMode { get; set; }
        public List<ListingPremium> ListingPremium { get; set; }
        public List<ListingServices> ListingServices { get; set; }
        public List<ListingSocialMedia> ListingSocialMedia { get; set; }
        public List<ListingsBusinessTypes> ListingsBusinessTypes { get; set; }
        public List<VerifiedListing> VerifiedListing { get; set; }
        public List<CompanyListingRating> CompanyListingRating { get; set; }
        public AspNetUsers Registration { get; set; }
        public VMRegistrationCustomer CustomerRegistration { get; set; }
    }
}
