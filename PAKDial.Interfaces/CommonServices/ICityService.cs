using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Domains.ViewModels;
using System.Collections.Generic;

namespace PAKDial.Interfaces.CommonServices
{
    public interface ICityService
    {
        int Update(City instance);
        int Delete(decimal Id);
        int Add(City instance);
        int UpdateRange(List<City> instance);
        int DeleteRange(List<City> instance);
        int AddRange(List<City> instance);
        City FindById(decimal id);
        IEnumerable<City> GetAll();
        IEnumerable<City> GetAllCitiesByStates(decimal StateId);
        IEnumerable<VMGenericKeyValuePair<decimal>> GetAllCityByStatesKey(decimal StateId);
        CityResponse GetCities(CityRequestModel request);
        IEnumerable<StateProvince> GetStateByCityId(decimal Id);
        GetCityResponse GetCitySearchList(CityRequestModel request);
        IEnumerable<VMGenericKeyValuePair<decimal>> GetAssignedCity(decimal ManagerId);
        List<LoadCities> GetAllCitiesLoad();
    }
}
