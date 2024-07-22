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
using PAKDial.StoreProcdures;
using PAKDial.Common;
using PAKDial.Domains.UserEndViewModel;

namespace PAKDial.Repository.Repositories
{
    public class CityAreaRepository : BaseRepository<CityArea,decimal> ,ICityAreaRepository
    {
        public CityAreaRepository(PAKDialSolutionsContext context)
           : base(context)
        {

        }

        // Primary database set
        protected override DbSet<CityArea> DbSet
        {
            get
            {
                return db.CityArea;
            }
        }

        public IEnumerable<CityArea> GetAllAreasByCity(decimal Id)
        {
            List<CityArea> cityAreas = null;
                                        
                if (Id > 0)
                { 
            
                    cityAreas = db.CityArea.Where(c => c.CityId == Id).ToList();
                }
                else
                {
                    cityAreas = new List<CityArea>();
                }
                return cityAreas;
            
        }

        public IEnumerable<VMGenericKeyValuePair<decimal>> GetAllAreaByCityIdKeyValue(decimal CityId)
        {
            return DbSet.Where(c => c.CityId == CityId).Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.Name }).ToList();
        }

        public StateCityResponse GetSCByCityAreaId(decimal Id)
        {
            List<StateProvince> stateProvinces = new List<StateProvince>();
            List<City> cities = new List<City>();
            var GetCityArea = db.VCityArea.Where(c => c.Id == Id).FirstOrDefault();
            if(GetCityArea != null)
            {
                stateProvinces.Add(new StateProvince { Id = (decimal)GetCityArea.StateId, Name = GetCityArea.StateName });
                cities.Add(new City { Id = (decimal)GetCityArea.CityId, Name = GetCityArea.CityName });
            }
            return new StateCityResponse { StateProvinces = stateProvinces, Cities = cities };
        }

        public CityAreaResponse GetCitiesArea(CityAreaRequestModel request)
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
                //bool isSearchCityId = request.CityId != 0;

                Expression<Func<VCityArea, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.Name.Contains(request.SearchString)) ||
                        (exp.CityName.Contains(request.SearchString)) || (exp.StateName.Contains(request.SearchString)) ||
                        (exp.CountryName.Contains(request.SearchString))) || !isSearchFilterSpecified);

                int rowCount = db.VCityArea.Count(query);
                // Server Side Pager
                IEnumerable<VCityArea> cityAreas = request.IsAsc
                    ? db.VCityArea.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : db.VCityArea.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();
                return new CityAreaResponse
                {
                    RowCount = rowCount,
                    cityAreas = cityAreas
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public VMControlCityAreas GetAllAreabyUserId(string UserId)
        {
            VMControlCityAreas response = new VMControlCityAreas();
            var emp = db.Employee.Where(c => c.UserId == UserId.ToString()).FirstOrDefault();
            if (emp.ZoneManagerId > 0)
            {
                var AssignArea = db.AssignedEmpAreas.Where(c => c.EmployeeId == emp.ZoneManagerId).ToList();
                if (AssignArea.Count() > 0)
                {
                    var ActiveZones = db.VActiveZones.Where(c => c.CityId == AssignArea.FirstOrDefault().CityId && AssignArea.Select(e => e.ZoneId).Contains(c.ZoneId)).ToList();
                    response.StateId = ActiveZones.FirstOrDefault().StateId;
                    response.CityId = ActiveZones.FirstOrDefault().CityId;
                    response.CityAreaKeyValue = ActiveZones.Select(c => new VMGenericKeyValuePair<decimal> { Id = c.CityAreaId, Text = c.CityAreaName }).ToList();
                }
            }
            return response;
        }

        //For Searchable DropDrown List
        public GetVCombineRegionsResponse GetCombineRegionUserId(CombineRegionsRequestModel request)
        {
            try
            {
                request.CityAreaIds = SaleExecutiveOrders_Execution.SpGetCityAreasByUserId(request.UserId);
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;

                Expression<Func<VCombineRegions, bool>> query = null;

                if(request.CityAreaIds.Count() > 0)
                {
                    query = exp =>
                       (string.IsNullOrEmpty(request.SearchString) || (exp.CombCityArea.Contains(request.SearchString.Trim().ToLower()))
                          && request.CityAreaIds.Contains(exp.Id));
                }
                else
                {
                    query = exp =>
                       (string.IsNullOrEmpty(request.SearchString) || (exp.CombCityArea.Contains(request.SearchString.Trim().ToLower())));
                }

                int rowCount = db.VCombineRegions.Count(query);
                // Server Side Pager
                IEnumerable<VMGenericKeyValuePair<decimal>> CombineList =
                      db.VCombineRegions.Where(query)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList().Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.CombCityArea });

                return new GetVCombineRegionsResponse
                {
                    PageNo = fromRow,
                    RowCount = rowCount,
                    CombineCityAreas = CombineList
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        /*Client Front End Repository*/
        // Get City and CityNames Other Then Parameter Name 
        public List<string> GetCityandCityNames(string location,string CityArea,ref int TotalRecords)
        {
            List<string> vs = new List<string>();
            if(TotalRecords == 0)
            {
                vs = db.VCityArea.Where(c => CommonSpacing.RemoveSpacestoTrim(c.CityName) == CommonSpacing.RemoveSpacestoTrim(location) && CommonSpacing.RemoveSpacestoTrim(c.Name) != CommonSpacing.RemoveSpacestoTrim(CityArea)).Select(c => c.Name).Take(10).ToList();
                TotalRecords = 10;
            }
            else
            {
                vs = db.VCityArea.Where(c => CommonSpacing.RemoveSpacestoTrim(c.CityName) == CommonSpacing.RemoveSpacestoTrim(location) && CommonSpacing.RemoveSpacestoTrim(c.Name) != CommonSpacing.RemoveSpacestoTrim(CityArea)).Select(c => c.Name).Skip(TotalRecords).Take(10).ToList();
                TotalRecords = TotalRecords + 10;
            }
            return vs;
        }

        // Get CityArea By Location
        public List<LoadCities> GetCityNameByArea(string location)
        {
            return db.VCityArea.Where(c => CommonSpacing.RemoveSpacestoTrim(c.CityName) == CommonSpacing.RemoveSpacestoTrim(location)).Select(c => new LoadCities {Id=c.Id,Name = c.Name }).ToList();
        }

        public CityAreabyCityResponse GetAllAreasByCity(CityAreaRequestModel request)
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
               

                Expression<Func<VCityArea, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.Name.StartsWith(request.SearchString))&&
                         (exp.CityId == request.CityId)) || !isSearchFilterSpecified);

                int rowCount = db.VCityArea.Count(query);
                // Server Side Pager
                IEnumerable<VCityArea> cityAreas = request.IsAsc
                    ? db.VCityArea.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : db.VCityArea.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();
                return new CityAreabyCityResponse
                {
                    RowCount = rowCount,
                    cityAreas = cityAreas.Select(c => new VMKeyValuePair { id = c.Id, text = c.Name })
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
