using Microsoft.AspNetCore.Http;
using PAKDial.Common;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Domains.StoreProcedureModel.Home;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Interfaces.PakDialServices.Home;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using PAKDial.Interfaces.Repository;
using PAKDial.Interfaces.Repository.Home;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using PAKDial.Repository.Repositories.CompaniesListingsRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.Home
{
    public class HomeListingService : IHomeListingService
    {
        private readonly ICompanyListingRepository companyListingRepository;
        private readonly IListingGalleryRepository _IListingGalleryRepository;
        private readonly ICompanyListingTimmingRepository _ICompanyListingTimmingRepository;
        private readonly IListingPaymentsModeRepository _IListingPaymentsModeRepository;
        private readonly ICityAreaRepository cityAreaRepository;
        private readonly IListingCategoryRepository _IListingCategoryRepository;
        private readonly IMainMenuCategoryRepository _mainMenuCategory;
        private readonly ISubMenuCategoryRepository _ISubMenuCategoryRepository;
        private readonly IListingSocialMediaRepository _IListingSocialMediaRepository;
        private readonly IListingAddressRepository _IListingAddressRepository;
        private readonly IListingMobileNoRepository _IListingMobileNoRepository;
        private readonly IListingLandlineNoRepository _IListingLandlineNoRepository;
        private readonly ICompanyListingProfileRepository _ICompanyListingProfileRepository;
        private readonly ICompanyListingRatingService _companyListingRating;
        private readonly IVLoadHomePopularServiceRepository _IVLoadHomePopularServiceRepository;
        private readonly ICustomerService _customerService;

        public HomeListingService(ICompanyListingRepository companyListingRepository,
            IListingGalleryRepository IListingGalleryRepository,
            ICompanyListingTimmingRepository ICompanyListingTimmingRepository,
            IListingPaymentsModeRepository IListingPaymentsModeRepository,
            ICityAreaRepository cityAreaRepository,
            IListingCategoryRepository IListingCategoryRepository,
            IMainMenuCategoryRepository mainMenuCategory,
            ISubMenuCategoryRepository ISubMenuCategoryRepository,
            IListingSocialMediaRepository IListingSocialMediaRepository,
            IListingLandlineNoRepository IListingLandlineNoRepository,
            IListingMobileNoRepository IListingMobileNoRepository,
            IListingAddressRepository IListingAddressRepository,
            ICompanyListingProfileRepository ICompanyListingProfileRepository,
            ICompanyListingRatingService companyListingRating,
            IVLoadHomePopularServiceRepository IVLoadHomePopularServiceRepository,
            ICustomerService customerService)
        {
            this.companyListingRepository = companyListingRepository;
            _IListingGalleryRepository = IListingGalleryRepository;
            _ICompanyListingTimmingRepository = ICompanyListingTimmingRepository;
            _IListingPaymentsModeRepository = IListingPaymentsModeRepository;
            this.cityAreaRepository = cityAreaRepository;
            _IListingCategoryRepository = IListingCategoryRepository;
            _mainMenuCategory = mainMenuCategory;
            _ISubMenuCategoryRepository = ISubMenuCategoryRepository;
            _IListingSocialMediaRepository = IListingSocialMediaRepository;
            _IListingLandlineNoRepository = IListingLandlineNoRepository;
            _IListingMobileNoRepository = IListingMobileNoRepository;
            _IListingAddressRepository = IListingAddressRepository;
            _ICompanyListingProfileRepository = ICompanyListingProfileRepository;
            _companyListingRating = companyListingRating;
            _IVLoadHomePopularServiceRepository =IVLoadHomePopularServiceRepository;
            _customerService = customerService;
        }

        public List<ListingPaymentsMode> GetCompanyListingPaymentMode(decimal ListingId)
        {
            return _IListingPaymentsModeRepository.GetByListingId(ListingId);
        }

        public List<CompanyListingSubCate> GetCompanyListingSubCate(decimal ListingId,string Location)
        {
            List<CompanyListingSubCate> lLstCategory = new List<CompanyListingSubCate>();

            var lCategory = _IListingCategoryRepository.GetByListingId(ListingId);

            foreach (var item in lCategory)
            {
                var SubCategory = _ISubMenuCategoryRepository.GetSubCateById(item.SubCategoryId);
                CompanyListingSubCate lObjCompanyListingSubCate = new CompanyListingSubCate()
                {
                    Id = item.SubCategoryId,
                    Name = SubCategory.Name,
                    MainCategoryId = item.MainCategoryId,
                    MainCategoryName = _mainMenuCategory.Find(item.MainCategoryId).Name,
                    ListingId = item.ListingId,
                    Location = Location,
                };
                lLstCategory.Add(lObjCompanyListingSubCate);
            }
            return lLstCategory;
        }

        public List<CompanyListingTimming> GetCompanyListingTimming(decimal ListingId)
        {
            return _ICompanyListingTimmingRepository.GetTimmingByListingId(ListingId);
        }

        public List<CompanyListingPaymentMode> GetCompListingPaymentMode(decimal ListingId)
        {
            List<CompanyListingPaymentMode> lLstPymntMode = new List<CompanyListingPaymentMode>();

            var lPaymentMode = _IListingPaymentsModeRepository.GetByListingId(ListingId);
            foreach (var item in lPaymentMode)
            {
                var PaymentMode = _ICompanyListingTimmingRepository.GetPaymentModes(item.ModeId);
                CompanyListingPaymentMode lObjPymntMode = new CompanyListingPaymentMode()
                {
                    Id = PaymentMode.Id,
                    Name = PaymentMode.Name,
                    ImageDir = PaymentMode.ImageDir,
                    ImageUrl = PaymentMode.ImageUrl,
                    Description = PaymentMode.Description
                };
                lLstPymntMode.Add(lObjPymntMode);
            }
            return lLstPymntMode;
        }

        public List<ListingGallery> GetCompLstImageGallery(decimal ListingId)
        {
            return _IListingGalleryRepository.GetSelectedVluByListingId(ListingId);
        }

        public HomeListingResponse GetListingSearchLastNode(HomeListingRequest request)
        {
            return companyListingRepository.GetListingSearchLastNode(request);
        }
        public List<ShowPopularCatByArea> GetPopularCatByArea(string CtName, decimal SbCId, string SbCName, string ArName,int TotalRecord = 0)
        {
            int TotalRecords = TotalRecord;
            List<ShowPopularCatByArea> request = new List<ShowPopularCatByArea>();
            var OtherCityArea = cityAreaRepository.GetCityandCityNames(CtName, ArName,ref TotalRecords);
            foreach (var item in OtherCityArea)
            {
                ShowPopularCatByArea modes = new ShowPopularCatByArea
                {
                    CtName = CtName,
                    SbCId = SbCId,
                    SbCName = SbCName,
                    SbCNameReplace = SbCName.Replace("_", " "),
                    ArName = item,
                    SortFilter = "topResults",
                    TotalRecords = TotalRecords
                };
                request.Add(modes);
            }
            return request;
        }

        // Get CityArea By Location
        public List<LoadCities> GetCityNameByArea(string location)
        {
            return cityAreaRepository.GetCityNameByArea(location);
        }

        public List<CompanyListingSocialMedia> GetCompanyListingSocialMedia(decimal ListingId)
        {
            List<CompanyListingSocialMedia> response = new List<CompanyListingSocialMedia>();
            var socialmediaMode = _IListingSocialMediaRepository.SocialMediaModesList();
            foreach (var item in socialmediaMode)
            {
                ListingSocialMedia lsocialMedia = new ListingSocialMedia();
                lsocialMedia = _IListingSocialMediaRepository.GetListingSocialMedia(item.Id,ListingId);
                CompanyListingSocialMedia lObjCompanyListingSocialMedia = new CompanyListingSocialMedia()
                {
                    Id = (int)item.Id,
                    ImageUrl = item.ImageDir,
                    Name = item.Name,
                    ListingId = ListingId,
                    SitePath = lsocialMedia!=null ? lsocialMedia.SitePath : null,
                };
                response.Add(lObjCompanyListingSocialMedia);
            }
            return response;
        }

        public CompanyListingContact GetContactNo(decimal ListingId)
        {
            var MobileNo =_IListingMobileNoRepository.GetByListingId(ListingId);
            string strMobileNo = string.Join(", ", _IListingMobileNoRepository.GetByListingId(ListingId).Select(c => c.MobileNo));
            string strContactNo = string.Join(", ", _IListingLandlineNoRepository.GetByListingId(ListingId).Select(c => c.LandlineNumber));

            CompanyListingContact lObjCompanyListingContact = new CompanyListingContact()
            {
                Number = strMobileNo + ", " + strContactNo,
            };
         return lObjCompanyListingContact;
        }
        //Get Contact No For Mobile
        public List<CompanyListingContact> GetContactNoMobile(decimal ListingId)
        {
            List<CompanyListingContact> contacts = new List<CompanyListingContact>();
            var MobileNo = _IListingMobileNoRepository.GetByListingId(ListingId);
            var strMobileNo = _IListingMobileNoRepository.GetByListingId(ListingId).Select(c => new CompanyListingContact{Number = c.MobileNo });
            var strContactNo = _IListingLandlineNoRepository.GetByListingId(ListingId).Select(c => new CompanyListingContact { Number = c.LandlineNumber });

            if(strMobileNo.Count() > 0)
            {
                contacts.AddRange(strMobileNo);
            }
            if (strContactNo.Count() > 0)
            {
                contacts.AddRange(strContactNo);
            }
            return contacts;
        }

        public CompanyListingContact GetAddress(decimal ListingId)
        {
            var Address = _IListingAddressRepository.GetAddressByListingId(ListingId);
            CompanyListingContact lObjCompanyListingContact = new CompanyListingContact()
            {
                Address = Address
            };
            return lObjCompanyListingContact;
        }

        public CompanyListingContact GetcompanyListbyId(decimal ListingId)
        {
            var lComanyList = companyListingRepository.Find(ListingId);
            
            CompanyListingContact lObjCompanyListingContact = new CompanyListingContact()
            {
                
                Email = lComanyList.Email,
                Site = lComanyList.Website,
                
            };
            return lObjCompanyListingContact;
        }

        public List<SearchSbCategories> SearchFrontEndSubCategory(string SbCategoryName,string Location)
        {
            return _ISubMenuCategoryRepository.SearchFrontEndSubCategory(SbCategoryName,Location);
        }

        public CompanyListingProfile GetCompanyListingProfile(decimal ListingId)
        {
            return _ICompanyListingProfileRepository.GetByListingId(ListingId);
        }

        public SPGetCompnayDetailById GetcompanyDetailbyListingId(decimal ListingId)
        {
            return companyListingRepository.GetcompanyDetailbyListingId(ListingId);
        }

        public CategoryMetasKeywords GetMetaDetail(string Location, decimal CatId)
        {
            return _mainMenuCategory.GetMetaDetail(Location,CatId);
        }

        public CategoryMetasKeywords GetSubMetaDetail(string Location, decimal SubCatId)
        {
            return _ISubMenuCategoryRepository.GetSubMetaDetail(Location, SubCatId);
        }
        public CategoryMetasKeywords GetListingMetaDetail(string Location, decimal SubCatId)
        {
            return companyListingRepository.GetListingMetaDetail(Location, SubCatId);
        }
        public ListingRatingWrapperModel FindByListingIdFront(decimal ListingId)
        {
            return _companyListingRating.FindByListingIdFront(ListingId);
        }
        public ListingRatingWrapperModel FindByListingIdFront(decimal ListingId, int IncrementalCount)
        {
            return _companyListingRating.FindByListingIdFront(ListingId, IncrementalCount);
        }

        public AddListingRatingWrapperModel CompanyPostRating(CompanyListingRating rating)
        {
            return _companyListingRating.CompanyPostRating(rating);
        }
        public AddListingRatingWrapperModel PostRatingOtp(CompanyRatingsOTP ratingsOTP)
        {
            return _companyListingRating.PostRatingOtp(ratingsOTP);
        }

        public List<GetBulkQueryFormSubmittion> GetBulkQueryFormSubmittion(ListingQueryRequest request)
        {
            return _IVLoadHomePopularServiceRepository.GetBulkQueryFormSubmittion(request);
        }

        public bool CustomerClientLogin(ClientLogin login)
        {
            bool rs = false;
           var result = _customerService.CustomerClientLogin(login);
            if (result !=null && result.PhoneNumber != null && result.UserName != null)
            {
                rs = true;
            }
             
           return rs;
        }

        public Sp_GetClientSummaryResults GetClientSummaryResults(decimal CustomerId)
        {
            return companyListingRepository.GetClientSummaryResults(CustomerId);
        }

        public string RequestCounters(decimal ListingId)
        {
           return companyListingRepository.RequestCounters(ListingId);
        }

        public MainandChildWrapper GetMainandChildCategory()
        {
            MainandChildWrapper response = new MainandChildWrapper
            {
                MainSideBarMenu = _mainMenuCategory.GetSideMainMenu(),
                MainMenuSubMenu = _ISubMenuCategoryRepository.MainMenuSubMenuData()
            };
            return response;
        }

        public List<ListingQueryTrack> GetLeadQueryTrack(DateTime ToDate, DateTime FromDate, decimal ListingId, ref string CName)
        {
           return _IVLoadHomePopularServiceRepository.GetLeadQueryTrack(ToDate, FromDate, ListingId, ref CName);
        }

        public decimal changeNumber(ClientChangeNo login)
        {
            return _customerService.changeNumber(login);
        }
    }
}
