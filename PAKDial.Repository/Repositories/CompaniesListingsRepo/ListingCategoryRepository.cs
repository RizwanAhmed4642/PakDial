using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PAKDial.Repository.Repositories.CompaniesListingsRepo
{
    public class ListingCategoryRepository : BaseRepository<ListingCategory, decimal>, IListingCategoryRepository
    {
        public ListingCategoryRepository(PAKDialSolutionsContext context)
           : base(context)
        {

        }

        /// Primary database set
        protected override DbSet<ListingCategory> DbSet
        {
            get
            {
                return db.ListingCategory;
            }
        }

        public List<ListingCategory> GetByListingId(decimal ListingId)
        {
            return DbSet.Where(c => c.ListingId == ListingId).ToList();
        }

        public List<ListingCategory> GetListingCategories(decimal MainCategoryId)
        {
            return DbSet.Where(c => c.MainCategoryId == MainCategoryId).ToList();
        }

        public List<ListingCategory> GetListingCategories(decimal? SubCategoryId, string CategoryName)
        {
            bool isSearchFilterSpecified = !(SubCategoryId > 0 ? false : true);

            Expression<Func<ListingCategory, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.SubCategoryId == SubCategoryId) ||!isSearchFilterSpecified));

            return DbSet.Where(query).AsNoTracking().ToList();
        }

        public List<ListingCategory> GetListingCategories(decimal SubCategoryId, decimal MainCategoryId)
        {
            return DbSet.Where(c => c.MainCategoryId == MainCategoryId && c.SubCategoryId == SubCategoryId).ToList();
        }

        public ListingCategory GetListingCategories(decimal MainCategoryId, decimal SubCategoryId, decimal ListingId)
        {
            return DbSet.Where(c => c.MainCategoryId == MainCategoryId && c.SubCategoryId == SubCategoryId && c.ListingId == ListingId).FirstOrDefault();
        }
    }
}
