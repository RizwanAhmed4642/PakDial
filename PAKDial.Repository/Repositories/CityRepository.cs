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
using PAKDial.Domains.UserEndViewModel;

namespace PAKDial.Repository.Repositories
{
    public class CityRepository : BaseRepository<City,decimal> , ICityRepository
    {
        public CityRepository(PAKDialSolutionsContext context)
            : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<City> DbSet
        {
            get
            {
                return db.City;
            }
        }

        public IEnumerable<City> GetAllCitiesByStates(decimal StateId)
        {
            List<City> city = null;
            if (StateId > 0)
            {

                city = db.City.Where(c => c.StateId == StateId).ToList();
            }
            else
            {
                city = new List<City>();
            }
            return city;
        }
        public List<LoadCities> GetAllCitiesLoad()
        {
            return db.City.Select(c => new LoadCities { Id = c.Id, Name = c.Name }).ToList();
        }

        public IEnumerable<VMGenericKeyValuePair<decimal>> GetAllCityByStatesKey(decimal StateId)
        {
            return DbSet.Where(c => c.StateId == StateId).Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.Name }).ToList();
        }

        public IEnumerable<StateProvince> GetStateByCityId(decimal Id)
        {
            List<StateProvince> states = new List<StateProvince>(); 
            var state = db.VCity.Where(c=>c.Id == Id).FirstOrDefault();
            if(state != null)
            {
                states.Add(new StateProvince {Id=state.StateId,Name=state.StateName});
            }
            return states;
        }

        public CityResponse GetCities(CityRequestModel request)
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
                //bool isSearchCountryId = request.CountryId != 0;
                //bool isSearchStateId = request.StateId != 0;

                Expression<Func<VCity, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.Name.Contains(request.SearchString)) ||
                        (exp.StateName.Contains(request.SearchString)) ||
                        (exp.CountryName.Contains(request.SearchString))) || !isSearchFilterSpecified);

                int rowCount = db.VCity.Count(query);
                // Server Side Pager
                IEnumerable<VCity> cities = request.IsAsc
                    ? db.VCity.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList()
                    : db.VCity.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList();
                return new CityResponse
                {
                    RowCount = rowCount,
                    cities = cities
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public GetCityResponse GetCitySearchList(CityRequestModel request)
        {
            try
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;

                Expression<Func<City, bool>> query = exp =>
                       (string.IsNullOrEmpty(request.SearchString) || (exp.Name.Contains(request.SearchString.Trim().ToLower()))
                          && exp.StateId == request.StateId);

                int rowCount = DbSet.Count(query);
                // Server Side Pager
                IEnumerable<VMGenericKeyValuePair<decimal>> CityList =
                      DbSet.Where(query)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList().Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.Name });

                return new GetCityResponse
                {
                    PageNo = fromRow,
                    RowCount = rowCount,
                    CityList = CityList
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public IEnumerable<VMGenericKeyValuePair<decimal>> GetAssignedCity(decimal ManagerId)
        {
            List<VMGenericKeyValuePair<decimal>> response = new List<VMGenericKeyValuePair<decimal>>();
            if(ManagerId > 0)
            {
                var GetManagerCities = db.AssignedEmpAreas.Where(c => c.EmployeeId == ManagerId).Select(c => c.CityId).ToList();
                response = DbSet.Where(c => GetManagerCities.Contains(c.Id)).Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.Name }).ToList();
            }
            return response;
        }
    }
}
