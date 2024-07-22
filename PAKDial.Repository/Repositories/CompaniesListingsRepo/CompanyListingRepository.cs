using Microsoft.EntityFrameworkCore;
using PAKDial.Common;
using PAKDial.Domains.Common;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.Mapper.CompanyListingMappers;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.RequestModels.CompanyListings;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ResponseModels.CompanyListing;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Domains.StoreProcedureModel.Home;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.Repository;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using PAKDial.Interfaces.Repository.ISearchesBehaviour;
using PAKDial.Repository.BaseRepository;
using PAKDial.StoreProcdures;
using PAKDial.StoreProcdures.Home;
using PAKDial.StoreProcdures.ListingsProc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PAKDial.Repository.Repositories.CompaniesListingsRepo
{
    public class CompanyListingRepository : BaseRepository<CompanyListings, decimal>, ICompanyListingRepository
    {
        #region Prop
        private readonly ICompanyListingProfileRepository _companyListingProfile;
        private readonly IListingAddressRepository _listingAddressRepository;
        private readonly IListingCategoryRepository _listingCategoryRepository;
        private readonly IListingPaymentsModeRepository _listingPaymentsMode;
        private readonly IListingLandlineNoRepository _listingLandlineNo;
        private readonly IListingMobileNoRepository _listingMobileNo;
        private readonly IListingGalleryRepository _listingGallery;
        private readonly IListingPremiumRepository _premiumRepository;
        private readonly ICompanyListingTimmingRepository _companyListingTimming;
        private readonly IListingSocialMediaRepository _listingSocialMedia;
        private readonly IBusinessTypesRepository _IBusinessTypesRepository;
        private readonly IVerifiedListingRepository _verifiedListing;
        private readonly IListingServicesRepository _listingServices;
        private readonly IListingsBusinessTypesRepository _IListingsBusinessTypesRepository;
        private readonly ICompanyListingRatingRepository _companyListingRatingRepository;
        private readonly ISearchBehaviourRepository _searchBehaviour;

        #endregion

        #region Ctor
        public CompanyListingRepository(PAKDialSolutionsContext context
            , ICompanyListingProfileRepository companyListingProfile, IListingAddressRepository listingAddressRepository,
            IListingCategoryRepository listingCategoryRepository, IListingPaymentsModeRepository listingPaymentsMode,
            IListingLandlineNoRepository listingLandlineNo, IListingMobileNoRepository listingMobileNo,
            IListingGalleryRepository listingGallery, IListingPremiumRepository premiumRepository,
            ICompanyListingTimmingRepository companyListingTimming, IListingSocialMediaRepository listingSocialMedia,
            IVerifiedListingRepository verifiedListing, IListingServicesRepository listingServices,
            IBusinessTypesRepository IBusinessTypesRepository,
            IListingsBusinessTypesRepository IListingsBusinessTypesRepository,
           ICompanyListingRatingRepository companyListingRatingRepository
            , ISearchBehaviourRepository searchBehaviour) : base(context)
        {
            _companyListingProfile = companyListingProfile;
            _listingAddressRepository = listingAddressRepository;
            _listingCategoryRepository = listingCategoryRepository;
            _listingPaymentsMode = listingPaymentsMode;
            _listingLandlineNo = listingLandlineNo;
            _listingMobileNo = listingMobileNo;
            _listingGallery = listingGallery;
            _premiumRepository = premiumRepository;
            _companyListingTimming = companyListingTimming;
            _listingSocialMedia = listingSocialMedia;
            _verifiedListing = verifiedListing;
            _listingServices = listingServices;
            _IBusinessTypesRepository = IBusinessTypesRepository;
            _IListingsBusinessTypesRepository = IListingsBusinessTypesRepository;
            _companyListingRatingRepository = companyListingRatingRepository;
            _searchBehaviour = searchBehaviour;
            db.Database.SetCommandTimeout(120);
        }

        #endregion

        #region DBSet
        protected override DbSet<CompanyListings> DbSet
        {
            get
            {
                return db.CompanyListings;
            }
        }

        #endregion

        #region Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Instance"></param>
        /// <returns></returns>
        public decimal AddCompanyListing(VMAddCompanyListingModel Instance)
        {
            return ListingProcdures.Sp_AddListingFromBackEnd(Instance);
        }
        //public decimal AddCompanyListing(VMAddCompanyListingModel Instance)
        //{
        //    decimal Result = 0;

        //    using (var Transaction = db.Database.BeginTransaction())

        //    {
        //        try
        //        {
        //            if (!string.IsNullOrEmpty(Instance.Registration.Id))
        //            {
        //                db.AspNetUsers.Add(Instance.Registration);
        //                db.SaveChanges();
        //                Customers customers = new Customers
        //                {
        //                    FirstName = Instance.CompanyListings.FirstName,
        //                    LastName = Instance.CompanyListings.LastName,
        //                    IsActive = true,
        //                    IsDefault = false,
        //                    UserId = Instance.Registration.Id,
        //                    PhoneNumber = Instance.ListingMobileNo.Select(c => c.MobileNo).FirstOrDefault().ToString(),
        //                    CreatedBy = Instance.CompanyListings.CreatedBy,
        //                    CreatedDate = Instance.CompanyListings.CreatedDate,
        //                    UpdatedBy = Instance.CompanyListings.UpdatedBy,
        //                    UpdatedDate = Instance.CompanyListings.UpdatedDate
        //                };
        //                db.Customers.Add(customers);
        //                db.SaveChanges();
        //                db.AspNetUserRoles.Add(new AspNetUserRoles { UserId = Instance.Registration.Id, RoleId = db.AspNetRoles.Where(c => c.Name == "Guest").Select(c => c.Id).FirstOrDefault() });
        //                Instance.CompanyListings.CustomerId = customers.Id;
        //                db.SaveChanges();
        //            }
        //            if(Instance.CustomerRegistration != null)
        //            {
        //                if(Instance.CustomerRegistration.Email == null)
        //                {
        //                    var UserId = db.Customers.Where(c => c.Id == Instance.CompanyListings.CustomerId).FirstOrDefault().UserId;
        //                    Instance.CompanyListings.Email = db.AspNetUsers.Where(c => c.Id == UserId).FirstOrDefault().Email;
        //                }
        //            }
        //            db.CompanyListings.Add(Instance.CompanyListings);
        //            db.SaveChanges();
        //            // ListingAddress
        //            if (Instance.ListingAddress.CityId > 0)
        //            {
        //                Instance.ListingAddress.ListingId = Instance.CompanyListings.Id;
        //                Instance.ListingAddress.CreatedBy = Instance.CompanyListings.CreatedBy;
        //                Instance.ListingAddress.CreatedDate = Instance.CompanyListings.CreatedDate;
        //                Instance.ListingAddress.UpdateBy = Instance.CompanyListings.UpdatedBy;
        //                Instance.ListingAddress.UpdatedDate = Instance.CompanyListings.UpdatedDate;
        //                db.ListingAddress.Add(Instance.ListingAddress);
        //            }
        //            // CompanyListingProfile
        //            if (Instance.CompanyListingProfile != null)
        //            {
        //                Instance.CompanyListingProfile.ListingId = Instance.CompanyListings.Id;
        //                Instance.CompanyListingProfile.CreatedBy = Instance.CompanyListings.CreatedBy;
        //                Instance.CompanyListingProfile.CreatedDate = Instance.CompanyListings.CreatedDate;
        //                Instance.CompanyListingProfile.UpdatedBy = Instance.CompanyListings.UpdatedBy;
        //                Instance.CompanyListingProfile.UpdatedDate = Instance.CompanyListings.UpdatedDate;
        //                db.CompanyListingProfile.Add(Instance.CompanyListingProfile);
        //            }
        //            //ListingCategory
        //            if (Instance.ListingCategory != null && Instance.ListingCategory.Count() > 0)
        //            {
        //                foreach (var Lc in Instance.ListingCategory)
        //                {
        //                    Lc.ListingId = Instance.CompanyListings.Id;
        //                    Lc.CreatedBy = Instance.CompanyListings.CreatedBy;
        //                    Lc.CreatedDate = Instance.CompanyListings.CreatedDate;
        //                    Lc.UpdatedBy = Instance.CompanyListings.UpdatedBy;
        //                    Lc.UpdatedDate = Instance.CompanyListings.UpdatedDate;
        //                }
        //                db.ListingCategory.AddRange(Instance.ListingCategory);
        //            }
        //            //ListingPaymentsMode
        //            if (Instance.ListingPaymentsMode != null && Instance.ListingPaymentsMode.Count() > 0)
        //            {
        //                foreach (var LPM in Instance.ListingPaymentsMode)
        //                {
        //                    LPM.ListingId = Instance.CompanyListings.Id;
        //                    LPM.CreatedBy = Instance.CompanyListings.CreatedBy;
        //                    LPM.CreatedDate = Instance.CompanyListings.CreatedDate;
        //                    LPM.UpdatedBy = Instance.CompanyListings.UpdatedBy;
        //                    LPM.UpdateDate = Instance.CompanyListings.UpdatedDate;
        //                }
        //                db.ListingPaymentsMode.AddRange(Instance.ListingPaymentsMode);
        //            }
        //            //CompanyListingTimming
        //            if (Instance.CompanyListingTimming != null && Instance.CompanyListingTimming.Count() > 0)
        //            {
        //                foreach (var CLT in Instance.CompanyListingTimming)
        //                {
        //                    CLT.ListingId = Instance.CompanyListings.Id;
        //                    CLT.CreatedBy = Instance.CompanyListings.CreatedBy;
        //                    CLT.CreatedDate = Instance.CompanyListings.CreatedDate;
        //                    CLT.UpdatedBy = Instance.CompanyListings.UpdatedBy;
        //                    CLT.UpdatedDate = Instance.CompanyListings.UpdatedDate;
        //                }
        //                db.CompanyListingTimming.AddRange(Instance.CompanyListingTimming);
        //            }
        //            //ListingLandlineNo
        //            if (Instance.ListingLandlineNo != null && Instance.ListingLandlineNo.Count() > 0)
        //            {
        //                foreach (var LLN in Instance.ListingLandlineNo)
        //                {
        //                    LLN.ListingId = Instance.CompanyListings.Id;
        //                    LLN.CreatedBy = Instance.CompanyListings.CreatedBy;
        //                    LLN.CreatedDate = Instance.CompanyListings.CreatedDate;
        //                    LLN.UpdatedBy = Instance.CompanyListings.UpdatedBy;
        //                    LLN.UpdatedDate = Instance.CompanyListings.UpdatedDate;
        //                }
        //                db.ListingLandlineNo.AddRange(Instance.ListingLandlineNo);
        //            }
        //            if (Instance.ListingMobileNo != null && Instance.ListingMobileNo.Count() > 0)
        //            {
        //                //ListingMobileNo
        //                foreach (var LMN in Instance.ListingMobileNo)
        //                {
        //                    LMN.ListingId = Instance.CompanyListings.Id;
        //                    LMN.CreatedBy = Instance.CompanyListings.CreatedBy;
        //                    LMN.CreatedDate = Instance.CompanyListings.CreatedDate;
        //                    LMN.UpdatedBy = Instance.CompanyListings.UpdatedBy;
        //                    LMN.UpdatedDate = Instance.CompanyListings.UpdatedDate;
        //                }
        //                db.ListingMobileNo.AddRange(Instance.ListingMobileNo);
        //            }

        //            //ListingServices
        //            if (Instance.ListingServices != null && Instance.ListingServices.Count > 0)
        //            {
        //                foreach (var LS in Instance.ListingServices)
        //                {
        //                    LS.ListingId = Instance.CompanyListings.Id;
        //                    LS.CreatedBy = Instance.CompanyListings.CreatedBy;
        //                    LS.CreatedDate = Instance.CompanyListings.CreatedDate;
        //                    LS.UpdatedBy = Instance.CompanyListings.UpdatedBy;
        //                    LS.UpdatedDate = Instance.CompanyListings.UpdatedDate;
        //                }
        //                db.ListingServices.AddRange(Instance.ListingServices);
        //            }
        //            //ListingSocialMedia
        //            if (Instance.ListingSocialMedia != null && Instance.ListingSocialMedia.Count > 0)
        //            {
        //                foreach (var LSM in Instance.ListingSocialMedia)
        //                {
        //                    LSM.MediaId = db.SocialMediaModes.Where(c => c.Name.Trim().ToLower() == LSM.Name.Trim().ToLower()).Select(c => c.Id).FirstOrDefault();
        //                    LSM.ListingId = Instance.CompanyListings.Id;
        //                    LSM.CreatedBy = Instance.CompanyListings.CreatedBy;
        //                    LSM.CreatedDate = Instance.CompanyListings.CreatedDate;
        //                    LSM.UpdatedBy = Instance.CompanyListings.UpdatedBy;
        //                    LSM.UpdateDate = Instance.CompanyListings.UpdatedDate;
        //                }
        //                db.ListingSocialMedia.AddRange(Instance.ListingSocialMedia);
        //            }
        //            if (Instance.ListingsBusinessTypes != null && Instance.ListingsBusinessTypes.Count > 0)
        //            {
        //                foreach (var BT in Instance.ListingsBusinessTypes)
        //                {
        //                    BT.BusinessId = db.BusinessTypes.Where(x => x.Id == BT.BusinessId).FirstOrDefault().Id;
        //                    BT.ListingId = Instance.CompanyListings.Id;
        //                    BT.CreatedBy = Instance.CompanyListings.CreatedBy;
        //                    BT.CreatedDate = Instance.CompanyListings.CreatedDate;
        //                    BT.UpdatedBy = Instance.CompanyListings.UpdatedBy;
        //                    BT.UpdatedDate = Instance.CompanyListings.UpdatedDate;
        //                }
        //                db.ListingsBusinessTypes.AddRange(Instance.ListingsBusinessTypes);
        //            }
        //            db.SaveChanges();
        //            Transaction.Commit();
        //            Result = Instance.CompanyListings.Id;
        //        }
        //        catch (Exception ex)
        //        {
        //            Transaction.Rollback();
        //            Result = 0;
        //        }
        //    }

        //    return Result;

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VMAddCompanyListingModel FindRecord(decimal Id)
        {
            VMAddCompanyListingModel lObjListingModel = new VMAddCompanyListingModel();
            lObjListingModel.CompanyListings = DbSet.Find(Id);
            if(lObjListingModel.CompanyListings!=null)
            { 
            lObjListingModel.CompanyListingProfile = _companyListingProfile.GetByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.ListingAddress = _listingAddressRepository.GetByListingId(lObjListingModel.CompanyListings.Id).FirstOrDefault() ?? null;
            lObjListingModel.ListingCategory = _listingCategoryRepository.GetByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.ListingLandlineNo = _listingLandlineNo.GetByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.ListingMobileNo = _listingMobileNo.GetByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.ListingPaymentsMode = _listingPaymentsMode.GetByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.ListingPremium = _premiumRepository.GetByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.ListingServices = _listingServices.GetByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.ListingSocialMedia = _listingSocialMedia.GetByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.ListingsBusinessTypes = _IListingsBusinessTypesRepository.GetByListingsId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.VerifiedListing = _verifiedListing.GetByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.ListingGallery = _listingGallery.GetByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.CompanyListingTimming = _companyListingTimming.GetTimmingByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.CompanyListingRating = _companyListingRatingRepository.FindByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            }
            return lObjListingModel;
        }

        public VMCompanyListings FindRecord_2(decimal Id)
        {
            var res = ListingProcdures.Sp_Find2ListingListingById(Id);
            return res;
        }

        /// <summary>
        /// Update a record by View Model.
        /// </summary>
        /// <param name="Instance"></param>
        /// <returns></returns>
        public decimal UpdateCompanyListing(VMAddCompanyListingModel Instance)
        {
            decimal Result = 0;
            using (var Transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var lObjCompListing = db.CompanyListings.Find(Instance.CompanyListings.Id);
                    Instance.CompanyListings.ListingStatus = lObjCompListing.ListingStatus;
                    //ListingAddress
                    Instance.ListingAddress.UpdateBy = Instance.CompanyListings.UpdatedBy;
                    Instance.ListingAddress.UpdatedDate = Instance.CompanyListings.UpdatedDate;

                    //CompanyListingProfile
                    Instance.CompanyListingProfile.UpdatedBy = Instance.CompanyListings.UpdatedBy;
                    Instance.CompanyListingProfile.UpdatedDate = Instance.CompanyListings.UpdatedDate;

                    //CompanyListingTimming
                    foreach (var CLT in Instance.CompanyListingTimming)
                    {
                        CLT.ListingId = lObjCompListing.Id;
                        CLT.CreatedBy = lObjCompListing.CreatedBy;
                        CLT.CreatedDate = lObjCompListing.CreatedDate;
                        CLT.UpdatedBy = Instance.CompanyListings.UpdatedBy;
                        CLT.UpdatedDate = Instance.CompanyListings.UpdatedDate;
                    }
                    db.CompanyListingTimming.RemoveRange(db.CompanyListingTimming.Where(c => c.ListingId == lObjCompListing.Id).ToList());

                    //ListingCategory
                    foreach (var Lc in Instance.ListingCategory)
                    {
                        Lc.ListingId = lObjCompListing.Id;
                        Lc.CreatedBy = lObjCompListing.CreatedBy;
                        Lc.CreatedDate = lObjCompListing.CreatedDate;
                        Lc.UpdatedBy = Instance.CompanyListings.UpdatedBy;
                        Lc.UpdatedDate = Instance.CompanyListings.UpdatedDate;
                    }
                    db.ListingCategory.RemoveRange(db.ListingCategory.Where(c => c.ListingId == lObjCompListing.Id).ToList());

                    //ListingPaymentsMode
                    foreach (var LPM in Instance.ListingPaymentsMode)
                    {
                        LPM.ListingId = lObjCompListing.Id;
                        LPM.CreatedBy = lObjCompListing.CreatedBy;
                        LPM.CreatedDate = lObjCompListing.CreatedDate;
                        LPM.UpdatedBy = Instance.CompanyListings.UpdatedBy;
                        LPM.UpdateDate = Instance.CompanyListings.UpdatedDate;
                    }
                    db.ListingPaymentsMode.RemoveRange(db.ListingPaymentsMode.Where(c => c.ListingId == lObjCompListing.Id).ToList());

                    //ListingLandlineNo
                     if (Instance.ListingLandlineNo!=null && Instance.ListingLandlineNo.Count>0)
                    {

                    foreach (var LLN in Instance.ListingLandlineNo)
                    {
                        LLN.ListingId = lObjCompListing.Id;
                        LLN.CreatedBy = lObjCompListing.CreatedBy;
                        LLN.CreatedDate = lObjCompListing.CreatedDate;
                        LLN.UpdatedBy = Instance.CompanyListings.UpdatedBy;
                        LLN.UpdatedDate = Instance.CompanyListings.UpdatedDate;
                    }
                    db.ListingLandlineNo.RemoveRange(db.ListingLandlineNo.Where(c => c.ListingId == lObjCompListing.Id).ToList());

                    }
                    //ListingMobileNo
                    foreach (var LMN in Instance.ListingMobileNo)
                    {
                        LMN.ListingId = lObjCompListing.Id;
                        LMN.CreatedBy = lObjCompListing.CreatedBy;
                        LMN.CreatedDate = lObjCompListing.CreatedDate;
                        LMN.UpdatedBy = Instance.CompanyListings.UpdatedBy;
                        LMN.UpdatedDate = Instance.CompanyListings.UpdatedDate;
                    }
                    db.ListingMobileNo.RemoveRange(db.ListingMobileNo.Where(c => c.ListingId == lObjCompListing.Id).ToList());

                    //ListingServices
                    foreach (var LS in Instance.ListingServices)
                    {
                        LS.ListingId = lObjCompListing.Id;
                        LS.CreatedBy = lObjCompListing.CreatedBy;
                        LS.CreatedDate = lObjCompListing.CreatedDate;
                        LS.UpdatedBy = Instance.CompanyListings.UpdatedBy;
                        LS.UpdatedDate = Instance.CompanyListings.UpdatedDate;
                    }
                    db.ListingServices.RemoveRange(db.ListingServices.Where(c => c.ListingId == Instance.CompanyListings.Id));
                    db.ListingsBusinessTypes.RemoveRange(db.ListingsBusinessTypes.Where(c => c.ListingId == Instance.CompanyListings.Id));
                    //ListingSocialMedia
                    db.ListingSocialMedia.RemoveRange(db.ListingSocialMedia.Where(c => c.ListingId == lObjCompListing.Id).ToList());
                    //One Sb.SaveChange Remove All Existing Data
                    db.SaveChanges();
                    if (Instance.ListingSocialMedia != null && Instance.ListingSocialMedia.Count > 0)
                    {
                        
                        foreach (var LSM in Instance.ListingSocialMedia)
                        {
                            LSM.MediaId = db.SocialMediaModes.Where(c => c.Name.Trim().ToLower() == LSM.Name.Trim().ToLower()).Select(c => c.Id).FirstOrDefault();
                            LSM.ListingId = lObjCompListing.Id;
                            LSM.CreatedBy = lObjCompListing.CreatedBy;
                            LSM.CreatedDate = lObjCompListing.CreatedDate;
                            LSM.UpdatedBy = Instance.CompanyListings.UpdatedBy;
                            LSM.UpdateDate = Instance.CompanyListings.UpdatedDate;
                        }
                        db.ListingSocialMedia.AddRange(Instance.ListingSocialMedia);
                    }

                    if (Instance.ListingsBusinessTypes != null && Instance.ListingsBusinessTypes.Count > 0)
                    {
                        foreach (var BT in Instance.ListingsBusinessTypes)
                        {

                            BT.BusinessId = db.BusinessTypes.Where(x => x.Id == BT.BusinessId).FirstOrDefault().Id;
                            BT.CreatedBy = Instance.CompanyListings.CreatedBy;
                            BT.CreatedDate = Instance.CompanyListings.CreatedDate;
                            BT.UpdatedBy = Instance.CompanyListings.UpdatedBy;
                            BT.UpdatedDate = Instance.CompanyListings.UpdatedDate;
                        }
                        db.ListingsBusinessTypes.AddRange(Instance.ListingsBusinessTypes);
                    }

                    db.CompanyListings.Update(CompanyListingMapper.MapCompanyListing(Instance.CompanyListings, lObjCompListing));
                    db.CompanyListingProfile.Update(CompanyListingProfileMapper.MapCompanyListingProfile(Instance.CompanyListingProfile, db.CompanyListingProfile.Where(c => c.ListingId == lObjCompListing.Id).FirstOrDefault()));
                    db.ListingAddress.Update(ListingAddressMapper.MapListingAddress(Instance.ListingAddress, db.ListingAddress.Where(c => c.ListingId == lObjCompListing.Id).FirstOrDefault()));
                    db.CompanyListingTimming.AddRange(Instance.CompanyListingTimming);
                    db.ListingCategory.AddRange(Instance.ListingCategory);
                    db.ListingPaymentsMode.AddRange(Instance.ListingPaymentsMode);
                    if(Instance.ListingLandlineNo!=null && Instance.ListingLandlineNo.Count>0)
                    {
                        db.ListingLandlineNo.AddRange(Instance.ListingLandlineNo);
                    }
                   
                    db.ListingMobileNo.AddRange(Instance.ListingMobileNo);
                    db.ListingServices.AddRange(Instance.ListingServices);
                    db.SaveChanges();
                    Transaction.Commit();
                    Result = Instance.CompanyListings.Id;
                }
                catch (Exception ex)
                {
                    Transaction.Rollback();
                    Result = 0;
                }
            }

            return Result;
        }

        public decimal DeleteCompanyListings(decimal Id)
        {
            decimal Result = 0;
            using (var Transaction = db.Database.BeginTransaction())
            {
                try
                {

                    var Model = FindRecord(Id);
                    if (Model.CompanyListingProfile != null)
                    {
                        db.CompanyListingProfile.Remove(Model.CompanyListingProfile);
                    }
                    if (Model.ListingAddress != null)
                    {
                        db.ListingAddress.Remove(Model.ListingAddress);
                    }
                    if (Model.CompanyListingTimming != null)
                    {
                        db.CompanyListingTimming.RemoveRange(Model.CompanyListingTimming);
                    }
                    if (Model.ListingCategory != null)
                    {
                        db.ListingCategory.RemoveRange(Model.ListingCategory);
                    }
                    if (Model.ListingPaymentsMode != null)
                    {
                        db.ListingPaymentsMode.RemoveRange(Model.ListingPaymentsMode);
                    }
                    if (Model.ListingLandlineNo != null)
                    {
                        db.ListingLandlineNo.RemoveRange(Model.ListingLandlineNo);
                    }
                    if (Model.ListingMobileNo != null)
                    {
                        db.ListingMobileNo.RemoveRange(Model.ListingMobileNo);
                    }
                    if (Model.ListingGallery != null)
                    {
                        db.ListingGallery.RemoveRange(Model.ListingGallery);
                    }
                    if (Model.ListingPremium != null)
                    {
                        db.ListingPremium.RemoveRange(Model.ListingPremium);
                    }
                    if (Model.ListingSocialMedia != null)
                    {
                        db.ListingSocialMedia.RemoveRange(Model.ListingSocialMedia);
                    }
                    if (Model.VerifiedListing != null)
                    {
                        db.VerifiedListing.RemoveRange(Model.VerifiedListing);
                    }
                    if (Model.ListingsBusinessTypes != null)
                    {
                        db.ListingsBusinessTypes.RemoveRange(Model.ListingsBusinessTypes);
                    }

                    if (Model.ListingServices != null)
                    {

                        db.ListingServices.RemoveRange(Model.ListingServices);
                    }
                    if (Model.CompanyListingRating != null)
                    {
                        db.CompanyListingRating.RemoveRange(db.CompanyListingRating.Where(c => c.ListingId == Model.CompanyListings.Id).ToList());
                    }
                    db.SaveChanges();
                    db.CompanyListings.RemoveRange(Model.CompanyListings);
                    db.SaveChanges();
                    Transaction.Commit();
                    Result = Id;
                }
                catch (Exception ex)
                {
                    Transaction.Rollback();
                    Result = 0;
                }
            }

            return Result;

        }

        public void UploadImages(decimal Id, string ImagePath, string AbsolutePath)
        {
            //Bannner 
            if (!string.IsNullOrEmpty(ImagePath))
            {

                var companyListings = DbSet.Where(c => c.Id == Id).FirstOrDefault();
                companyListings.BannerImage = ImagePath;
                companyListings.BannerImageUrl = AbsolutePath;
                Update(companyListings);
            }
        }

        public void UploadGalleryImages(string ImagePath, string AbsolutePath, ListingGallery Entity)
        {
            throw new NotImplementedException();
        }

        public CompanyListingsResponse GetCompanyListings(CompanyListingsRequestModel request)
        {
            try
            {
                request.CityAreaIds = SaleExecutiveOrders_Execution.SpGetCityAreasByUserId(request.UserId);
                int fromRow = 0;
                if (request.PageNo == 1)
                {
                    fromRow = (request.PageNo - 1) * request.PageSize;
                }
                else
                {
                    fromRow = request.PageNo;
                }

                int toRow = request.PageSize;
                bool isSearchFilterSpecified = !string.IsNullOrEmpty(request.SearchString);
                Expression<Func<VCompanyListings, bool>> query = null;
				var checkNumeric = decimal.TryParse(request.SearchString, out decimal result);

				if (!checkNumeric)
				{
					if (request.CityAreaIds.Count() > 0)
					{
						query =
						exp =>
							   (isSearchFilterSpecified && ((exp.CompanyName.Contains(request.SearchString))) ||
							   ((exp.FullName.Contains(request.SearchString))) 
							   || !isSearchFilterSpecified) && request.CityAreaIds.Contains(exp.CityAreaId) && exp.CreatedBy == request.UserId;
					}
					else
					{
						query =
						exp =>
							   (isSearchFilterSpecified && ((exp.CompanyName.Contains(request.SearchString))) ||
							   ((exp.FullName.Contains(request.SearchString))) 
							   || !isSearchFilterSpecified);
					}
				}
				else
				{
					if (request.CityAreaIds.Count() > 0)
					{
						query =
						exp =>
							   (isSearchFilterSpecified && ((exp.MobileNumber.Contains(request.SearchString)))
							   || !isSearchFilterSpecified) && request.CityAreaIds.Contains(exp.CityAreaId) && exp.CreatedBy == request.UserId;
					}
					else
					{
						query =
						exp =>
							   (isSearchFilterSpecified && ((exp.MobileNumber.Contains(request.SearchString)))
							   || !isSearchFilterSpecified);
					}
				}


                int rowCount = db.VCompanyListings.Count(query);
                // Server Side Pager
                IEnumerable<VCompanyListings> _CompanyListings = request.IsAsc
                    ? db.VCompanyListings.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList()
                    : db.VCompanyListings.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList();
                return new CompanyListingsResponse
                {
                    RowCount = rowCount,
                    CompanyListings = _CompanyListings
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public CompanyListingsResponse GetClientCompanyListings(CompanyListingsRequestModel request)
        {
            try
            {
                int fromRow = 0;
                if (request.PageNo == 1)
                {
                    fromRow = (request.PageNo - 1) * request.PageSize;
                }
                else
                {
                    fromRow = request.PageNo;
                }

                int toRow = request.PageSize;
                bool isSearchFilterSpecified = !string.IsNullOrEmpty(request.SearchString);
                Expression<Func<VCompanyListings, bool>> query=
                    exp =>
                           (isSearchFilterSpecified && ((exp.CompanyName.Contains(request.SearchString))) ||
                           ((exp.FullName.Contains(request.SearchString))) || ((exp.ListingType.Contains(request.SearchString)))
                           || !isSearchFilterSpecified) && exp.CustomerId == request.CustomerId;

                int rowCount = db.VCompanyListings.Count(query);
                // Server Side Pager
                IEnumerable<VCompanyListings> _CompanyListings = request.IsAsc
                    ? db.VCompanyListings.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList()
                    : db.VCompanyListings.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList();
                return new CompanyListingsResponse
                {
                    RowCount = rowCount,
                    CompanyListings = _CompanyListings
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        /// <summary>
        /// Return Value Without Filter in AddMode
        /// </summary>
        /// <returns></returns>
        public GetAddCompanyListingResponse GetCompanyListingWrapperList(string UserId)
        {
            GetAddCompanyListingResponse response = new GetAddCompanyListingResponse
            {
                ListingTypes = db.ListingTypes.Where(c => c.IsActive == true).Select(c => new VMKeyValuePair { id = c.Id, text = c.Name }).ToList(),
                Categories = db.MainMenuCategory.Where(c => c.IsActive == true).Select(c => new VMKeyValuePair { id = c.Id, text = c.Name }).ToList(),
                States = db.StateProvince.Select(c => new VMKeyValuePair { id = c.Id, text = c.Name }).ToList(),
                SocialMediaModes = db.SocialMediaModes.Where(c => c.IsActive == true).ToList(),
            };
            var emp = db.Employee.Where(c => c.UserId == UserId.ToString()).FirstOrDefault();
            if (emp.ZoneManagerId > 0)
            {
                var AssignArea = db.AssignedEmpAreas.Where(c => c.EmployeeId == emp.ZoneManagerId).ToList();
                if (AssignArea.Count() > 0)
                {
                    var ActiveZones = db.VActiveZones.Where(c => c.CityId == AssignArea.FirstOrDefault().CityId && AssignArea.Select(e => e.ZoneId).Contains(c.ZoneId)).ToList();
                    response.StateId = ActiveZones.FirstOrDefault().StateId;
                    response.CityId = ActiveZones.FirstOrDefault().CityId;
                    response.CityAreaKeyValue = ActiveZones.Select(c => new VMGenericKeyValuePair<decimal> { Id = c.CityAreaId, Text = c.CityAreaName }).ToList();
                }
            }
            return response;
        }
        public GetAddCompanyListingResponse GetClientCompanyListingWrapperList()
        {
            GetAddCompanyListingResponse response = new GetAddCompanyListingResponse
            {
                ListingTypes = db.ListingTypes.Where(c => c.IsActive == true).Select(c => new VMKeyValuePair { id = c.Id, text = c.Name }).ToList(),
                Categories = db.MainMenuCategory.Where(c => c.IsActive == true).Select(c => new VMKeyValuePair { id = c.Id, text = c.Name }).ToList(),
                States = db.StateProvince.Select(c => new VMKeyValuePair { id = c.Id, text = c.Name }).ToList(),
                SocialMediaModes = db.SocialMediaModes.Where(c => c.IsActive == true).ToList(),
            };
            //var emp = db.Employee.Where(c => c.UserId == UserId.ToString()).FirstOrDefault();
            //if (emp.ZoneManagerId > 0)
            //{
            //    var AssignArea = db.AssignedEmpAreas.Where(c => c.EmployeeId == emp.ZoneManagerId).ToList();
            //    if (AssignArea.Count() > 0)
            //    {
            //        var ActiveZones = db.VActiveZones.Where(c => c.CityId == AssignArea.FirstOrDefault().CityId && AssignArea.Select(e => e.ZoneId).Contains(c.ZoneId)).ToList();
            //        response.StateId = ActiveZones.FirstOrDefault().StateId;
            //        response.CityId = ActiveZones.FirstOrDefault().CityId;
            //        response.CityAreaKeyValue = ActiveZones.Select(c => new VMGenericKeyValuePair<decimal> { Id = c.CityAreaId, Text = c.CityAreaName }).ToList();
            //    }
            //}
            return response;
        }
        public decimal VerifyUnVerifyListing(decimal Id, string name, string CreatedBy, DateTime CreatedDate)
        {
            decimal Result = 0; //Not Verified and UnVerified
            if (name == "Verify")
            {
                List<VerifiedListing> verifieds = new List<VerifiedListing>();
                var ListingResult = db.VerificationTypes.ToList();
                foreach (var item in ListingResult)
                {
                    verifieds.Add(new VerifiedListing
                    {
                        ListingId = Id,
                        VerificationId = item.Id,
                        CreatedBy = CreatedBy,
                        CreatedDate = CreatedDate,
                        UpdatedBy = CreatedBy,
                        UpdateDate = CreatedDate
                    });
                }
                db.VerifiedListing.AddRange(verifieds);
                db.SaveChanges();
                Result = 1;
            }
            else if (name == "UnVerify")
            {
                db.VerifiedListing.RemoveRange(db.VerifiedListing.Where(c => c.ListingId == Id).ToList());
                db.SaveChanges();
                Result = 1;
            }
            return Result;
        }

        public decimal ActiveInActiveListUpdate(CompanyListings Entity)
        {
            decimal Result = 0;
            try
            {
                Update(Entity);
                db.SaveChanges();
                Result = Entity.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public decimal DeleteBannerImage(decimal Id, string Path)
        {
            decimal Result = 0;
            try
            {
                var CompanyList = db.CompanyListings.Where(c => c.Id == Id).FirstOrDefault();

                if (File.Exists(Path.Replace("\\", "/") + CompanyList.BannerImage))
                {
                    var ImgPath = Path.Replace("\\", "/") + CompanyList.BannerImage;
                    File.Delete(ImgPath);
                }
                CompanyList.BannerImage = null;
                CompanyList.BannerImageUrl = null;
                Update(CompanyList);
                db.SaveChanges();
                Result = CompanyList.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public GetCompanyKeyValueSearchResponse GetCompanySearchOnAreaBases(CompanyKeyValueSearchRequestModel request)
        {
            try
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;
                Expression<Func<VActiveCompanyListing, bool>> query =
                   exp =>
                       (string.IsNullOrEmpty(request.SearchString) || (exp.CompanyName.Contains(request.SearchString))
                          && exp.CityAreaId == request.CityAreaId);
                int rowCount = db.VActiveCompanyListing.Count(query);
                // Server Side Pager
                IEnumerable<VMGenericKeyValuePair<decimal>> SelectedCompany =
                      db.VActiveCompanyListing.Where(query)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList().Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.CompanyName });
                return new GetCompanyKeyValueSearchResponse
                {
                    PageNo = fromRow,
                    RowCount = rowCount,
                    AreasBasedCompany = SelectedCompany
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*Client Front End Repository*/
        public HomeListingResponse GetListingSearchLastNode(HomeListingRequest request)
        {
            request.PageSize = 30;
            int offset = (request.PageSize * request.PageNo) - request.PageSize;
            //if(string.IsNullOrEmpty(request.ArName))
            //{
            //    request.ArName = db.VCityArea.Where(c => CommonSpacing.RemoveSpacestoTrim(c.CityName)
            //                     == CommonSpacing.RemoveSpacestoTrim(request.CtName)).FirstOrDefault().Name;
            //}
            if(request.PageNo == 1 && request.SortColumnName == FilterStatus.topResults.ToString())
            {
                _searchBehaviour.Add(new SearchBehaviour { SearchResults = request.SbCName.Replace("_", " "), LocationSearch = request.CtName, AreaSearch = request.ArName != null ? request.ArName : "NULL", CreatedDate = DateTime.Now });
                _searchBehaviour.SaveChanges();
            }
           
            //List<decimal> SbCatId = new List<decimal>();
            var origalname = request.SbCName.Replace("_", " ");
            var Procedure = ListingSearchProcdure.GetCompanyListingPaging(request.SbCId, origalname,request.CtName,request.ArName,request.SortColumnName
                ,request.Ratingstatus,offset,request.PageSize);
            //var CheckLastNode = db.SubMenuCategory.Where(c => c.Id == request.SbCId && c.Name.ToLower().Trim() == origalname.ToLower().Trim() && c.IsLastNode == true).FirstOrDefault();
            //if (CheckLastNode != null)
            //{
            //    SbCatId.Add(request.SbCId);
            //}
            //else
            //{
            //    SbCatId = ListingSearchProcdure.GetSearchListingByRecursion(request.SbCId, origalname);
            //}
            //Expression<Func<VHomeListingSearch, bool>> query = null;
            //if(!string.IsNullOrEmpty(request.ArName))
            //{
            //    query =
            //            exp =>
            //                  ((exp.SpaceCityName == CommonSpacing.RemoveSpacestoTrim(request.CtName)) &&
            //                   (exp.SpaceCityArea == CommonSpacing.RemoveSpacestoTrim(request.ArName)))
            //                   && SbCatId.Contains(exp.SubCatId);
            //}
            //else
            //{
            //    query =
            //            exp =>
            //                  (exp.SpaceCityName == CommonSpacing.RemoveSpacestoTrim(request.CtName))
            //                   && SbCatId.Contains(exp.SubCatId);
            //}

            //int rowCount = db.VHomeListingSearch.Count(query);
            int rowCount = Procedure.RowCount;
            // Server Side Pager
            List<VHomeListingSearch> search = Procedure.HomeListingSearch;
               
            //if(request.SortColumnName == FilterStatus.topResults.ToString() 
            //    || request.SortColumnName == FilterStatus.popularity.ToString()
            //    || request.SortColumnName == FilterStatus.location.ToString())
            //{
            //    search = db.VHomeListingSearch.Where(query)
            //        .OrderByDescending(c => c.IsPremium)
            //        .OrderByDescending(c=>c.AvgRating)
            //        .Skip(offset)
            //        .Take(request.PageSize)
            //        .ToList();
            //}
            //else if(request.SortColumnName == FilterStatus.openNow.ToString())
            //{
            //    search = db.VHomeListingSearch.Where(query)
            //        .Skip(offset)
            //        .Take(request.PageSize)
            //        .ToList();
            //}
            //else if (request.SortColumnName == FilterStatus.ratings.ToString())
            //{
            //    if(request.Ratingstatus == "Asc")
            //    {
            //        search = db.VHomeListingSearch.Where(query)
            //        .OrderByDescending(c => c.AvgRating)
            //        .OrderByDescending(c => c.IsPremium)
            //        .Skip(offset)
            //        .Take(request.PageSize)
            //        .ToList();
            //    }
            //    else
            //    {
            //       search = db.VHomeListingSearch.Where(query)
            //       .OrderBy(c => c.AvgRating)
            //       .Skip(offset)
            //       .Take(request.PageSize)
            //       .ToList();
            //    }
            //}
            return new HomeListingResponse
            {
                CtName = request.CtName,
                //CatId = db.SubMenuCategory.Where(c => c.Id == request.SbCId).FirstOrDefault().MainCategoryId,
                CatId = db.SubMenuCategory.Where(c => c.Id == request.SbCId).FirstOrDefault() != null ? db.SubMenuCategory.Where(c => c.Id == request.SbCId).FirstOrDefault().MainCategoryId : request.SbCId,
                SbCId = request.SbCId,
                SbCName = request.SbCName,
                ArName=request.ArName,
                SortColumnName = request.SortColumnName,
                Ratingstatus = request.Ratingstatus,
                PageNo = request.PageNo,
                PageSize = request.PageSize,
                RowCount = rowCount,
                HomeListingSearch = search
            };
        }

        public SPGetCompnayDetailById GetcompanyDetailbyListingId(decimal ListingId)
        {
            return UserFrontStoreProcedure.SPGetCompnayDetailById(ListingId);
        }

        public CategoryMetasKeywords GetListingMetaDetail(string Location, decimal ListingId)
        {
            CategoryMetasKeywords res = new CategoryMetasKeywords();
            var results = db.CompanyListings.Where(c=> c.Id == ListingId).Select(c => new CategoryMetasKeywords { Id = c.Id, MetaTitle = c.MetaTitle.Replace("*#Cities#*", Location), MetaKeyword = c.MetaKeyword.Replace("*#Cities#*", Location), MetaDescription = c.MetaDescription.Replace("*#Cities#*", Location), Location = Location }).FirstOrDefault();
            return results ?? res;
        }

        public List<CompanyListings> GetCompanyListingsByUserId(decimal CustomerId)
        {
            return DbSet.Where(c => c.CustomerId == CustomerId).Take(7).ToList();
        }

        public bool CheckCustomerListing (decimal CustomerId , decimal ListingId)
        {
            bool Updates = true;
            var Results = DbSet.Where(c => c.CustomerId == CustomerId && c.Id == ListingId).FirstOrDefault();
            if(Results == null)
            {
                Updates = false;
            }
            return Updates;
        }
        public VMAddCompanyListingModel FindRecord(decimal Id, decimal CustomerId)
        {
            VMAddCompanyListingModel lObjListingModel = new VMAddCompanyListingModel();
            lObjListingModel.CompanyListings = DbSet.Where(c=>c.Id==Id && c.CustomerId == CustomerId).FirstOrDefault();
            if(lObjListingModel.CompanyListings!=null)
            { 
            lObjListingModel.CompanyListingProfile = _companyListingProfile.GetByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.ListingAddress = _listingAddressRepository.GetByListingId(lObjListingModel.CompanyListings.Id).FirstOrDefault() ?? null;
            lObjListingModel.ListingCategory = _listingCategoryRepository.GetByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.ListingLandlineNo = _listingLandlineNo.GetByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.ListingMobileNo = _listingMobileNo.GetByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.ListingPaymentsMode = _listingPaymentsMode.GetByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.ListingPremium = _premiumRepository.GetByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.ListingServices = _listingServices.GetByListingId(lObjListingModel.CompanyListings.Id);
            lObjListingModel.ListingSocialMedia = _listingSocialMedia.GetByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.ListingsBusinessTypes = _IListingsBusinessTypesRepository.GetByListingsId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.VerifiedListing = _verifiedListing.GetByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.ListingGallery = _listingGallery.GetByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.CompanyListingTimming = _companyListingTimming.GetTimmingByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            lObjListingModel.CompanyListingRating = _companyListingRatingRepository.FindByListingId(lObjListingModel.CompanyListings.Id) ?? null;
            }
            return lObjListingModel;
        }

        public Sp_GetClientSummaryResults GetClientSummaryResults(decimal CustomerId)
        {
            return UserFrontStoreProcedure.GetClientSummaryResults(CustomerId);
        }

        public string RequestCounters(decimal ListingId)
        {
            var result = DbSet.Where(c => c.Id == ListingId).FirstOrDefault();
            if(result != null)
            {
                if(result.RequestCounter > 0)
                {
                    result.RequestCounter = result.RequestCounter + 1;
                }
                else
                {
                    result.RequestCounter = 1;
                }
                DbSet.Update(result);
                db.SaveChanges();
            }
            return result.CompanyName;
        }

        public decimal Transferupdate(decimal CustomerId, decimal ListingId)
        {
            decimal Results = 0;
            var ListingUpdate = DbSet.Find(ListingId);
            var UserId = db.Customers.Find(CustomerId).UserId;
            var Email = db.AspNetUsers.Find(UserId).Email;
            ListingUpdate.CustomerId = CustomerId;
            ListingUpdate.Email = Email;
            DbSet.Update(ListingUpdate);
            Results = db.SaveChanges();
            return Results;
        }

        #endregion


    }
}
