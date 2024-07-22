using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Interfaces.Repository;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PAKDial.Repository.Repositories
{
    public class HomeSecMainMenuCatRepository : BaseRepository<HomeSecMainMenuCat, decimal>, IHomeSecMainMenuCatRepository
    {
        public HomeSecMainMenuCatRepository(PAKDialSolutionsContext context)
         : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<HomeSecMainMenuCat> DbSet
        {
            get
            {
                return db.HomeSecMainMenuCat;
            }
        }

        public List<HomeSecMainMenuCat> GetHomeSecByBothId(decimal MainMenuCatId, decimal HomeSecCatId)
        {
            return DbSet.Where(c => c.MainMenuCatId == MainMenuCatId && c.HomeSecCatId == HomeSecCatId).ToList();
        }

        public List<HomeSecMainMenuCat> GetHomeSecByMenuId(decimal MainMenuCatId)
        {
            return DbSet.Where(c => c.MainMenuCatId == MainMenuCatId).ToList();
        }

        public List<HomeSecMainMenuCat> GetHomeSecBySecId(decimal HomeSecCatId)
        {
            return DbSet.Where(c => c.HomeSecCatId == HomeSecCatId).ToList();
        }

        public HomeSecMainMenuCatResponse GetHomeSecMainMenu(HomeSecMainMenuCatRequestModel request)
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
                Expression<Func<VHomeSecMainMenuCat, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.MainMenuCatName.Contains(request.SearchString)) || (exp.HomeSecCatName.Contains(request.SearchString))) ||
                         !isSearchFilterSpecified);

                int rowCount = db.VHomeSecMainMenuCat.Count(query);
                // Server Side Pager
                IEnumerable<VHomeSecMainMenuCat> VHomeSecMainMenuCats = request.IsAsc
                    ? db.VHomeSecMainMenuCat.Where(query)
                        .OrderBy(x => x.MainMenuCat_Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : db.VHomeSecMainMenuCat.Where(query)
                        .OrderByDescending(x => x.MainMenuCat_Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();
                return new HomeSecMainMenuCatResponse
                {
                    RowCount = rowCount,
                    VHomeSecMainMenuCats = VHomeSecMainMenuCats
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
