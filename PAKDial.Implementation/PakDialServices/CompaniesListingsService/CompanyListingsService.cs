using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.RequestModels.CompanyListings;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ResponseModels.CompanyListing;
using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using PAKDial.Interfaces.Repository;
using PAKDial.Interfaces.Repository.Configuration;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.CompaniesListingsService
{
    public class CompanyListingsService : ICompanyListingsService
    {
        #region Prop

        private readonly ICompanyListingRepository _companyListingRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IMainMenuCategoryRepository _IMainMenuCategoryRepository;
        private readonly ISubMenuCategoryRepository _ISubMenuCategoryRepository;
        private readonly IPaymentModesRepository _IPaymentModesRepository;
        private readonly ITypeOfServicesRepository _ITypeOfServicesRepository;
        private readonly ISocialMediaModesRepository _ISocialMediaModesRepository;
        private readonly ICityRepository _ICityRepository;
        private readonly IListingGalleryRepository _listingGalleryRepsitory;
        private readonly IListingsBusinessTypesRepository _IListingsBusinessTypesRepository;
        private readonly IBusinessTypesRepository _IBusinessTypesRepository;
        private readonly IListingMobileNoRepository _IListingMobileNoRepository;
        private readonly IListingLandlineNoRepository _IListingLandlineNoRepository;


        #endregion

        #region Ctor

        public CompanyListingsService(ICompanyListingRepository companyListingRepository, IHostingEnvironment hostingEnvironment,
            IMainMenuCategoryRepository IMainMenuCategoryRepository,
            ISubMenuCategoryRepository ISubMenuCategoryRepository,
            IPaymentModesRepository IPaymentModesRepository,
             ITypeOfServicesRepository ITypeOfServicesRepository,
             ISocialMediaModesRepository ISocialMediaModesRepository,
             ICityRepository ICityRepository,
             IListingGalleryRepository listingGalleryRepsitory,
             IListingsBusinessTypesRepository IListingsBusinessTypesRepository,
             IBusinessTypesRepository IBusinessTypesRepository,
             IListingMobileNoRepository IListingMobileNoRepository,
              IListingLandlineNoRepository IListingLandlineNoRepository)
        {
            _companyListingRepository = companyListingRepository;
            this.hostingEnvironment = hostingEnvironment;
            _IMainMenuCategoryRepository = IMainMenuCategoryRepository;
            _ISubMenuCategoryRepository = ISubMenuCategoryRepository;
            _IPaymentModesRepository = IPaymentModesRepository;
            _ITypeOfServicesRepository = ITypeOfServicesRepository;
            _ISocialMediaModesRepository = ISocialMediaModesRepository;
            _ICityRepository = ICityRepository;
            _listingGalleryRepsitory = listingGalleryRepsitory;
            _IListingsBusinessTypesRepository = IListingsBusinessTypesRepository;
            _IBusinessTypesRepository =IBusinessTypesRepository;
            _IListingMobileNoRepository = IListingMobileNoRepository;
            _IListingLandlineNoRepository = IListingLandlineNoRepository;

        }
        #endregion

        #region ServiceMethod

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Instance"></param>
        /// <returns></returns>
        public decimal AddCompanyListing(VMAddCompanyListingModel Instance)
        {
            return _companyListingRepository.AddCompanyListing(Instance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GetAddCompanyListingResponse GetCompanyListingWrapperList(string UserId)
        {
            return _companyListingRepository.GetCompanyListingWrapperList(UserId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public decimal DeleteCompanyListings(decimal Id)
        {
            return _companyListingRepository.DeleteCompanyListings(Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VMAddCompanyListingModel FindRecord(decimal Id)
        {
            return _companyListingRepository.FindRecord(Id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CompanyListings> GetAll()
        {
            return _companyListingRepository.GetAll();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CompanyListingsResponse GetCompanyListings(CompanyListingsRequestModel request)
        {
            return _companyListingRepository.GetCompanyListings(request);
        }
        public CompanyListingsResponse GetClientCompanyListings(CompanyListingsRequestModel request)
        {
            return _companyListingRepository.GetClientCompanyListings(request);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Instance"></param>
        /// <returns></returns>
        public decimal UpdateCompanyListing(VMAddCompanyListingModel Instance)
        {
            return _companyListingRepository.UpdateCompanyListing(Instance);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="file"></param>
        /// <param name="AbsolutePath"></param>
        /// <returns></returns>
        public int UploadBannerImage(decimal Id, IFormFile file, string AbsolutePath)
        {
            int Save = 0;
            var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
            var Bannerpath = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","ListingCompany","BannerImages",Id.ToString()
                    });
            if (Directory.Exists(Bannerpath))
            {
                Directory.Delete(Bannerpath, true);
            }
            var path = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","ListingCompany","BannerImages",Id.ToString(),
                           Guid.NewGuid() + filename.Trim()
                    });

            //Zain
            ImageCodecInfo codecInfo = ImageCodecInfo.GetImageEncoders()
               .Where(r => r.CodecName.ToUpperInvariant().Contains("JPEG") || r.CodecName.ToUpperInvariant().Contains("PNG"))
               .Select(r => r).FirstOrDefault();

            Directory.CreateDirectory(Path.GetDirectoryName(path));
            //Zain
            var encoder = System.Drawing.Imaging.Encoder.Quality;
            var parameters = new EncoderParameters(1);
            var parameter = new EncoderParameter(encoder, 40L);
            parameters.Param[0] = parameter;

            using (FileStream fs = System.IO.File.Create(path))
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyToAsync(memoryStream);
                    using (var img = Image.FromStream(memoryStream))
                    {
                        img.Save(fs, codecInfo, parameters);

                        fs.Flush();
                    }
                }

                //file.CopyTo(fs);
                //fs.Flush();
            }
            path = path.Replace(hostingEnvironment.WebRootPath, "").Replace("\\", "/");
            if (!string.IsNullOrEmpty(path))
            {
                _companyListingRepository.UploadImages(Id, path, AbsolutePath + path);
                Save = _companyListingRepository.SaveChanges();
            }
            return Save;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="file"></param>
        /// <param name="AbsolutePath"></param>
        /// <returns></returns>
        public decimal UploadGalleryImage(IFormFileCollection file, string AbsolutePath, ListingGallery Entity)
        {
            decimal Save = 0;
            List<ListingGallery> lLstListingGallery = new List<ListingGallery>();
            foreach (var item in file)
            {
                var filename = ContentDispositionHeaderValue
                                    .Parse(item.ContentDisposition)
                                    .FileName
                                    .Trim('"');
                var Bannerpath = Path.Combine(new string[]
                        {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","ListingCompany","GalleryImages",Entity.ListingId.ToString()
                        });
                //if (Directory.Exists(Bannerpath))
                //{
                //    Directory.Delete(Bannerpath, true);
                //}
                var path = Path.Combine(new string[]
                        {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","ListingCompany","GalleryImages",Entity.ListingId.ToString(),
                           Guid.NewGuid() + filename.Trim()
                        });
                ImageCodecInfo codecInfo = ImageCodecInfo.GetImageEncoders()
               .Where(r => r.CodecName.ToUpperInvariant().Contains("JPEG") || r.CodecName.ToUpperInvariant().Contains("PNG"))
               .Select(r => r).FirstOrDefault();
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                var encoder = System.Drawing.Imaging.Encoder.Quality;
                var parameters = new EncoderParameters(1);
                var parameter = new EncoderParameter(encoder, 40L);
                parameters.Param[0] = parameter;
                using (FileStream fs = System.IO.File.Create(path))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        item.CopyToAsync(memoryStream);
                        using (var img = Image.FromStream(memoryStream))
                        {
                            img.Save(fs, codecInfo, parameters);

                            fs.Flush();
                        }
                    }
                    // item.CopyTo(fs);
                    //fs.Flush();
                }
                path = path.Replace(hostingEnvironment.WebRootPath, "").Replace("\\", "/");
                if (!string.IsNullOrEmpty(path))
                {
                    
                    ListingGallery lObjListingGallery = new ListingGallery()
                    {
                        ListingId = Entity.ListingId,
                        FileUrl = AbsolutePath + path,
                        UploadDir = path,
                        CreatedBy = Entity.CreatedBy,
                        CreatedDate = Entity.CreatedDate,
                        UpdatedBy = Entity.UpdatedBy,
                        UpdatedDate = Entity.UpdatedDate,
                        Id = Entity.Id,
                        FileName = item.FileName,
                        FileType = item.ContentType
                    };

                    lLstListingGallery.Add(lObjListingGallery);

                }

            }
            Save = _listingGalleryRepsitory.AddGalleryImages(lLstListingGallery);
            return Save;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VMCompanyListings FindRecord_2(decimal Id)
        {
            return _companyListingRepository.FindRecord_2(Id);
            //VMCompanyListings _VMCompanyListings = new VMCompanyListings();
            //if(lObjVMCompanyListings.CompanyListings!=null)
            //{ 
            //VMCompanyListing _VMCompanyListing = new VMCompanyListing
            //{
            //    Id = lObjVMCompanyListings.CompanyListings.Id,
            //    BannerImage = lObjVMCompanyListings.CompanyListings.BannerImage,
            //    BannerImageUrl = lObjVMCompanyListings.CompanyListings.BannerImageUrl,
            //    CompanyName = lObjVMCompanyListings.CompanyListings.CompanyName,
            //    Email = lObjVMCompanyListings.CompanyListings.Email,
            //    FirstName = lObjVMCompanyListings.CompanyListings.FirstName,
            //    LastName = lObjVMCompanyListings.CompanyListings.LastName,
            //    ListingStatus = lObjVMCompanyListings.CompanyListings.ListingStatus,
            //    ListingTypeId = lObjVMCompanyListings.CompanyListings.ListingTypeId,
            //    MetaDescription = lObjVMCompanyListings.CompanyListings.MetaDescription,
            //    MetaKeyword = lObjVMCompanyListings.CompanyListings.MetaKeyword,
            //    MetaTitle = lObjVMCompanyListings.CompanyListings.MetaTitle,
            //    OtpCode = lObjVMCompanyListings.CompanyListings.OtpCode,
            //    UpdatedBy = lObjVMCompanyListings.CompanyListings.UpdatedBy,
            //    UpdatedDate = lObjVMCompanyListings.CompanyListings.UpdatedDate,
            //    Website = lObjVMCompanyListings.CompanyListings.Website,
            //    CreatedBy = lObjVMCompanyListings.CompanyListings.CreatedBy,
            //    CreatedDate = lObjVMCompanyListings.CompanyListings.CreatedDate,
            //};
            //_VMCompanyListings.CompanyListings = _VMCompanyListing;
            //if (lObjVMCompanyListings.CompanyListingProfile != null)
            //{
            //    VMCompanyListingProfile _VMCompanyListingProfile = new VMCompanyListingProfile()
            //    {
            //        Id = lObjVMCompanyListings.CompanyListingProfile.Id,
            //        ListingId = lObjVMCompanyListings.CompanyListingProfile.ListingId,
            //        AnnualTurnOver = lObjVMCompanyListings.CompanyListingProfile.AnnualTurnOver,
            //        BriefAbout = lObjVMCompanyListings.CompanyListingProfile.BriefAbout,
            //        Certification = lObjVMCompanyListings.CompanyListingProfile.Certification,
            //        CreatedBy = lObjVMCompanyListings.CompanyListingProfile.CreatedBy,
            //        CreatedDate = lObjVMCompanyListings.CompanyListingProfile.CreatedDate,
            //        LocationOverview = lObjVMCompanyListings.CompanyListingProfile.LocationOverview,
            //        NumberofEmployees = lObjVMCompanyListings.CompanyListingProfile.NumberofEmployees,
            //        ProductAndServices = lObjVMCompanyListings.CompanyListingProfile.ProductAndServices,
            //        ProfessionalAssociation = lObjVMCompanyListings.CompanyListingProfile.ProfessionalAssociation,
            //        UpdatedBy = lObjVMCompanyListings.CompanyListingProfile.UpdatedBy,
            //        UpdatedDate = lObjVMCompanyListings.CompanyListingProfile.UpdatedDate,
            //        YearEstablished = lObjVMCompanyListings.CompanyListingProfile.YearEstablished
            //    };

            //    _VMCompanyListings.CompanyListingProfile = _VMCompanyListingProfile;
            //}
            //if (lObjVMCompanyListings.ListingAddress != null)
            //{
            //    VMListingAddress _VMListingAddress = new VMListingAddress()
            //    {
            //        Area = lObjVMCompanyListings.ListingAddress.Area,
            //        UpdatedDate = lObjVMCompanyListings.ListingAddress.UpdatedDate,
            //        BuildingAddress = lObjVMCompanyListings.ListingAddress.BuildingAddress,
            //        CityId = lObjVMCompanyListings.ListingAddress.CityId,
            //        CountryId = lObjVMCompanyListings.ListingAddress.CountryId,
            //        CityAreaId = lObjVMCompanyListings.ListingAddress.CityAreaId,
            //        CityAreaName = lObjVMCompanyListings.ListingAddress.CityAreaName,
            //        CreatedBy = lObjVMCompanyListings.ListingAddress.CreatedBy,
            //        CreatedDate = lObjVMCompanyListings.ListingAddress.CreatedDate,
            //        LandMark = lObjVMCompanyListings.ListingAddress.LandMark,
            //        Id = lObjVMCompanyListings.ListingAddress.Id,
            //        CityName = _ICityRepository.Find(lObjVMCompanyListings.ListingAddress.CityId).Name,
            //        Latitude = lObjVMCompanyListings.ListingAddress.Latitude,
            //        LatLogAddress = lObjVMCompanyListings.ListingAddress.LatLogAddress,
            //        ListingId = lObjVMCompanyListings.ListingAddress.ListingId,
            //        Longitude = lObjVMCompanyListings.ListingAddress.Longitude,
            //        StateId = lObjVMCompanyListings.ListingAddress.StateId,
            //        StreetAddress = lObjVMCompanyListings.ListingAddress.StreetAddress,
            //        UpdateBy = lObjVMCompanyListings.ListingAddress.UpdateBy
            //    };
            //    _VMCompanyListings.ListingAddress = _VMListingAddress;
            //}
            //if (lObjVMCompanyListings.CompanyListingTimming != null)
            //{
            //    foreach (var item in lObjVMCompanyListings.CompanyListingTimming)
            //    {
            //        VMCompanyListingTimming _VMCompanyListingTimming = new VMCompanyListingTimming()
            //        {
            //            CreatedBy = item.CreatedBy,
            //            CreatedDate = item.CreatedDate,
            //            DaysName = item.DaysName,
            //            Id = item.Id,
            //            IsClosed = item.IsClosed,
            //            ListingId = item.ListingId,
            //            TimeFrom = item.TimeFrom,
            //            TimeTo = item.TimeTo,
            //            UpdatedBy = item.UpdatedBy,
            //            UpdatedDate = item.UpdatedDate,
            //            WeekDayNo = item.WeekDayNo
            //        };
            //        _VMCompanyListings.CompanyListingTimming.Add(_VMCompanyListingTimming);
            //    }

            //}
            //if (lObjVMCompanyListings.ListingCategory != null)
            //{
            //    foreach (var lObjListingCategory in lObjVMCompanyListings.ListingCategory)
            //    {
            //        VMListingCategory _VMListingCategory = new VMListingCategory()
            //        {
            //            CreatedBy = lObjListingCategory.CreatedBy,
            //            CreatedDate = lObjListingCategory.CreatedDate,
            //            Id = lObjListingCategory.Id,
            //            ListingId = lObjListingCategory.ListingId,
            //            MainCategoryId = lObjListingCategory.MainCategoryId,
            //            SubCategoryId = lObjListingCategory.SubCategoryId,
            //            SubCategoryName = _ISubMenuCategoryRepository.Find(lObjListingCategory.SubCategoryId).Name,
            //            MainCategoryName = _IMainMenuCategoryRepository.Find(lObjListingCategory.MainCategoryId).Name,
            //            UpdatedBy = lObjListingCategory.UpdatedBy,
            //            UpdatedDate = lObjListingCategory.UpdatedDate,

            //        };
            //        _VMCompanyListings.ListingCategory.Add(_VMListingCategory);
            //    }
            //}
            //if (lObjVMCompanyListings.ListingGallery != null)
            //{
            //    foreach (var item in lObjVMCompanyListings.ListingGallery)
            //    {
            //        VMListingGallery _VMListingGallery = new VMListingGallery()
            //        {
            //            CreatedBy = item.CreatedBy,
            //            CreatedDate = item.CreatedDate,
            //            FileName = item.FileName,
            //            UpdatedDate = item.UpdatedDate,
            //            FileType = item.FileType,
            //            FileUrl = item.FileUrl,
            //            Id = item.Id,
            //            ListingId = item.ListingId,
            //            UpdatedBy = item.UpdatedBy,
            //            UploadDir = item.UploadDir,
                        

            //        };
            //        _VMCompanyListings.ListingGallery.Add(_VMListingGallery);

            //    }
            //}
            //if (lObjVMCompanyListings.ListingLandlineNo != null)
            //{
            //    foreach (var item in lObjVMCompanyListings.ListingLandlineNo)
            //    {
            //        VMListingLandlineNo _VMListingLandlineNo = new VMListingLandlineNo()
            //        {
            //            CreatedBy = item.CreatedBy,
            //            CreatedDate = item.CreatedDate,
            //            Id = item.Id,
            //            LandlineNumber = item.LandlineNumber,
            //            ListingId = item.ListingId,
            //            UpdatedBy = item.UpdatedBy,
            //            UpdatedDate = item.UpdatedDate

            //        };
            //        _VMCompanyListings.ListingLandlineNo.Add(_VMListingLandlineNo);
            //    }
            //}
            //if (lObjVMCompanyListings.ListingMobileNo != null)
            //{
            //    foreach (var item in lObjVMCompanyListings.ListingMobileNo)
            //    {
            //        VMListingMobileNo _VMListingMobileNo = new VMListingMobileNo()
            //        {
            //            CreatedBy = item.CreatedBy,
            //            CreatedDate = item.CreatedDate,
            //            Id = item.Id,
            //            ListingId = item.ListingId,
            //            MobileNo = item.MobileNo,
            //            OptCode = item.OptCode,
            //            UpdatedBy = item.UpdatedBy,
            //            UpdatedDate = item.UpdatedDate

            //        };
            //        _VMCompanyListings.ListingMobileNo.Add(_VMListingMobileNo);
            //    }

            //}
            //if (lObjVMCompanyListings.ListingPaymentsMode != null)
            //{
            //    foreach (var item in lObjVMCompanyListings.ListingPaymentsMode)
            //    {
            //        VMListingPaymentsMode _VMListingPaymentsMode = new VMListingPaymentsMode()
            //        {
            //            CreatedBy = item.CreatedBy,
            //            CreatedDate = item.CreatedDate,
            //            Id = item.Id,
            //            ListingId = item.ListingId,
            //            ModeId = item.ModeId,
            //            ModeName = _IPaymentModesRepository.Find(item.ModeId).Name,
            //            UpdateDate = item.UpdateDate,
            //            UpdatedBy = item.UpdatedBy
            //        };
            //        _VMCompanyListings.ListingPaymentsMode.Add(_VMListingPaymentsMode);
            //    }
            //}
            //if (lObjVMCompanyListings.ListingsBusinessTypes != null)
            //{
            //    foreach (var item in lObjVMCompanyListings.ListingsBusinessTypes)
            //    {
            //        VMListingsBusinessTypes _VMListingsBusinessTypes = new VMListingsBusinessTypes()
            //        {
            //            CreatedBy = item.CreatedBy,
            //            CreatedDate = item.CreatedDate,
            //            Id = item.Id,
            //            BusinessId = item.BusinessId,
            //            Text = _IBusinessTypesRepository.Find(item.BusinessId).Name,
            //            ListingId = item.ListingId,
            //            UpdatedDate = item.UpdatedDate,
            //            UpdatedBy = item.UpdatedBy
            //        };
            //        _VMCompanyListings. ListingsBusinessTypes.Add(_VMListingsBusinessTypes);
            //    }
            //}
            //if (lObjVMCompanyListings.ListingServices != null)
            //{
            //    foreach (var item in lObjVMCompanyListings.ListingServices)
            //    {
            //        VMListingServices _VMListingServices = new VMListingServices()
            //        {
            //            CreatedBy = item.CreatedBy,
            //            CreatedDate = item.CreatedDate,
            //            Id = item.Id,
            //            ListingId = item.ListingId,
            //            ServiceName = _ITypeOfServicesRepository.Find(item.ServiceTypeId).Name,
            //            ServiceTypeId = item.ServiceTypeId,
            //            UpdatedBy = item.UpdatedBy,
            //            UpdatedDate = item.UpdatedDate
            //        };
            //        _VMCompanyListings.ListingServices.Add(_VMListingServices);
            //    }
            //}
            //if (lObjVMCompanyListings.ListingSocialMedia != null)
            //{
            //    foreach (var item in lObjVMCompanyListings.ListingSocialMedia)
            //    {
            //        VMSocialMediaModes _VMSocialMediaModes = new VMSocialMediaModes()
            //        {
            //            CreatedBy = item.CreatedBy,
            //            CreatedDate = item.CreatedDate,
            //            Id = item.Id,
            //            MediaId = item.MediaId,
            //            Name = _ISocialMediaModesRepository.Find(item.MediaId).Name,
            //            SitePath = item.SitePath,
            //            Imagedir = _ISocialMediaModesRepository.Find(item.MediaId).ImageDir,
            //            ImageUrl = _ISocialMediaModesRepository.Find(item.MediaId).ImageUrl,

            //        };
            //        _VMCompanyListings.SocialMediaModes.Add(_VMSocialMediaModes);
            //    }
            //}
            //}
            //return _VMCompanyListings;

        }

        public decimal VerifyUnVerifyListing(decimal Id, string name, string CreatedBy, DateTime CreatedDate)
        {
            return _companyListingRepository.VerifyUnVerifyListing(Id, name, CreatedBy, CreatedDate);
        }

        public decimal TransferCustomerOwnerShip(decimal CustomerId, decimal ListingId)
        {
            return _companyListingRepository.Transferupdate(CustomerId, ListingId);
        }

        public VMCompanyListing FindById(decimal ListingId)
        {
            var GetById = _companyListingRepository.Find(ListingId);
            VMCompanyListing listing = new VMCompanyListing();
            if (GetById !=null)
            {
                listing.Id = GetById.Id;
                listing.CompanyName = GetById.CompanyName;
                listing.FirstName = GetById.FirstName;
                listing.LastName = GetById.LastName;
            }
            return listing;
        }

        public decimal ActiveInActiveListUpdate(decimal Id, string Value,string UserId)
        {
            decimal Result = 0;
            var lObjCompanyListngs = _companyListingRepository.Find(Id);
            if (Value == "Active")
            {
                lObjCompanyListngs.ListingStatus = false;
                lObjCompanyListngs.UpdatedBy = UserId;
                lObjCompanyListngs.UpdatedDate = DateTime.Now;
            }
            else
            {
                lObjCompanyListngs.ListingStatus = true;
                lObjCompanyListngs.UpdatedBy = UserId;
                lObjCompanyListngs.UpdatedDate = DateTime.Now;
            }

            Result = _companyListingRepository.ActiveInActiveListUpdate(lObjCompanyListngs);
            return Result;

        }
        public decimal DeleteBannerImage(decimal Id)
        {
            var path = Path.Combine(new string[]
                        {
                           hostingEnvironment.WebRootPath

                        });
            return _companyListingRepository.DeleteBannerImage(Id, path);
        }

        public GetCompanyKeyValueSearchResponse GetCompanySearchOnAreaBases(CompanyKeyValueSearchRequestModel request)
        {
            return _companyListingRepository.GetCompanySearchOnAreaBases(request);
        }

        public List<ListingMobileNo>   CheckMobileNo(string MobileNo, int Id)
        {
             return _IListingMobileNoRepository.GetListingMobileNoByName(MobileNo, Id);
            
        }

        public List<ListingLandlineNo> CheckLandlineNo(string LandlineNo, int Id)
        {
            return _IListingLandlineNoRepository.GetListingLandlineNoByName(LandlineNo, Id);
        }

        public List<CompanyListings> GetCompanyListingsByUserId(decimal CustomerId)
        {
              return _companyListingRepository.GetCompanyListingsByUserId(CustomerId);
        }

        public GetAddCompanyListingResponse GetClientCompanyListingWrapperList()
        {
            return _companyListingRepository.GetClientCompanyListingWrapperList();
        }

        public VMCompanyListings FindRecord_2(decimal Id, decimal CustomerId)
        {
            var lObjVMCompanyListings = _companyListingRepository.FindRecord(Id, CustomerId);

            VMCompanyListings _VMCompanyListings = new VMCompanyListings();
            if (lObjVMCompanyListings.CompanyListings != null)
            {
                VMCompanyListing _VMCompanyListing = new VMCompanyListing
                {
                    Id = lObjVMCompanyListings.CompanyListings.Id,
                    BannerImage = lObjVMCompanyListings.CompanyListings.BannerImage,
                    BannerImageUrl = lObjVMCompanyListings.CompanyListings.BannerImageUrl,
                    CompanyName = lObjVMCompanyListings.CompanyListings.CompanyName,
                    Email = lObjVMCompanyListings.CompanyListings.Email,
                    FirstName = lObjVMCompanyListings.CompanyListings.FirstName,
                    LastName = lObjVMCompanyListings.CompanyListings.LastName,
                    ListingStatus = lObjVMCompanyListings.CompanyListings.ListingStatus,
                    ListingTypeId = lObjVMCompanyListings.CompanyListings.ListingTypeId,
                    MetaDescription = lObjVMCompanyListings.CompanyListings.MetaDescription,
                    MetaKeyword = lObjVMCompanyListings.CompanyListings.MetaKeyword,
                    MetaTitle = lObjVMCompanyListings.CompanyListings.MetaTitle,
                    OtpCode = lObjVMCompanyListings.CompanyListings.OtpCode,
                    UpdatedBy = lObjVMCompanyListings.CompanyListings.UpdatedBy,
                    UpdatedDate = lObjVMCompanyListings.CompanyListings.UpdatedDate,
                    Website = lObjVMCompanyListings.CompanyListings.Website,
                    CreatedBy = lObjVMCompanyListings.CompanyListings.CreatedBy,
                    CreatedDate = lObjVMCompanyListings.CompanyListings.CreatedDate,
                };
                _VMCompanyListings.CompanyListings = _VMCompanyListing;
                if (lObjVMCompanyListings.CompanyListingProfile != null)
                {
                    VMCompanyListingProfile _VMCompanyListingProfile = new VMCompanyListingProfile()
                    {
                        Id = lObjVMCompanyListings.CompanyListingProfile.Id,
                        ListingId = lObjVMCompanyListings.CompanyListingProfile.ListingId,
                        AnnualTurnOver = lObjVMCompanyListings.CompanyListingProfile.AnnualTurnOver,
                        BriefAbout = lObjVMCompanyListings.CompanyListingProfile.BriefAbout,
                        Certification = lObjVMCompanyListings.CompanyListingProfile.Certification,
                        CreatedBy = lObjVMCompanyListings.CompanyListingProfile.CreatedBy,
                        CreatedDate = lObjVMCompanyListings.CompanyListingProfile.CreatedDate,
                        LocationOverview = lObjVMCompanyListings.CompanyListingProfile.LocationOverview,
                        NumberofEmployees = lObjVMCompanyListings.CompanyListingProfile.NumberofEmployees,
                        ProductAndServices = lObjVMCompanyListings.CompanyListingProfile.ProductAndServices,
                        ProfessionalAssociation = lObjVMCompanyListings.CompanyListingProfile.ProfessionalAssociation,
                        UpdatedBy = lObjVMCompanyListings.CompanyListingProfile.UpdatedBy,
                        UpdatedDate = lObjVMCompanyListings.CompanyListingProfile.UpdatedDate,
                        YearEstablished = lObjVMCompanyListings.CompanyListingProfile.YearEstablished
                    };

                    _VMCompanyListings.CompanyListingProfile = _VMCompanyListingProfile;
                }
                if (lObjVMCompanyListings.ListingAddress != null)
                {
                    VMListingAddress _VMListingAddress = new VMListingAddress()
                    {
                        Area = lObjVMCompanyListings.ListingAddress.Area,
                        UpdatedDate = lObjVMCompanyListings.ListingAddress.UpdatedDate,
                        BuildingAddress = lObjVMCompanyListings.ListingAddress.BuildingAddress,
                        CityId = lObjVMCompanyListings.ListingAddress.CityId,
                        CountryId = lObjVMCompanyListings.ListingAddress.CountryId,
                        CityAreaId = lObjVMCompanyListings.ListingAddress.CityAreaId,
                        CreatedBy = lObjVMCompanyListings.ListingAddress.CreatedBy,
                        CreatedDate = lObjVMCompanyListings.ListingAddress.CreatedDate,
                        LandMark = lObjVMCompanyListings.ListingAddress.LandMark,
                        Id = lObjVMCompanyListings.ListingAddress.Id,
                        CityName = _ICityRepository.Find(lObjVMCompanyListings.ListingAddress.CityId).Name,
                        Latitude = lObjVMCompanyListings.ListingAddress.Latitude,
                        LatLogAddress = lObjVMCompanyListings.ListingAddress.LatLogAddress,
                        ListingId = lObjVMCompanyListings.ListingAddress.ListingId,
                        Longitude = lObjVMCompanyListings.ListingAddress.Longitude,
                        StateId = lObjVMCompanyListings.ListingAddress.StateId,
                        StreetAddress = lObjVMCompanyListings.ListingAddress.StreetAddress,
                        UpdateBy = lObjVMCompanyListings.ListingAddress.UpdateBy
                    };
                    _VMCompanyListings.ListingAddress = _VMListingAddress;
                }
                if (lObjVMCompanyListings.CompanyListingTimming != null)
                {
                    foreach (var item in lObjVMCompanyListings.CompanyListingTimming)
                    {
                        VMCompanyListingTimming _VMCompanyListingTimming = new VMCompanyListingTimming()
                        {
                            CreatedBy = item.CreatedBy,
                            CreatedDate = item.CreatedDate,
                            DaysName = item.DaysName,
                            Id = item.Id,
                            IsClosed = item.IsClosed,
                            ListingId = item.ListingId,
                            TimeFrom = item.TimeFrom,
                            TimeTo = item.TimeTo,
                            UpdatedBy = item.UpdatedBy,
                            UpdatedDate = item.UpdatedDate,
                            WeekDayNo = item.WeekDayNo
                        };
                        _VMCompanyListings.CompanyListingTimming.Add(_VMCompanyListingTimming);
                    }

                }
                if (lObjVMCompanyListings.ListingCategory != null)
                {
                    foreach (var lObjListingCategory in lObjVMCompanyListings.ListingCategory)
                    {
                        VMListingCategory _VMListingCategory = new VMListingCategory()
                        {
                            CreatedBy = lObjListingCategory.CreatedBy,
                            CreatedDate = lObjListingCategory.CreatedDate,
                            Id = lObjListingCategory.Id,
                            ListingId = lObjListingCategory.ListingId,
                            MainCategoryId = lObjListingCategory.MainCategoryId,
                            SubCategoryId = lObjListingCategory.SubCategoryId,
                            SubCategoryName = _ISubMenuCategoryRepository.Find(lObjListingCategory.SubCategoryId).Name,
                            MainCategoryName = _IMainMenuCategoryRepository.Find(lObjListingCategory.MainCategoryId).Name,
                            UpdatedBy = lObjListingCategory.UpdatedBy,
                            UpdatedDate = lObjListingCategory.UpdatedDate,

                        };
                        _VMCompanyListings.ListingCategory.Add(_VMListingCategory);
                    }
                }
                if (lObjVMCompanyListings.ListingGallery != null)
                {
                    foreach (var item in lObjVMCompanyListings.ListingGallery)
                    {
                        VMListingGallery _VMListingGallery = new VMListingGallery()
                        {
                            CreatedBy = item.CreatedBy,
                            CreatedDate = item.CreatedDate,
                            FileName = item.FileName,
                            UpdatedDate = item.UpdatedDate,
                            FileType = item.FileType,
                            FileUrl = item.FileUrl,
                            Id = item.Id,
                            ListingId = item.ListingId,
                            UpdatedBy = item.UpdatedBy,
                            UploadDir = item.UploadDir,


                        };
                        _VMCompanyListings.ListingGallery.Add(_VMListingGallery);

                    }
                }
                if (lObjVMCompanyListings.ListingLandlineNo != null)
                {
                    foreach (var item in lObjVMCompanyListings.ListingLandlineNo)
                    {
                        VMListingLandlineNo _VMListingLandlineNo = new VMListingLandlineNo()
                        {
                            CreatedBy = item.CreatedBy,
                            CreatedDate = item.CreatedDate,
                            Id = item.Id,
                            LandlineNumber = item.LandlineNumber,
                            ListingId = item.ListingId,
                            UpdatedBy = item.UpdatedBy,
                            UpdatedDate = item.UpdatedDate

                        };
                        _VMCompanyListings.ListingLandlineNo.Add(_VMListingLandlineNo);
                    }
                }
                if (lObjVMCompanyListings.ListingMobileNo != null)
                {
                    foreach (var item in lObjVMCompanyListings.ListingMobileNo)
                    {
                        VMListingMobileNo _VMListingMobileNo = new VMListingMobileNo()
                        {
                            CreatedBy = item.CreatedBy,
                            CreatedDate = item.CreatedDate,
                            Id = item.Id,
                            ListingId = item.ListingId,
                            MobileNo = item.MobileNo,
                            OptCode = item.OptCode,
                            UpdatedBy = item.UpdatedBy,
                            UpdatedDate = item.UpdatedDate

                        };
                        _VMCompanyListings.ListingMobileNo.Add(_VMListingMobileNo);
                    }

                }
                if (lObjVMCompanyListings.ListingPaymentsMode != null)
                {
                    foreach (var item in lObjVMCompanyListings.ListingPaymentsMode)
                    {
                        VMListingPaymentsMode _VMListingPaymentsMode = new VMListingPaymentsMode()
                        {
                            CreatedBy = item.CreatedBy,
                            CreatedDate = item.CreatedDate,
                            Id = item.Id,
                            ListingId = item.ListingId,
                            ModeId = item.ModeId,
                            ModeName = _IPaymentModesRepository.Find(item.ModeId).Name,
                            UpdateDate = item.UpdateDate,
                            UpdatedBy = item.UpdatedBy
                        };
                        _VMCompanyListings.ListingPaymentsMode.Add(_VMListingPaymentsMode);
                    }
                }
                if (lObjVMCompanyListings.ListingsBusinessTypes != null)
                {
                    foreach (var item in lObjVMCompanyListings.ListingsBusinessTypes)
                    {
                        VMListingsBusinessTypes _VMListingsBusinessTypes = new VMListingsBusinessTypes()
                        {
                            CreatedBy = item.CreatedBy,
                            CreatedDate = item.CreatedDate,
                            Id = item.Id,
                            BusinessId = item.BusinessId,
                            Text = _IBusinessTypesRepository.Find(item.BusinessId).Name,
                            ListingId = item.ListingId,
                            UpdatedDate = item.UpdatedDate,
                            UpdatedBy = item.UpdatedBy
                        };
                        _VMCompanyListings.ListingsBusinessTypes.Add(_VMListingsBusinessTypes);
                    }
                }
                if (lObjVMCompanyListings.ListingServices != null)
                {
                    foreach (var item in lObjVMCompanyListings.ListingServices)
                    {
                        VMListingServices _VMListingServices = new VMListingServices()
                        {
                            CreatedBy = item.CreatedBy,
                            CreatedDate = item.CreatedDate,
                            Id = item.Id,
                            ListingId = item.ListingId,
                            ServiceName = _ITypeOfServicesRepository.Find(item.ServiceTypeId).Name,
                            ServiceTypeId = item.ServiceTypeId,
                            UpdatedBy = item.UpdatedBy,
                            UpdatedDate = item.UpdatedDate
                        };
                        _VMCompanyListings.ListingServices.Add(_VMListingServices);
                    }
                }
                if (lObjVMCompanyListings.ListingSocialMedia != null)
                {
                    foreach (var item in lObjVMCompanyListings.ListingSocialMedia)
                    {
                        VMSocialMediaModes _VMSocialMediaModes = new VMSocialMediaModes()
                        {
                            CreatedBy = item.CreatedBy,
                            CreatedDate = item.CreatedDate,
                            Id = item.Id,
                            MediaId = item.MediaId,
                            Name = _ISocialMediaModesRepository.Find(item.MediaId).Name,
                            SitePath = item.SitePath,
                            Imagedir = _ISocialMediaModesRepository.Find(item.MediaId).ImageDir,
                            ImageUrl = _ISocialMediaModesRepository.Find(item.MediaId).ImageUrl,

                        };
                        _VMCompanyListings.SocialMediaModes.Add(_VMSocialMediaModes);
                    }
                }
            }
            return _VMCompanyListings;
        }

        public Sp_GetClientSummaryResults GetClientSummaryResults(decimal CustomerId)
        {
            return _companyListingRepository.GetClientSummaryResults(CustomerId);
        }

        public bool CheckCustomerListing(decimal CustomerId, decimal ListingId)
        {
            return _companyListingRepository.CheckCustomerListing(CustomerId,ListingId);
        }

        public void RequestCounters(decimal ListingId)
        {
            _companyListingRepository.RequestCounters(ListingId);
        }

        #endregion

    }
}
