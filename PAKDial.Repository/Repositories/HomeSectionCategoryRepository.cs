using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Interfaces.Repository;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PAKDial.Repository.Repositories
{
    public class HomeSectionCategoryRepository : BaseRepository<HomeSectionCategory, decimal>, IHomeSectionCategoryRepository
    {
        public HomeSectionCategoryRepository(PAKDialSolutionsContext context)
          : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<HomeSectionCategory> DbSet
        {
            get
            {
                return db.HomeSectionCategory;
            }
        }

        public bool CheckExistance(HomeSectionCategory instance)
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

        public HomeSectionCategoryResponse GetHomeSection(HomeSectionCategoryRequestModel request)
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
                Expression<Func<HomeSectionCategory, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.Name.Contains(request.SearchString))) ||
                         !isSearchFilterSpecified);

                int rowCount = DbSet.Count(query);
                // Server Side Pager
                IEnumerable<HomeSectionCategory> HomeSectionCategories = request.IsAsc
                    ? DbSet.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : DbSet.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();
                return new HomeSectionCategoryResponse
                {
                    RowCount = rowCount,
                    HomeSectionCategories = HomeSectionCategories
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
