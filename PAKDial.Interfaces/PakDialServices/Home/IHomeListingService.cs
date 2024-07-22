using PAKDial.Domains.DomainModels;
using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Domains.StoreProcedureModel.Home;
using PAKDial.Domains.UserEndViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.Home
{
    public interface IHomeListingService
    {
        HomeListingResponse GetListingSearchLastNode(HomeListingRequest request);
        List<ListingGallery> GetCompLstImageGallery(decimal ListingId);
        List<CompanyListingTimming> GetCompanyListingTimming(decimal ListingId);
        List<ListingPaymentsMode> GetCompanyListingPaymentMode(decimal ListingId);
        List<ShowPopularCatByArea> GetPopularCatByArea(string CtName, decimal SbCId, string SbCName, string ArName,int TotalRecord = 0);
        List<CompanyListingPaymentMode> GetCompListingPaymentMode(decimal ListingId);
        List<CompanyListingSubCate> GetCompanyListingSubCate(decimal ListingId,string Location);
        List<CompanyListingSocialMedia> GetCompanyListingSocialMedia(decimal ListingId);
        CompanyListingContact GetContactNo(decimal ListingId);
        CompanyListingContact GetAddress(decimal ListingId);
        CompanyListingContact GetcompanyListbyId(decimal ListingId);
        SPGetCompnayDetailById GetcompanyDetailbyListingId(decimal ListingId);
        CompanyListingProfile GetCompanyListingProfile(decimal ListingId);      
        List<LoadCities> GetCityNameByArea(string location);
        List<SearchSbCategories> SearchFrontEndSubCategory(string SbCategoryName,string Location);
        CategoryMetasKeywords GetMetaDetail(string Location, decimal CatId);
        CategoryMetasKeywords GetSubMetaDetail(string Location, decimal SubCatId);
        CategoryMetasKeywords GetListingMetaDetail(string Location, decimal ListingId);
        ListingRatingWrapperModel FindByListingIdFront(decimal ListingId);
        ListingRatingWrapperModel FindByListingIdFront(decimal ListingId, int IncrementalCount);
        AddListingRatingWrapperModel CompanyPostRating(CompanyListingRating rating);
        AddListingRatingWrapperModel PostRatingOtp(CompanyRatingsOTP ratingsOTP);
        List<GetBulkQueryFormSubmittion> GetBulkQueryFormSubmittion(ListingQueryRequest request);
        List<ListingQueryTrack> GetLeadQueryTrack(DateTime FromDate , DateTime ToDate, decimal ListingId ,ref string CName);
        bool CustomerClientLogin(ClientLogin login);
        decimal changeNumber(ClientChangeNo login);
        Sp_GetClientSummaryResults GetClientSummaryResults(decimal CustomerId);
        string RequestCounters(decimal ListingId);
        List<CompanyListingContact> GetContactNoMobile(decimal ListingId);
        MainandChildWrapper GetMainandChildCategory();
    }
}
