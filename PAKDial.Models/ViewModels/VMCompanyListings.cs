using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ViewModels
{
    public class VMCompanyListings
    {
        public VMCompanyListings()
        {
            CompanyListings = new VMCompanyListing();
            CompanyListingProfile = new VMCompanyListingProfile();
            ListingAddress = new VMListingAddress();
            CompanyListingTimming = new  List<VMCompanyListingTimming>();
            ListingCategory = new List<VMListingCategory>();
            ListingGallery = new List<VMListingGallery>();
            ListingLandlineNo = new List<VMListingLandlineNo>();
            ListingMobileNo = new List<VMListingMobileNo>();
            ListingPaymentsMode = new List<VMListingPaymentsMode>();
            ListingPremium = new List<VMListingPremium>();
            ListingServices = new List<VMListingServices>();
            VerifiedListing = new List<VMVerifiedListing>();
            ListingTypes = new VMListingTypes();
            SocialMediaModes = new List<VMSocialMediaModes>();
            ListingsBusinessTypes = new List<VMListingsBusinessTypes>();
        }

        public VMCompanyListing CompanyListings { get; set; }
        public VMCompanyListingProfile CompanyListingProfile { get; set; }
        public VMListingAddress ListingAddress { get; set; }
        public List<VMCompanyListingTimming> CompanyListingTimming { get; set; }
        public List<VMListingCategory> ListingCategory { get; set; }
        public List<VMListingGallery> ListingGallery { get; set; }
        public List<VMListingLandlineNo> ListingLandlineNo { get; set; }
        public List<VMListingMobileNo> ListingMobileNo { get; set; }
        public List<VMListingPaymentsMode> ListingPaymentsMode { get; set; }
        public List<VMListingPremium> ListingPremium { get; set; }
        public List<VMListingServices> ListingServices { get; set; }
        public List<VMVerifiedListing> VerifiedListing { get; set; }
        public VMListingTypes ListingTypes { get; set; }
        public List<VMSocialMediaModes> SocialMediaModes { get; set; }
        public List<VMListingsBusinessTypes> ListingsBusinessTypes { get; set; }
    }

    public class VMCompanyListing
    {
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
    }

    public class VMListingTypes
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }

    }
    public class VMCompanyListingProfile
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
    }
    public class VMCompanyListingRating
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public int MobileNo { get; set; }
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
    }
    public class VMCompanyListingTimming
    {
        public decimal Id { get; set; }
        public int? WeekDayNo { get; set; }
        public string DaysName { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public bool IsClosed { get; set; }
        public decimal ListingId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    public class VMListingAddress
    {
        public decimal Id { get; set; }
        public string BuildingAddress { get; set; }
        public string StreetAddress { get; set; }
        public string LandMark { get; set; }
        public string Area { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string LatLogAddress { get; set; }
        public decimal CityId { get; set; }
        public string  CityName{ get; set; }
        public string CityAreaName { get; set; }
        public decimal StateId { get; set; }
        public decimal CountryId { get; set; }
        public decimal? CityAreaId { get; set; }
        public decimal ListingId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
    public class VMListingCategory
    {
        public decimal Id { get; set; }
        public decimal ListingId { get; set; }
        public decimal MainCategoryId { get; set; }
        public string MainCategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public decimal SubCategoryId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    public class VMListingGallery
    {
        public decimal Id { get; set; }
        public string FileName { get; set; }
        public string UploadDir { get; set; }
        public string FileType { get; set; }
        public string FileUrl { get; set; }
        public decimal ListingId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    public class VMListingLandlineNo
    {
        public decimal Id { get; set; }
        public string LandlineNumber { get; set; }
        public decimal ListingId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    public class VMListingMobileNo
    {
        public decimal Id { get; set; }
        public string MobileNo { get; set; }
        public int? OptCode { get; set; }
        public decimal ListingId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
    public class VMListingPaymentsMode
    {
        public decimal Id { get; set; }
        public decimal ListingId { get; set; }
        public decimal ModeId { get; set; }
        public string ModeName{ get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }

    }

    public class VMListingsBusinessTypes
    {
        public decimal Id { get; set; }
        public decimal ListingId { get; set; }
        public string Text{ get; set; }
        public decimal BusinessId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
    public class VMListingPremium
    {
        public decimal Id { get; set; }
        public decimal ListingId { get; set; }
        public decimal PackageId { get; set; }
        public decimal ModeId { get; set; }
        public DateTime ListingFrom { get; set; }
        public DateTime ListingTo { get; set; }
        public string OtherInfo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
    public class VMListingServices
    {
        public decimal Id { get; set; }
        public decimal ListingId { get; set; }
        public decimal ServiceTypeId { get; set; }
        public string  ServiceName{ get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
    public class VMSocialMediaModes
    {

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string ImageDir { get; set; }
        public decimal MediaId{ get; set; }
        public string ImageUrl { get; set; }
        public string SitePath { get; set; }
        public string Imagedir { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }

    }
    public class VMVerifiedListing
    {
        public decimal Id { get; set; }
        public decimal ListingId { get; set; }
        public decimal VerificationId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }


}
