using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ViewModels;
using System.Collections.Generic;

namespace PAKDial.Interfaces.CommonServices
{
    public interface ICityAreaService
    {
        int Update(CityArea instance);
        int Delete(decimal Id);
        int Add(CityArea instance);
        int UpdateRange(List<CityArea> instance);
        int DeleteRange(List<CityArea> instance);
        int AddRange(List<CityArea> instance);
        CityArea FindById(decimal id);
        IEnumerable<CityArea> GetAll();
        IEnumerable<CityArea> GetAllAreasByCity(decimal CityId);
        CityAreabyCityResponse GetAllAreasByCity(CityAreaRequestModel request);
        CityAreaResponse GetCitiesArea(CityAreaRequestModel request);

        StateCityResponse GetSCByCityAreaId(decimal Id);
        IEnumerable<VMGenericKeyValuePair<decimal>> GetAllAreaByCityIdKeyValue(decimal CityId);
        VMControlCityAreas GetAllAreabyUserId(string UserId);
        GetVCombineRegionsResponse GetCombineRegionUserId(CombineRegionsRequestModel request);
    }

}
