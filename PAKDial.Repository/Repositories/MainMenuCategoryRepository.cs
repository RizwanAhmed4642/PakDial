using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.Repository;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PAKDial.Repository.Repositories
{
    public class MainMenuCategoryRepository : BaseRepository<MainMenuCategory, decimal>, IMainMenuCategoryRepository
    {
        public MainMenuCategoryRepository(PAKDialSolutionsContext context)
          : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<MainMenuCategory> DbSet
        {
            get
            {
                return db.MainMenuCategory;
            }
        }
        public bool CheckExistance(MainMenuCategory instance)
        {
            bool Results = false;
            if (instance.Id > 0)
            {
                Results = DbSet.Where(c => c.Name.ToLower().Trim() == instance.Name.ToLower().Trim() && c.Id != instance.Id)
                    .Count() > 0 ? true : false;
            }
            else
            {
                Results = DbSet.Where(c => c.Name.ToLower().Trim() == instance.Name.ToLower().Trim())
                    .Count() > 0 ? true : false;
            }
            return Results;
        }
        public MainMenuCategoryResponse GetMenus(MainMenuCategoryRequestModel request)
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
                Expression<Func<VMainCategory, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.Name.Contains(request.SearchString))) ||
                         !isSearchFilterSpecified);

                int rowCount = db.VMainCategory.Count(query);
                // Server Side Pager
                IEnumerable<VMainCategory> MainMenuCategories = request.IsAsc
                    ? db.VMainCategory.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : db.VMainCategory.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();
                return new MainMenuCategoryResponse
                {
                    RowCount = rowCount,
                    MainMenuCategories = MainMenuCategories
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public GetMainMenuCategoryResponse GetMainMenuSearchList(MainMenuCategoryRequestModel request)
        {
            try
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;

                bool isSearchFilterSpecified = !string.IsNullOrEmpty(request.SearchString);
                Expression<Func<MainMenuCategory, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.Name.Contains(request.SearchString))) ||
                         !isSearchFilterSpecified);

                int rowCount = DbSet.Count(query);
                // Server Side Pager
                IEnumerable<VMGenericKeyValuePair<decimal>> Category =
                      DbSet.Where(query)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList().Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.Name });

                return new GetMainMenuCategoryResponse
                {
                    PageNo = fromRow,
                    RowCount = rowCount,
                    MainMenuCategories = Category
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public void UploadImages(decimal Id, string ImagePath, int Type , string AbsolutePath)
        {
            //Bannner 
            if(Type == 1 && !string.IsNullOrEmpty(ImagePath))
            {
                var GetCategory = DbSet.Where(c => c.Id == Id).FirstOrDefault();
                GetCategory.CatBannerImage = ImagePath;
                GetCategory.CatBannerImageUrl = AbsolutePath;
                Update(GetCategory);
                
            }
            //Featured
            else if(Type == 2 && !string.IsNullOrEmpty(ImagePath))
            {
                var GetCategory = DbSet.Where(c => c.Id == Id).FirstOrDefault();
                GetCategory.CatFeatureImage = ImagePath;
                GetCategory.CatFeatureImageUrl = AbsolutePath;
                Update(GetCategory);
            }
        }
        public List<MainSideBarMenu> GetSideMainMenu()
        {
            var response = DbSet.Where(c => c.IsActive == true)
                      .Select(c=>new MainSideBarMenu {Id=c.Id,Name=c.Name,WebDir=c.WebDir,WebUrl=c.WebUrl,MobileDir=c.MobileDir,MobileUrl=c.MobileUrl })
                      .ToList();
            response.Sort((x, y) => String.Compare(x.Name, y.Name));

            return response;
        }

        public List<MainSideBarMenu> GetSideMainMenu(int RecordsRequireds)
        {
            var response = DbSet.Where(c => c.IsActive == true)
                      .Select(c => new MainSideBarMenu { Id = c.Id, Name = c.Name,Description=c.Description, WebDir = c.WebDir, WebUrl = c.WebUrl, MobileDir = c.MobileDir, MobileUrl = c.MobileUrl }).Take(RecordsRequireds)
                      .ToList();
            return response;
        }
        /*Register page Category*/
        public List<MainSideBarMenu> GetRegListMenu(int CatRequireds)
        {
            List<MainSideBarMenu> mainSideBarMenu = new List<MainSideBarMenu>();            
            var RegPageCategory = db.MainMenuCategory.ToList();
            foreach (var item in RegPageCategory)
            {
                if (item.Name.ToLower() == "Education".ToLower()|| item.Name.ToLower() == "Mobile, Tablets & Accessories".ToLower() || item.Name.ToLower() == "B2B".ToLower() || item.Name.ToLower() == "Shopping".ToLower() || item.Name.ToLower() == "Personal Care".ToLower() || item.Name.ToLower() == "Repairs".ToLower() || item.Name.ToLower() == "Daily Needs".ToLower() || item.Name.ToLower() == "Automobiles".ToLower() || item.Name.ToLower() == "Jewellery".ToLower() || item.Name.ToLower() == "Modular Kitchen".ToLower())
                {
                    MainSideBarMenu mainSideBar = new MainSideBarMenu()
                    {
                      MobileDir = item.MobileDir,
                      Name = item.Name,
                      Id = item.Id,
                      WebDir= item.WebDir,
                    };
                    mainSideBarMenu.Add(mainSideBar);
                }
            }
            return mainSideBarMenu;
        }
        /*Client Front End Repository*/
        public CategoryBannerModel GetBannerModel(decimal Id)
        {
            return DbSet.Where(c => c.IsActive == true && c.Id == Id).Select(c => new CategoryBannerModel { Id = c.Id, BannerName = c.Name, BannerUrl = c.CatBannerImage }).FirstOrDefault();
        }

        public CategoryMetasKeywords GetMetaDetail(string Location, decimal CatId)
        {
            CategoryMetasKeywords res = new CategoryMetasKeywords();
            var results =  DbSet.Where(c => c.IsActive == true && c.Id == CatId).Select(c => new CategoryMetasKeywords { Id = c.Id, MetaTitle = c.MetaTitle.Replace("*#Cities#*", Location), MetaKeyword = c.MetaKeyword.Replace("*#Cities#*", Location), MetaDescription = c.MetaDescription.Replace("*#Cities#*", Location), Location = Location}).FirstOrDefault();
            return results ?? res;
        }

      
    }
}
