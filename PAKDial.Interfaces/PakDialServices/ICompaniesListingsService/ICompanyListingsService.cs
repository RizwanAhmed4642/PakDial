using Microsoft.AspNetCore.Http;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.RequestModels.CompanyListings;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ResponseModels.CompanyListing;
using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.ICompaniesListingsService
{
    public interface ICompanyListingsService
    {
        decimal AddCompanyListing(VMAddCompanyListingModel Instance);
        decimal UpdateCompanyListing(VMAddCompanyListingModel Instance);
        decimal VerifyUnVerifyListing(decimal Id, string name, string CreatedBy, DateTime CreatedDate);
        decimal TransferCustomerOwnerShip(decimal CustomerId, decimal ListingId);
        VMAddCompanyListingModel FindRecord(decimal Id);
        VMCompanyListing FindById(decimal ListingId);
        VMCompanyListings FindRecord_2(decimal Id);
        VMCompanyListings FindRecord_2(decimal Id,decimal CustomerId);
        decimal DeleteCompanyListings(decimal Id);
        int UploadBannerImage(decimal Id, IFormFile file, string AbsolutePath);
        decimal UploadGalleryImage(IFormFileCollection file, string AbsolutePath, ListingGallery Entity);
        IEnumerable<CompanyListings> GetAll();
        CompanyListingsResponse GetCompanyListings(CompanyListingsRequestModel request);
        CompanyListingsResponse GetClientCompanyListings(CompanyListingsRequestModel request);
        GetAddCompanyListingResponse GetCompanyListingWrapperList(string UserId);
        decimal ActiveInActiveListUpdate(decimal Id , string Value,string UserId);
        decimal DeleteBannerImage(decimal Id);
        GetCompanyKeyValueSearchResponse GetCompanySearchOnAreaBases(CompanyKeyValueSearchRequestModel request);
        List<ListingMobileNo>  CheckMobileNo( string MobileNo ,int Id);
        List<ListingLandlineNo> CheckLandlineNo(string LandlineNo, int Id);
        List<CompanyListings> GetCompanyListingsByUserId(decimal CustomerId);
        GetAddCompanyListingResponse GetClientCompanyListingWrapperList();
        Sp_GetClientSummaryResults GetClientSummaryResults(decimal CustomerId);
        bool CheckCustomerListing(decimal CustomerId, decimal ListingId);
        void RequestCounters(decimal ListingId);
    }
}
