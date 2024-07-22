using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.Repository;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using PAKDial.Domains.ViewModels;
using PAKDial.Domains.UserEndViewModel;

namespace PAKDial.Implementation.CommonServices
{
    public class CityService : ICityService
    {
        private readonly ICityRepository cityRepository;
        private readonly ICityAreaService cityAreaService;
        public CityService(ICityRepository cityRepository, ICityAreaService cityAreaService)
        {
            this.cityRepository = cityRepository;
            this.cityAreaService = cityAreaService;
        }

        public int Add(City instance)
        {
            int Save = 0; //"City Not Added";
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            var CheckExistance = GetAllCitiesByStates(instance.StateId).Where(c => c.Name.ToLower().Trim() == instance.Name.ToLower().Trim()).Count();
            if (CheckExistance < 1)
            {
                cityRepository.Add(instance);
                Save = cityRepository.SaveChanges(); //1 City Added Successfully
            }
            else
            {
                Save = 2; // City Already Exist.
            }
            return Save;
        }
        public int Update(City instance)
        {
            int Result = 0; //0-City Not Updated Successfully
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            var CheckExistance = GetAllCitiesByStates(instance.StateId).Where(c => c.Name.ToLower().Trim() == instance.Name.ToLower().Trim() && c.Id != instance.Id).Count();
            if (CheckExistance < 1)
            {
                var EditCity = FindById(instance.Id);
                EditCity.StateId = instance.StateId;
                EditCity.CountryId = instance.CountryId;
                EditCity.Name = instance.Name;
                EditCity.UpdatedBy = instance.UpdatedBy;
                EditCity.UpdatedDate = instance.UpdatedDate;
                EditCity.CityLat = instance.CityLat;
                EditCity.CityLog = instance.CityLog;
                cityRepository.Update(EditCity);
                Result = cityRepository.SaveChanges(); //1-City Updated Successfully
            }
            else
            {
                Result = 2; //"2-City Already Exits";
            }
            return Result;

        }

        public int Delete(decimal Id)
        {
            int Results = 0; //"City Not Deleted."
            var checkCitiesArea = cityAreaService.GetAllAreasByCity(Id).Count();
            if (checkCitiesArea < 1)
            {
                cityRepository.Delete(FindById(Id));
                Results = cityRepository.SaveChanges(); // City Deleted Successfully.
            }
            else
            {
                Results = 2; //Please Delete its Areas First.
            }
            return Results;
        }

        public int AddRange(List<City> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            cityRepository.AddRange(instance);
            return cityRepository.SaveChanges();
        }

        public int UpdateRange(List<City> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            cityRepository.UpdateRange(instance);
            return cityRepository.SaveChanges();
        }

        public int DeleteRange(List<City> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            cityRepository.DeleteRange(instance);
            return cityRepository.SaveChanges();
        }

        public City FindById(decimal id)
        {
            return cityRepository.Find(id);
        }

        public IEnumerable<City> GetAll()
        {
            return cityRepository.GetAll();
        }
        public List<LoadCities> GetAllCitiesLoad()
        {
            return cityRepository.GetAllCitiesLoad();
        }
        public IEnumerable<City> GetAllCitiesByStates(decimal StateId)
        {
            return cityRepository.GetAllCitiesByStates(StateId);
        }

        public IEnumerable<StateProvince> GetStateByCityId(decimal Id)
        {
            return cityRepository.GetStateByCityId(Id);
        }

        public CityResponse GetCities(CityRequestModel request)
        {
            return cityRepository.GetCities(request);
        }

        public GetCityResponse GetCitySearchList(CityRequestModel request)
        {
            return cityRepository.GetCitySearchList(request);
        }
        public IEnumerable<VMGenericKeyValuePair<decimal>> GetAllCityByStatesKey(decimal StateId)
        {
            return cityRepository.GetAllCityByStatesKey(StateId);
        }

        public IEnumerable<VMGenericKeyValuePair<decimal>> GetAssignedCity(decimal ManagerId)
        {
            return cityRepository.GetAssignedCity(ManagerId);

        }
    }
}
