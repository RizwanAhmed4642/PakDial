using PAKDial.Domains.DomainModels;
using PAKDial.Domains.UserEndViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.ICompaniesListingsRepo
{
    public interface ICompanyListingRatingRepository : IBaseRepository<CompanyListingRating,decimal>
    {
        List<CompanyListingRating> FindByListingId(decimal ListingId);
        List<CompanyListingRating> GetVerifiedRating(bool IsVerified);
        List<CompanyListingRating> GetVerifiedRating(bool IsVerified, decimal ListingId);
        CompanyListingRating FindByListingId(decimal ListingId, string MobileNo);
        ListingRatingWrapperModel FindByListingIdFront(decimal ListingId);
        ListingRatingWrapperModel FindByListingIdFront(decimal ListingId, int IncrementalCount);
        AddListingRatingWrapperModel CompanyPostRating(CompanyListingRating rating);
        AddListingRatingWrapperModel PostRatingOtp(CompanyRatingsOTP ratingsOTP);
        void AutoDeleteUnVerfiedRating();

    }
}
