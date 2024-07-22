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
using PAKDial.Domains.SqlViewModels;
using PAKDial.Domains.ViewModels;

namespace PAKDial.Repository.Repositories
{
    public class StateProvinceRepository : BaseRepository<StateProvince,decimal>,IStateProvinceRepository
    {
        public StateProvinceRepository(PAKDialSolutionsContext context)
           : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<StateProvince> DbSet
        {
            get
            {
                return db.StateProvince;
            }
        }

        public IEnumerable<StateProvince> GetAllStatesByCountry(decimal CountryId)
        {
            List<StateProvince> stateProvinces = null;
            if(CountryId > 0)
            {
                stateProvinces = db.StateProvince.Where(c => c.CountryId == CountryId).ToList();
            }
            else
            {
                stateProvinces = new List<StateProvince>();
            }
            return stateProvinces;
        }

        public IEnumerable<VMGenericKeyValuePair<decimal>> GetAllStatesKeyValue()
        {
            return DbSet.Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.Name }).ToList();
        }

        public StateProvinceResponse GetStateProvinces(StateProvinceRequestModel request)
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

                Expression<Func<VStateProvince, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.Name.Contains(request.SearchString)) || 
                        (exp.CountryCode.Contains(request.SearchString)) || (exp.CountryName.Contains(request.SearchString))) 
                        || !isSearchFilterSpecified);

                int rowCount = db.VStateProvince.Count(query);
                // Server Side Pager
                IEnumerable<VStateProvince> stateProvinces = request.IsAsc
                    ? db.VStateProvince.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList()
                    : db.VStateProvince.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList();
                return new StateProvinceResponse
                {
                    RowCount = rowCount,
                    stateProvinces = stateProvinces
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
    }
}
