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
    public interface ICityRepository : IBaseRepository <City,decimal>
    {
        IEnumerable<City> GetAllCitiesByStates(decimal StateId);
        CityResponse GetCities(CityRequestModel request);
        IEnumerable<StateProvince> GetStateByCityId(decimal Id);
        GetCityResponse GetCitySearchList(CityRequestModel request);
        IEnumerable<VMGenericKeyValuePair<decimal>> GetAllCityByStatesKey(decimal StateId);
        IEnumerable<VMGenericKeyValuePair<decimal>> GetAssignedCity(decimal ManagerId);
        List<LoadCities> GetAllCitiesLoad();

    }
}
