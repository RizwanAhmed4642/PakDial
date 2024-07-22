using Microsoft.EntityFrameworkCore;
using PAKDial.Interfaces.Repository;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PAKDial.Repository.Repositories
{
    public class CountryRepository : BaseRepository<Country, Decimal> , ICountryRepository
    {
        public CountryRepository(PAKDialSolutionsContext context)
            : base(context)
        {


























        }
        /// Primary database set
        protected override DbSet<Country> DbSet
        {
            get
            {
                return db.Country;
            }
        }

        public CountryResponse GetCountries(CountryRequestModel request)
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
                //int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;
                bool isSearchFilterSpecified = !string.IsNullOrEmpty(request.SearchString);
                Expression<Func<Country, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.Name.Contains(request.SearchString))) ||
                         !isSearchFilterSpecified);

                int rowCount = DbSet.Count(query);
                // Server Side Pager
                IEnumerable<Country> countries = request.IsAsc
                    ? DbSet.Where(query)
                        .OrderBy(x=>x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList()
                    : DbSet.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList();
                return new CountryResponse
                {
                    RowCount = rowCount,
                    Countries = countries
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        public Country FindByCode(string CountryCode)
        {
            return DbSet.Where(c => c.CountryCode == CountryCode).FirstOrDefault();
        }

        public bool FindCountryCodes(Country instance)
        {
            var CheckCount = DbSet.Where(c => c.CountryCode == instance.CountryCode && c.Id != instance.Id).Count();
            if (CheckCount > 0)
            {
                return false;
            }
            else
                return true;
        }

    }
}
