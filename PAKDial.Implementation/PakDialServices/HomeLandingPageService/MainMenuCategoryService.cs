using Microsoft.AspNetCore.Http;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Interfaces.PakDialServices.IHomeLandingPageService;
using PAKDial.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using PAKDial.Domains.UserEndViewModel;

namespace PAKDial.Implementation.PakDialServices.HomeLandingPageService
{
    public class MainMenuCategoryService : IMainMenuCategoryService
    {
        private readonly IMainMenuCategoryRepository mainMenuCategoryRepository;
        private readonly ISubMenuCategoryRepository subMenuCategoryRepository;
        private readonly IHomeSecMainMenuCatRepository homeSecMainMenuCatRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public MainMenuCategoryService(IMainMenuCategoryRepository mainMenuCategoryRepository
            , ISubMenuCategoryRepository subMenuCategoryRepository,
            IHomeSecMainMenuCatRepository homeSecMainMenuCatRepository,
            IHostingEnvironment hostingEnvironment)
        {
            this.mainMenuCategoryRepository = mainMenuCategoryRepository;
            this.subMenuCategoryRepository = subMenuCategoryRepository;
            this.homeSecMainMenuCatRepository = homeSecMainMenuCatRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        public decimal Add(MainMenuCategory instance)
        {
            decimal Save = 0; //"MainMenuCategory Not Added";
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            var CheckExistance = mainMenuCategoryRepository.CheckExistance(instance);
            if (!CheckExistance)
            {
                mainMenuCategoryRepository.Add(instance);
                Save = mainMenuCategoryRepository.SaveChanges(); //1 MainMenuCategory Added Successfully
                if(Save > 0)
                {
                    Save = instance.Id;
                }
            }
            else
            {
                Save = -2; // MainMenuCategory Already Exist.
            }
            return Save;
        }

        public int Update(MainMenuCategory instance)
        {
            int Result = 0; //Record Not Updated Successfully
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            bool result = mainMenuCategoryRepository.CheckExistance(instance);
            if (!result)
            {
                var MenuCat = FindById(instance.Id);
                MenuCat.Name = instance.Name;
                MenuCat.Description = instance.Description;
                MenuCat.MetaTitle = instance.MetaTitle;
                MenuCat.MetaKeyword = instance.MetaKeyword;
                MenuCat.MetaDescription = instance.MetaDescription;
                MenuCat.UpdatedBy = instance.UpdatedBy;
                MenuCat.UpdatedDate = instance.UpdatedDate;
                MenuCat.IsActive = instance.IsActive;
                MenuCat.CategoryTypeId = instance.CategoryTypeId;
                mainMenuCategoryRepository.Update(MenuCat);
                Result = mainMenuCategoryRepository.SaveChanges(); //Record Updated Successfully
            }
            else
            {
                Result = -2; //"MainMenuCategory Already Exits";
            }
            return Result;
        }

        public int Delete(decimal Id)
        {
            int Results = 0; //"MainMenuCategory Not Deleted."
            var checkStates = subMenuCategoryRepository.SubCategoryExistsAgainstMain(Id);
            var checkStates1 = homeSecMainMenuCatRepository.GetHomeSecByMenuId(Id).Count() > 0 ? true : false;
            if (!checkStates && !checkStates1)
            {
                mainMenuCategoryRepository.Delete(FindById(Id));
                Results = mainMenuCategoryRepository.SaveChanges(); // MainMenuCategory Deleted Successfully.
            }
            else
            {
                Results = 2; // Please Delete its Sub Category First.
            }
            var Bannerpath = Path.Combine(new string[]
                   {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","MainCategory","BannerImages",Id.ToString()
                   });
            var Featurepath = Path.Combine(new string[]
                   {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","MainCategory","FeatureImages",Id.ToString()
                   });

            var WebIcon = Path.Combine(new string[]
                   {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","MainCategory","WebIcons",Id.ToString()
                   });
            var MobileIcon = Path.Combine(new string[]
                   {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","MainCategory","MobileIcons",Id.ToString()
                   });
            if (Results == 1)
            {
                if (Directory.Exists(Bannerpath))
                {
                    Directory.Delete(Bannerpath, true);
                }
                if (Directory.Exists(Featurepath))
                {
                    Directory.Delete(Featurepath, true);
                }
                if (Directory.Exists(WebIcon))
                {
                    Directory.Delete(WebIcon, true);
                }
                if (Directory.Exists(MobileIcon))
                {
                    Directory.Delete(MobileIcon, true);
                }
            }

            return Results;
        }

        public MainMenuCategory FindById(decimal id)
        {
            return mainMenuCategoryRepository.Find(id);
        }

        public IEnumerable<MainMenuCategory> GetAll()
        {
            return mainMenuCategoryRepository.GetAll();
        }

        public MainMenuCategoryResponse GetMenus(MainMenuCategoryRequestModel request)
        {
            return mainMenuCategoryRepository.GetMenus(request);
        }

        public int UploadCategoryBannerImage(decimal Id, IFormFile file,string AbsolutePath)
        {
            int Save = 0;
            var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
            var Bannerpath = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","MainCategory","BannerImages",Id.ToString()
                    });
            if (Directory.Exists(Bannerpath))
            {
                Directory.Delete(Bannerpath, true);
            }
            var path = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","MainCategory","BannerImages",Id.ToString(),
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
                mainMenuCategoryRepository.UploadImages(Id, path, 1, AbsolutePath + path);
                Save = mainMenuCategoryRepository.SaveChanges();
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
                           "SystemImages","MainCategory","FeatureImages",Id.ToString()
                });
            if (Directory.Exists(Featurepath))
            {
                Directory.Delete(Featurepath, true);
            }
            var path = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","MainCategory","FeatureImages",Id.ToString(),
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
                mainMenuCategoryRepository.UploadImages(Id, path, 2,  AbsolutePath + path);
                Save = mainMenuCategoryRepository.SaveChanges();
            }
            return Save;
        }

        public int UploadWebIcons(decimal Id, IFormFile file, string AbsolutePath)
        {
            int Save = 0;
            var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
            var Bannerpath = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","MainCategory","WebIcons",Id.ToString()
                    });
            if (Directory.Exists(Bannerpath))
            {
                Directory.Delete(Bannerpath, true);
            }
            var path = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","MainCategory","WebIcons",Id.ToString(),
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
                var WebIcon = FindById(Id);
                WebIcon.WebDir = path;
                WebIcon.WebUrl = AbsolutePath + path;
                mainMenuCategoryRepository.Update(WebIcon);
                Save = mainMenuCategoryRepository.SaveChanges();
            }
            return Save;
        }

        public int UploadMobileIcons(decimal Id, IFormFile file, string AbsolutePath)
        {
            int Save = 0;
            var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
            var Featurepath = Path.Combine(new string[]
                {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","MainCategory","MobileIcons",Id.ToString()
                });
            if (Directory.Exists(Featurepath))
            {
                Directory.Delete(Featurepath, true);
            }
            var path = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","MainCategory","MobileIcons",Id.ToString(),
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
                var MobileIcon = FindById(Id);
                MobileIcon.MobileDir = path;
                MobileIcon.MobileUrl = AbsolutePath + path;
                mainMenuCategoryRepository.Update(MobileIcon);
                Save = mainMenuCategoryRepository.SaveChanges();
            }
            return Save;
        }

        public GetMainMenuCategoryResponse GetMainMenuSearchList(MainMenuCategoryRequestModel request)
        {
            return mainMenuCategoryRepository.GetMainMenuSearchList(request);
        }

        /*Client Front End Service*/
        public List<MainSideBarMenu> GetSideMainMenu()
        {
            return mainMenuCategoryRepository.GetSideMainMenu();
        }

        public List<MainSideBarMenu> GetSideMainMenu(int RecordsRequireds)
        {
            return mainMenuCategoryRepository.GetSideMainMenu(RecordsRequireds);
        }
        public List<MainSideBarMenu> GetRegListMenu(int CatRequireds)
        {
            return mainMenuCategoryRepository.GetRegListMenu(CatRequireds);
        }

        public CategoryBannerModel GetBannerModel(decimal Id)
        {
            return mainMenuCategoryRepository.GetBannerModel(Id);
        }
    }
}
