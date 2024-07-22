using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.Repository;
using PAKDial.Repository.BaseRepository;
using PAKDial.StoreProcdures.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PAKDial.Repository.Repositories
{
    public class SubMenuCategoryRepository : BaseRepository<SubMenuCategory, decimal>, ISubMenuCategoryRepository
    {
        public SubMenuCategoryRepository(PAKDialSolutionsContext context)
          : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<SubMenuCategory> DbSet
        {
            get
            {
                return db.SubMenuCategory;
            }
        }

        public bool CheckExistance(SubMenuCategory instance)
        {
            bool Results = false;
            if (instance.Id > 0)
            {
                Results = DbSet.Where(c => c.Name.ToLower().Trim() == instance.Name.ToLower().Trim() && c.Id != instance.Id && (c.ParentSubCategoryId != instance.ParentSubCategoryId && c.MainCategoryId != instance.MainCategoryId))
                    .Count() > 0 ? true : false;
            }
            else
            {
                Results = DbSet.Where(c => c.Name.ToLower().Trim() == instance.Name.ToLower().Trim() && (c.ParentSubCategoryId != instance.ParentSubCategoryId && c.MainCategoryId != instance.MainCategoryId))
                   .Count() > 0 ? true : false;
            }
            return Results;
        }
        public decimal IsPopularCategory(decimal Id, string name)
        {
            decimal Result = 0; //Not Verified and UnVerified
            var Category = DbSet.Where(c => c.Id == Id).FirstOrDefault();
            if (name == "Yes")
            {
                Category.IsPopular = true;
                db.SubMenuCategory.Update(Category);
                db.SaveChanges();
                Result = 1;
            }
            else if (name == "No")
            {
                Category.IsPopular = false;
                db.SubMenuCategory.Update(Category);
                db.SaveChanges();
                Result = 1;
            }
            return Result;
        }
        public decimal AddSubCategory(SubMenuCategory instance)
        {
            decimal Save = 0;
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var CategoryIdCheck = db.MainMenuCategory.Find(instance.MainCategoryId);
                    instance.CategoryTypeId = CategoryIdCheck.CategoryTypeId;
                    db.SubMenuCategory.Add(instance);
                    db.SaveChanges();
                    var FindInserted = Find(instance.Id);
                    if (!string.IsNullOrEmpty(instance.TrackIds))
                    {
                        var SubCategory = GetByTrackIds(instance.TrackIds);
                        FindInserted.ParentSubCategoryId = SubCategory.Id;
                        FindInserted.TrackIds = SubCategory.TrackIds + '|' + FindInserted.Id;
                        FindInserted.TrackNames = SubCategory.TrackNames + '>' + FindInserted.Name;
                    }
                    else
                    {
                        FindInserted.TrackIds = FindInserted.Id.ToString();
                        FindInserted.TrackNames = FindInserted.Name;
                    }
                    db.SubMenuCategory.Attach(FindInserted);
                    db.Entry(FindInserted).State = EntityState.Modified;
                    db.SaveChanges();
                    transaction.Commit();
                    Save = FindInserted.Id;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Save = 0;
                }
            }
            return Save;
        }

        public int UpdateSubCategory(SubMenuCategory instance)
        {
            int Save = 0;
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (!string.IsNullOrEmpty(instance.TrackIds))
                    {
                        var SubCategory = GetByTrackIds(instance.TrackIds);
                        instance.ParentSubCategoryId = SubCategory.Id;
                        instance.TrackIds = SubCategory.TrackIds + '|' + instance.Id;
                        instance.TrackNames = SubCategory.TrackNames + '>' + instance.Name;
                    }
                    else
                    {
                        instance.TrackIds = instance.Id.ToString();
                        instance.TrackNames = instance.Name;
                    }
                    db.SubMenuCategory.Attach(instance);
                    db.Entry(instance).State = EntityState.Modified;
                    db.SaveChanges();
                    transaction.Commit();
                    Save = 1;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Save = 0;
                }
            }
            return Save;
        }

        public List<SubMenuCategory> FindByMainCategory(decimal MainCategoryId)
        {
            return DbSet.Where(c => c.MainCategoryId == MainCategoryId).ToList();
        }

        public bool SubCategoryExistsAgainstMain(decimal MainCategoryId)
        {
            return DbSet.Where(c => c.MainCategoryId == MainCategoryId).FirstOrDefault() != null ? true:false;
        }

        public List<SubMenuCategory> FindByMPSubCategory(decimal MainCategoryId, decimal ParentSubCategoryId)
        {
            return DbSet.Where(c => c.MainCategoryId == MainCategoryId && c.ParentSubCategoryId == ParentSubCategoryId).ToList();
        }

        public List<SubMenuCategory> FindByPSubCategory(decimal ParentSubCategoryId)
        {
            return DbSet.Where(c => c.ParentSubCategoryId == ParentSubCategoryId).ToList();
        }

        public List<MainMenuSubMenu> MainMenuSubMenuData()
        {
            return DbSet.Where(c => c.IsLastNode == true && c.IsActive == true).Select(c=>new MainMenuSubMenu {CatId=c.MainCategoryId,Id=c.Id,Name=c.Name,IsLastNode=true } ).Take(40).ToList();
        }

        public SubMenuCategoryResponse GetSubMenus(SubMenuCategoryRequestModel request)
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
                Expression<Func<VSubMenuCategory, bool>> query =
                    exp =>
                         (isSearchFilterSpecified && ((exp.TrackNames.Contains(request.SearchString)) ||
                         (exp.MainCategoryName.Contains(request.SearchString))) || !isSearchFilterSpecified);


                int rowCount = db.VSubMenuCategory.Count(query);
                // Server Side Pager
                IEnumerable<VSubMenuCategory> SubMenuCategories = request.IsAsc
                    ? db.VSubMenuCategory.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : db.VSubMenuCategory.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();
                return new SubMenuCategoryResponse
                {
                    RowCount = rowCount,
                    SubMenuCategories = SubMenuCategories
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
            if (Type == 1 && !string.IsNullOrEmpty(ImagePath))
            {
                var GetCategory = DbSet.Where(c => c.Id == Id).FirstOrDefault();
                GetCategory.SubBannerImage = ImagePath;
                GetCategory.SubBannerImageUrl = AbsolutePath;
                Update(GetCategory);

            }
            //Featured
            else if (Type == 2 && !string.IsNullOrEmpty(ImagePath))
            {
                var GetCategory = DbSet.Where(c => c.Id == Id).FirstOrDefault();
                GetCategory.SubFeatureImage = ImagePath;
                GetCategory.SubFeatureImageUrl = AbsolutePath;
                Update(GetCategory);
            }
        }

        public GetSubCategoryResponse GetSubMenusList(SubCategoryRequestModel request)
        {
            try
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;
                bool IsCategoryIdisSpecified = request.SubCatId > 0; //its means Edit Mode

                Expression<Func<SubMenuCategory, bool>> query = null;
                if(IsCategoryIdisSpecified == true)
                {
                    query = exp =>
                        (string.IsNullOrEmpty(request.SearchString) || (exp.TrackNames.Contains(request.SearchString.Trim().ToLower()))
                          && exp.MainCategoryId == request.MainCatId && exp.Id != request.SubCatId && !exp.IsLastNode == true);
                }
                else
                {
                    query = exp =>
                       (string.IsNullOrEmpty(request.SearchString) || (exp.TrackNames.Contains(request.SearchString.Trim().ToLower()))
                          && exp.MainCategoryId == request.MainCatId && !exp.IsLastNode == true);
                }
                    

                int rowCount = DbSet.Count(query);
                // Server Side Pager
                IEnumerable<VMSubCategoryValuePair> SubMenuCategories =
                      DbSet.Where(query)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList().Select(c => new VMSubCategoryValuePair { id = c.TrackIds, text = c.TrackNames });
                  
                return new GetSubCategoryResponse
                {
                    PageNo = fromRow,
                    RowCount = rowCount,
                    SubCategoy = SubMenuCategories
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public SubMenuCategory GetByTrackIds(string TrackIds)
        {
            return DbSet.Where(c => c.TrackIds == TrackIds).FirstOrDefault();
        }

        public SubMenuCategory GetByTrackName(string TrackNames)
        {
            return DbSet.Where(c => c.TrackNames.ToLower() == TrackNames.ToLower()).FirstOrDefault();
        }

        public GetSubMenuCategoryResponse GetSubMenusSearchList(SubCategoryRequestModel request)
        {
            try
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;

                Expression<Func<SubMenuCategory, bool>> query = exp =>
                       (string.IsNullOrEmpty(request.SearchString) || (exp.TrackNames.Contains(request.SearchString.Trim().ToLower()))
                          && exp.MainCategoryId == request.MainCatId && exp.IsLastNode == true && exp.IsActive == true);

                int rowCount = DbSet.Count(query);
                // Server Side Pager
                IEnumerable<VMGenericKeyValuePair<decimal>> SubMenuCategories =
                      DbSet.Where(query)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList().Select(c => new VMGenericKeyValuePair<decimal> {Id=c.Id,Text=c.TrackNames});

                return new GetSubMenuCategoryResponse
                {
                    PageNo = fromRow,
                    RowCount = rowCount,
                    SubMenuCategoryList = SubMenuCategories
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*Client Front End Repository*/
        public List<LoadSubCategoryModel> GetLoadSubCategories(decimal CatId,decimal? SubCatId,bool IsSubCategory)
        {
            List<LoadSubCategoryModel> model = new List<LoadSubCategoryModel>();
            if (IsSubCategory == false)
            {
                model = DbSet.Where(c => c.MainCategoryId == CatId && c.ParentSubCategoryId == null)
                       .Select(c => new LoadSubCategoryModel
                       {
                           SubCatId = c.Id,
                           Name = c.Name,
                           MainCategoryId = c.MainCategoryId,
                           ParentSubCategoryId = c.ParentSubCategoryId,
                           Web_Url = c.WebUrl,
                           IsLastNode = c.IsLastNode,
                           IsPopular = c.IsPopular,
                       }).ToList();
            }
            else
            {
                model = DbSet.Where(c => c.MainCategoryId == CatId && c.ParentSubCategoryId == SubCatId)
                       .Select(c => new LoadSubCategoryModel
                       {
                           SubCatId = c.Id,
                           Name = c.Name,
                           MainCategoryId = c.MainCategoryId,
                           ParentSubCategoryId = c.ParentSubCategoryId,
                           Web_Url = c.WebUrl,
                           IsLastNode = c.IsLastNode,
                           IsPopular = c.IsPopular,
                       }).ToList();
            }
            model.Sort((x, y) => String.Compare(x.Name, y.Name));
            return model;
        }

        public SubMenuCategory GetSubCateById(decimal SubCategoryId)
        {
            return DbSet.Where(c => c.Id == SubCategoryId).FirstOrDefault();
        }
        public List<SearchSbCategories> SearchFrontEndSubCategory(string SbCategoryName,string Location)
        {
            string Category = "";
            string CityAreas = "";
            List<SearchSbCategories> SubMenuCategories = new List<SearchSbCategories>();
            List<CityArea> areas = new List<CityArea>();
            decimal LocId = db.City.Where(c => c.Name.Contains(Location)).FirstOrDefault().Id;
            int position = SbCategoryName.IndexOf(" in");
            if (position < 0)
            {
                Expression<Func<VCatAndCompanyListingsSearch, bool>> query =
                exp =>
                     (string.IsNullOrEmpty(SbCategoryName) || (exp.CatName.StartsWith(SbCategoryName))
                     || (exp.CompanyName.StartsWith(SbCategoryName) && exp.CityName.Contains(Location)));
                // Server Side Pager
                SubMenuCategories =
                 db.VCatAndCompanyListingsSearch.Where(query)
                   .Take(20)
                   .Select(c => new SearchSbCategories { Id = c.CatId, SbCatName = c.CatName, ListingId = c.ListingId, CompanyName = c.CompanyName, CtName = c.CityName, ArName = c.CityAreaName, AvgRating = c.AvgRating })
                   .ToList();

                SubMenuCategories.AddRange(ReturnSubCatKeywords(SbCategoryName));
            }
            else
            {
                Category = SbCategoryName.Substring(0, position);
                CityAreas = SbCategoryName.Substring(position + 3);
                Expression<Func<VCatAndCompanyListingsSearch, bool>> query =
                exp =>
                     (string.IsNullOrEmpty(Category) || (exp.CatName.StartsWith(Category))
                     || (exp.CompanyName.StartsWith(Category) && exp.CityName.Contains(Location)));
                // Server Side Pager
                List<SearchSbCategories> SubMenuCategory =
                      db.VCatAndCompanyListingsSearch.Where(query)
                        .Take(20)
                        .Select(c => new SearchSbCategories { Id = c.CatId, SbCatName = c.CatName, ListingId = c.ListingId, CompanyName = c.CompanyName, CtName = c.CityName, ArName = c.CityAreaName, AvgRating = c.AvgRating })
                        .ToList();

                SubMenuCategory.AddRange(ReturnSubCatKeywords(Category));

                if (!string.IsNullOrEmpty(CityAreas) && SubMenuCategory.Count() > 0)
                {
                    areas = db.CityArea.Where(c => c.Name.Trim().ToLower().StartsWith(CityAreas.Trim().ToLower()) && c.CityId == LocId).Take(10).ToList();
                }
                else if (SubMenuCategory.Count() > 0)
                {
                    areas = db.CityArea.Where(c => c.CityId == LocId).Take(10).ToList();
                }
                foreach (var area in areas)
                {
                    foreach (var item in SubMenuCategory.Where(c => c.CompanyName == null))
                    {
                        SearchSbCategories search = new SearchSbCategories
                        {
                            Id = item.Id,
                            SbCatName = item.SbCatName,
                            ListingId = item.ListingId,
                            CompanyName = item.CompanyName,
                            CtName = item.CtName,
                            ArName = area.Name,
                            AvgRating = item.AvgRating
                        };
                        SubMenuCategories.Add(search);
                    }
                }
                SubMenuCategories.AddRange(SubMenuCategory.Where(c => c.ListingId > 0));
            }

            return SubMenuCategories;
        }

        public List<SearchSbCategories> ReturnSubCatKeywords(string SbCategoryName)
        {
            //Expression<Func<VSubCategoryKeywordsSearch, bool>> query =
            //   exp =>
            //        (string.IsNullOrEmpty(SbCategoryName) || (exp.CatName.ToLower().StartsWith(SbCategoryName.ToLower())));
            //// Server Side Pager
            //List<SearchSbCategories> SubMenuCategories =
            // db.VSubCategoryKeywordsSearch.Where(query)
            //   .Take(20)
            //   .Select(c => new SearchSbCategories { Id = c.CatId, SbCatName = c.CatName })
            //   .ToList();

            //return SubMenuCategories;
            return UserFrontStoreProcedure.Sp_SubCategoryKeywordsSearch(SbCategoryName);
        }
        public CategoryMetasKeywords GetSubMetaDetail(string Location, decimal CatId)
        {
            CategoryMetasKeywords res = new CategoryMetasKeywords();
            var results = DbSet.Where(c => c.IsActive == true && c.Id == CatId).Select(c => new CategoryMetasKeywords { Id = c.Id, MetaTitle = c.MetaTitle.Replace("*#Cities#*", Location), MetaKeyword = c.MetaKeyword.Replace("*#Cities#*", Location), MetaDescription = c.MetaDescription.Replace("*#Cities#*", Location), Location = Location }).FirstOrDefault();
            return results ?? res;
        }
    }
}
