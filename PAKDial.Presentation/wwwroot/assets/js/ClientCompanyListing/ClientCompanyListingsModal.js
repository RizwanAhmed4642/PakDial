function AddUpdateCompanyListings() {
    this.CompanyListings = '',
        this.CompanyListingProfile = '',
        this.ListingAddress = '',
        this.CompanyListingTimming = '',
        this.ListingCategory = '',
        this.ListingGallery = '',
        this.ListingLandlineNo = '',
        this.ListingMobileNo = '',
        this.ListingPaymentsMode = '',
        this.ListingPremium = '',
        this.ListingServices = '',
        this.ListingSocialMedia = '',
        this.ListingsBusinessTypes='',
        this.VerifiedListing = '',
        this.CustomerRegistration = ''
}

function CCustomerRegistration() {
    this.Email = '',
        this.Password = ''
}
function CompanyListings() {
    this.CompanyName = '',
        this.Id = '',
        this.FirstName = '',
        this.LastName = '',
        this.Website = '',
        this.MetaTitle = '',
        this.MetaDescription = '',
        this.MetaKeyword = '',
        this.ListingStatus = '',
        this.BannerImage = '',
        this.BannerImageUrl = '',
        this.CustomerId = '',
        this.ListingTypeId = ''

}

function CompanyListingProfile() {
    this.Id = '',
        this.YearEstablished = '',
        this.AnnualTurnOver = '',
        this.NumberofEmployees = '',
        this.ProfessionalAssociation = '',
        this.Certification = '',
        this.BriefAbout = '',
        this.LocationOverview = '',
        this.ProductAndServices = '',
        this.ListingId = ''
}

function ListingAddress() {
    this.Id = '',
        this.BuildingAddress = '',
        this.StreetAddress = '',
        this.LandMark = '',
        this.Area = '',
        this.Latitude = '',
        this.Longitude = '',
        this.LatLogAddress = '',
        this.CityId = '',
        this.StateId = '',
        this.CityAreaId ='',
        this.CountryId = '',
        this.ListingId = ''
}

function CompanyListingTimming() {
    this.Id = '',
        this.WeekDayNo = '',
        this.DaysName = '',
        this.TimeFrom = '',
        this.TimeTo = '',
        this.IsClosed = '',
        this.ListingId = ''
}

function ListingCategory() {
    this.Id = '',
        this.ListingId = '',
        this.MainCategoryId = '',
        this.SubCategoryId = ''
}

function ListingGallery() {
    this.Id = '',
        this.FileName = '',
        this.UploadDir = '',
        this.FileType = '',
        this.FileUrl = '',
        this.ListingId = ''
}

function ListingLandlineNo() {
    this.Id = '',
        this.LandlineNumber = '',
        this.ListingId = ''
}

function ListingMobileNo() {
    this.Id = '',
        this.MobileNo = '',
        this.ListingId = ''
}

function ListingPaymentsMode() {
    this.Id = '',
        this.ListingId = '',
        this.ModeId = ''
}

function ListingPremium() {
    this.Id = '',
        this.ListingId = '',
        this.PackageId = '',
        this.ModeId = ''
}

function ListingServices() {
    this.Id = '',
        this.ListingId = '',
        this.ServiceTypeId = ''
}

function ListingsBusinessTypes() {
    this.Id = '',
        this.ListingId = '',
        this.BusinessId ='' 
}
function ListingSocialMedia() {
    this.Id = '',
        this.Name = '',
        this.MediaId = '',
        this.SitePath = '',
        this.ListingId = ''
}

function VerifiedListing() {
    this.Id = '',
        this.ListingId = '',
        this.VerificationId = ''

}
function ListingGallery() {
    this.Id = '',
        this.FileName = '',
        this.UploadDir = '',
        this.FileType = '',
        this.FileUrl = '',
        this.ListingId = ''
      
}

var SocialMediaEnum = {
    Facebook:'Facebook',
    Twitter:'Twitter',
    Instagram:'Instagram',
    Youtube:'Youtube',
    Linkedln:'Linkedln'
}
