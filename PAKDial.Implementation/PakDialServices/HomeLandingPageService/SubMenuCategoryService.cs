using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Interfaces.PakDialServices.IHomeLandingPageService;
using PAKDial.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace PAKDial.Implementation.PakDialServices.HomeLandingPageService
{
    public class SubMenuCategoryService : ISubMenuCategoryService
    {
        private readonly ISubMenuCategoryRepository subMenuCategoryRepository;
        private readonly IMainMenuCategoryRepository mainMenuCategory;
        private readonly IHostingEnvironment hostingEnvironment;


        public SubMenuCategoryService(ISubMenuCategoryRepository subMenuCategoryRepository, IMainMenuCategoryRepository mainMenuCategory
            , IHostingEnvironment hostingEnvironment)
        {
            this.subMenuCategoryRepository = subMenuCategoryRepository;
            this.mainMenuCategory = mainMenuCategory;
            this.hostingEnvironment = hostingEnvironment;
        }

        public decimal Add(SubMenuCategory instance)
        {
            decimal Save = 0; //"SubMenuCategory Not Added";
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            //var CheckExistance = subMenuCategoryRepository.CheckExistance(instance);
            //if (!CheckExistance)
            //{
            //    Save = subMenuCategoryRepository.AddSubCategory(instance); //1 SubMenuCategory Added Successfully
            //}
            Save = subMenuCategoryRepository.AddSubCategory(instance); //1 SubMenuCategory Added Successfully
            if (Save > 0)
            {
                //return Save;
            }
            else
            {
                Save = -2; // SubMenuCategory Already Exist.
            }
            return Save;
        }

        public int Update(SubMenuCategory instance)
        {
            int Result = 0; //Record Not Updated Successfully
            var TrackIds = instance.TrackIds;
            var TrackNames = instance.TrackNames;
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            //bool result = subMenuCategoryRepository.CheckExistance(instance);
            bool CheckExistance = FindByPSubCategory(instance.Id).Count() > 0 ? true : false;
            if (!CheckExistance) //!result &&
            {
                var SubCat = FindById(instance.Id);
                SubCat.Name = instance.Name;
                SubCat.Description = instance.Description;
                SubCat.MetaTitle = instance.MetaTitle;
                SubCat.MetaKeyword = instance.MetaKeyword;
                SubCat.MetaDescription = instance.MetaDescription;
                SubCat.Icon = instance.Icon;
                SubCat.SubBannerImage = instance.SubBannerImage;
                SubCat.SubFeatureImage = instance.SubFeatureImage;
                SubCat.UpdatedBy = instance.UpdatedBy;
                SubCat.UpdatedDate = instance.UpdatedDate;
                SubCat.IsActive = instance.IsActive;
                SubCat.IsLastNode = instance.IsLastNode;
                SubCat.TrackIds = TrackIds;
                SubCat.TrackNames = TrackNames;
                Result = subMenuCategoryRepository.UpdateSubCategory(SubCat); //Record Updated Successfully
            }
            else if(CheckExistance)
            {
                Result = -3; //"SubCategory Already in Use".
            }
            else
            {
                Result = -2; //"SubMenuCategory Already Exits";
            }
            return Result;
        }

        public int Delete(decimal Id)
        {        
            int Results = 0; //"SubMenuCategory Not Deleted."
            var Bannerpath = Path.Combine(new string[]
                   {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","SubCategory","BannerImages",Id.ToString()
                   });
            var Featurepath = Path.Combine(new string[]
                   {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","SubCategory","FeatureImages",Id.ToString()
                   });
            var WebIconPath = Path.Combine(new string[]
                   {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","SubCategory","WebIcons",Id.ToString()
                   });
            var MobileIconPath = Path.Combine(new string[]
                   {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","SubCategory","MobileIcons",Id.ToString()
                   });
            var checkStates = subMenuCategoryRepository.FindByPSubCategory(Id).Count() > 0 ? true : false;
            if (!checkStates)
            {
                subMenuCategoryRepository.Delete(FindById(Id));
                Results = subMenuCategoryRepository.SaveChanges(); // SubMenuCategory Deleted Successfully.
            }
            else
            {
                Results = -2; // Please Delete its Child Sub Category First.
            }
            if(Results == 1)
            {
                if (Directory.Exists(Bannerpath))
                {
                    Directory.Delete(Bannerpath, true);
                }
                if (Directory.Exists(Featurepath))
                {
                    Directory.Delete(Featurepath, true);
                }
                if (Directory.Exists(WebIconPath))
                {
                    Directory.Delete(WebIconPath, true);
                }
                if (Directory.Exists(MobileIconPath))
                {
                    Directory.Delete(MobileIconPath, true);
                }
            }
            return Results;
        }

        public SubMenuCategory FindById(decimal id)
        {
            return subMenuCategoryRepository.Find(id);
        }

        public List<SubMenuCategory> FindByMainCategory(decimal MainCategoryId)
        {
            return subMenuCategoryRepository.FindByMainCategory(MainCategoryId);
        }

        public List<SubMenuCategory> FindByMPSubCategory(decimal MainCategoryId, decimal ParentSubCategoryId)
        {
            return subMenuCategoryRepository.FindByMPSubCategory(MainCategoryId, ParentSubCategoryId);
        }

        public List<SubMenuCategory> FindByPSubCategory(decimal ParentSubCategoryId)
        {
            return subMenuCategoryRepository.FindByPSubCategory(ParentSubCategoryId);
        }
        public decimal IsPopularCategory(decimal Id, string name)
        {
            return subMenuCategoryRepository.IsPopularCategory(Id,name);
        }

        public IEnumerable<SubMenuCategory> GetAll()
        {
            return subMenuCategoryRepository.GetAll();
        }

        public SubMenuCategoryResponse GetSubMenus(SubMenuCategoryRequestModel request)
        {
            return subMenuCategoryRepository.GetSubMenus(request);
        }
        public GetSubCategoryResponse GetSubMenusList(SubCategoryRequestModel request)
        {
            return subMenuCategoryRepository.GetSubMenusList(request);
        }

        public int UploadCategoryBannerImage(decimal Id, IFormFile file, string AbsolutePath)
        {
            int Save = 0;
            var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
            var Bannerpath = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","SubCategory","BannerImages",Id.ToString()
                    });
            if (Directory.Exists(Bannerpath))
            {
                Directory.Delete(Bannerpath, true);
            }
            var path = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","SubCategory","BannerImages",Id.ToString(),
                           Guid.NewGuid() + filename
                    });

            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (FileStream fs = System.IO.File.Create(path))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            path = path.Replace(hostingEnvironment.WebRootPath, "").Replace("\\", "/");
            if (!string.IsNullOrEmpty(path))
            {
                subMenuCategoryRepository.UploadImages(Id, path, 1, AbsolutePath + path);
                Save = subMenuCategoryRepository.SaveChanges();
            }
            return Save;
        }

        public int UploadCategoryFeatureImage(decimal Id, IFormFile file, string AbsolutePath)
        {
            int Save = 0;
            var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
            var Featurepath = Path.Combine(new string[]
                {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","SubCategory","FeatureImages",Id.ToString()
                });
            if (Directory.Exists(Featurepath))
            {
                Directory.Delete(Featurepath, true);
            }
            var path = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","SubCategory","FeatureImages",Id.ToString(),
                           Guid.NewGuid() + filename
                    });
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (FileStream fs = System.IO.File.Create(path))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            path = path.Replace(hostingEnvironment.WebRootPath, "").Replace("\\", "/");
            if (!string.IsNullOrEmpty(path))
            {
                subMenuCategoryRepository.UploadImages(Id, path, 2, AbsolutePath + path);
                Save = subMenuCategoryRepository.SaveChanges();
            }
            return Save;
        }

        public int UploadWebIcon(decimal Id, IFormFile file, string AbsolutePath)
        {
            int Save = 0;
            var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
            var Bannerpath = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","SubCategory","WebIcons",Id.ToString()
                    });
            if (Directory.Exists(Bannerpath))
            {
                Directory.Delete(Bannerpath, true);
            }
            var path = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","SubCategory","WebIcons",Id.ToString(),
                           Guid.NewGuid() + filename
                    });

            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (FileStream fs = System.IO.File.Create(path))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            path = path.Replace(hostingEnvironment.WebRootPath, "").Replace("\\", "/");
            if (!string.IsNullOrEmpty(path))
            {
                var UploadWebIcon = FindById(Id);
                UploadWebIcon.WebDir = path;
                UploadWebIcon.WebUrl = AbsolutePath + path;
                subMenuCategoryRepository.Update(UploadWebIcon);
                Save = subMenuCategoryRepository.SaveChanges();
            }
            return Save;
        }

        public int UploadMobileIcon(decimal Id, IFormFile file, string AbsolutePath)
        {
            int Save = 0;
            var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
            var Featurepath = Path.Combine(new string[]
                {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","SubCategory","MobileIcons",Id.ToString()
                });
            if (Directory.Exists(Featurepath))
            {
                Directory.Delete(Featurepath, true);
            }
            var path = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","SubCategory","MobileIcon",Id.ToString(),
                           Guid.NewGuid() + filename
                    });
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (FileStream fs = System.IO.File.Create(path))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            path = path.Replace(hostingEnvironment.WebRootPath, "").Replace("\\", "/");
            if (!string.IsNullOrEmpty(path))
            {
                var UploadMobileIcon = FindById(Id);
                UploadMobileIcon.MobileDir = path;
                UploadMobileIcon.MobileUrl = AbsolutePath + path;
                subMenuCategoryRepository.Update(UploadMobileIcon);
                Save = subMenuCategoryRepository.SaveChanges();
            }
            return Save;
        }

        public GetSubMenuCategoryResponse GetSubMenusSearchList(SubCategoryRequestModel request)
        {
            return subMenuCategoryRepository.GetSubMenusSearchList(request);
        }
        /*Client Front End Service*/
        public List<LoadSubCategoryModel> GetLoadSubCategories(decimal CatId, decimal? SubCatId, bool IsSubCategory)
        {
            return subMenuCategoryRepository.GetLoadSubCategories(CatId, SubCatId, IsSubCategory);
        }

        public List<MainMenuSubMenu> MainMenuSubMenuData()
        {
            return subMenuCategoryRepository.MainMenuSubMenuData();
        }
    }
}
