using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository
{
    public interface ICityAreaRepository : IBaseRepository <CityArea , decimal>
    {
        IEnumerable<CityArea> GetAllAreasByCity(decimal CityId);
        CityAreabyCityResponse GetAllAreasByCity(CityAreaRequestModel request);
        CityAreaResponse GetCitiesArea(CityAreaRequestModel request);
        StateCityResponse GetSCByCityAreaId(decimal Id);
        IEnumerable<VMGenericKeyValuePair<decimal>> GetAllAreaByCityIdKeyValue(decimal CityId);
        VMControlCityAreas GetAllAreabyUserId(string UserId);
        GetVCombineRegionsResponse GetCombineRegionUserId(CombineRegionsRequestModel request);
        List<string> GetCityandCityNames(string location, string CityArea, ref int TotalRecord);
        List<LoadCities> GetCityNameByArea(string location);
    }
}
