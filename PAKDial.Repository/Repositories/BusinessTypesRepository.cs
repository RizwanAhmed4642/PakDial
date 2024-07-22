using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
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
    public class BusinessTypesRepository : BaseRepository<BusinessTypes, decimal>, IBusinessTypesRepository
    {
        public BusinessTypesRepository(PAKDialSolutionsContext context)
           : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<BusinessTypes> DbSet
        {
            get
            {
                return db.BusinessTypes;
            }
        }

        public bool CheckExistance(BusinessTypes instance)
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

        public IEnumerable<VMGenericKeyValuePair<decimal>> GetAllBusinessTypeKey()
        {
            return DbSet.Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.Name }).ToList();
        }

        public BusinessTypesResponse GetBusinessTypes(BusinessTypesRequestModel request)
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
                Expression<Func<BusinessTypes, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.Name.Contains(request.SearchString))) ||
                         !isSearchFilterSpecified);

                int rowCount = DbSet.Count(query);
                // Server Side Pager
                IEnumerable<BusinessTypes> Businesstype = request.IsAsc
                    ? DbSet.Where(query)
                        .OrderBy(x => x.Id)
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
                return new BusinessTypesResponse
                {
                    RowCount = rowCount,
                    Businesstypes = Businesstype
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public List<ListingsBusinessTypes> GetByListingId(decimal ListingId)
        {
            throw new NotImplementedException();
        }
    }
}
