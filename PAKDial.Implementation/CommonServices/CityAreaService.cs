using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.Repository;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using PAKDial.Domains.ViewModels;

namespace PAKDial.Implementation.CommonServices
{
    public class CityAreaService : ICityAreaService
    {
        private readonly ICityAreaRepository cityAreaRepository;

        public CityAreaService(ICityAreaRepository cityAreaRepository)
        {
            this.cityAreaRepository = cityAreaRepository;
        }


        public int Add(CityArea instance)
        {
            int Save = 0; //"City Area Not Added";
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            var CheckExistance = GetAllAreasByCity((decimal)instance.CityId).Where(c => c.Name.ToLower().Trim() == instance.Name.ToLower().Trim()).Count();
            if (CheckExistance < 1)
            {
                cityAreaRepository.Add(instance);
                Save = cityAreaRepository.SaveChanges(); //1 City Area Added Successfully
            }
            else
            {
                Save = 2; // City Area Already Exist.
            }
            return Save;
        }
        public int Update(CityArea instance)
        {
            int Result = 0; //0-City Area Not Updated Successfully
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            var CheckExistance = GetAllAreasByCity((decimal)instance.CityId).Where(c => c.Name.ToLower().Trim() == instance.Name.ToLower().Trim() && c.Id != instance.Id).Count();
            if (CheckExistance < 1)
            {
                var EditCityArea = FindById(instance.Id);
                EditCityArea.CityId = instance.CityId;
                EditCityArea.StateId = instance.StateId;
                EditCityArea.CountryId = instance.CountryId;
                EditCityArea.Name = instance.Name;
                EditCityArea.UpdatedBy = instance.UpdatedBy;
                EditCityArea.UpdatedDate = instance.UpdatedDate;
                EditCityArea.AreaLat = instance.AreaLat;
                EditCityArea.AreaLong = instance.AreaLong;
                cityAreaRepository.Update(EditCityArea);
                Result = cityAreaRepository.SaveChanges(); //1-City Area Updated Successfully
            }
            else
            {
                Result = 2; //"2-City Area Already Exits";
            }
            return Result;
        }
        //Must be Change in Future
        public int Delete(decimal Id)
        {
            cityAreaRepository.Delete(FindById(Id));
            return cityAreaRepository.SaveChanges();
        }

        public int AddRange(List<CityArea> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            cityAreaRepository.AddRange(instance);
            return cityAreaRepository.SaveChanges();
        }

        public int UpdateRange(List<CityArea> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            cityAreaRepository.UpdateRange(instance);
            return cityAreaRepository.SaveChanges();
        }

        public int DeleteRange(List<CityArea> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            cityAreaRepository.DeleteRange(instance);
            return cityAreaRepository.SaveChanges();
        }

        public CityArea FindById(decimal id)
        {
            return cityAreaRepository.Find(id);
        }

        public IEnumerable<CityArea> GetAll()
        {
            return cityAreaRepository.GetAll();
        }

        public IEnumerable<CityArea> GetAllAreasByCity(decimal CityId)
        {
            return cityAreaRepository.GetAllAreasByCity(CityId);
        }

        public CityAreaResponse GetCitiesArea(CityAreaRequestModel request)
        {
            return cityAreaRepository.GetCitiesArea(request);
        }

        public StateCityResponse GetSCByCityAreaId(decimal Id)
        {
            return cityAreaRepository.GetSCByCityAreaId(Id);
        }

        public IEnumerable<VMGenericKeyValuePair<decimal>> GetAllAreaByCityIdKeyValue(decimal CityId)
        {
            return cityAreaRepository.GetAllAreaByCityIdKeyValue(CityId);
        }

        public VMControlCityAreas GetAllAreabyUserId(string UserId)
        {
           return cityAreaRepository.GetAllAreabyUserId(UserId);
        }

        public GetVCombineRegionsResponse GetCombineRegionUserId(CombineRegionsRequestModel request)
        {
            return cityAreaRepository.GetCombineRegionUserId(request);
        }

        public CityAreabyCityResponse GetAllAreasByCity(CityAreaRequestModel request)
        {
            return cityAreaRepository.GetAllAreasByCity(request);
        }
    }
}
