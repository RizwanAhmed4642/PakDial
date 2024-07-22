using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.RequestModels.CompanyListings;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ResponseModels.CompanyListing;
using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Domains.StoreProcedureModel.Home;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.ICompaniesListingsRepo
{
    public interface ICompanyListingRepository:IBaseRepository<CompanyListings,decimal>
    {

        decimal AddCompanyListing(VMAddCompanyListingModel Instance);
        decimal UpdateCompanyListing(VMAddCompanyListingModel Instance);
        decimal VerifyUnVerifyListing(decimal Id, string name, string CreatedBy, DateTime CreatedDate);
        VMAddCompanyListingModel FindRecord(decimal Id);
        VMAddCompanyListingModel FindRecord(decimal Id,decimal Custoemr);
        decimal DeleteCompanyListings(decimal Id);
        void UploadImages(decimal Id, string ImagePath, string AbsolutePath);
        void UploadGalleryImages( string ImagePath, string AbsolutePath,ListingGallery Entity);
        CompanyListingsResponse GetCompanyListings(CompanyListingsRequestModel request);
        CompanyListingsResponse GetClientCompanyListings(CompanyListingsRequestModel request);
        GetAddCompanyListingResponse GetCompanyListingWrapperList(string UserId);
        GetAddCompanyListingResponse GetClientCompanyListingWrapperList();
        decimal ActiveInActiveListUpdate(CompanyListings Entity);
        decimal DeleteBannerImage(decimal Id, string Path);
        GetCompanyKeyValueSearchResponse GetCompanySearchOnAreaBases(CompanyKeyValueSearchRequestModel request);
        HomeListingResponse GetListingSearchLastNode(HomeListingRequest request);
        SPGetCompnayDetailById GetcompanyDetailbyListingId(decimal ListingId);
        CategoryMetasKeywords GetListingMetaDetail(string Location, decimal ListingId);
        List<CompanyListings> GetCompanyListingsByUserId(decimal CustomerId);
        Sp_GetClientSummaryResults GetClientSummaryResults(decimal CustomerId);
        bool CheckCustomerListing(decimal CustomerId, decimal ListingId);
        string RequestCounters(decimal ListingId);
        VMCompanyListings FindRecord_2(decimal Id);
        decimal Transferupdate(decimal CustomerId, decimal ListingId);
    }
}
