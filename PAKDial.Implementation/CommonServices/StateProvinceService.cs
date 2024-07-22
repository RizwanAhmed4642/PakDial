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
    public class StateProvinceService : IStateProvinceService
    {
        private readonly IStateProvinceRepository stateProvinceRepository;
        private readonly ICityService cityService;

        public StateProvinceService(IStateProvinceRepository stateProvinceRepository, ICityService cityService)
        {
            this.stateProvinceRepository = stateProvinceRepository;
            this.cityService = cityService;
        }

        public int Add(StateProvince instance)
        {
            int Save = 0; //"State Not Added";
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            var CheckExistance = GetAllStatesByCountry(instance.CountryId).Where(c => c.Name.ToLower().Trim() == instance.Name.ToLower().Trim()).Count();
            if(CheckExistance < 1)
            {
                stateProvinceRepository.Add(instance);
                Save=stateProvinceRepository.SaveChanges(); //1 State Added Successfully
            }
            else
            {
                Save = 2; // State Already Exist.
            }
            return Save;
        }
        public int Update(StateProvince instance)
        {
            int Result = 0; //0-Record Not Updated Successfully
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            var CheckExistance = GetAllStatesByCountry(instance.CountryId).Where(c => c.Name.ToLower().Trim() == instance.Name.ToLower().Trim() && c.Id != instance.Id).Count();
            //bool result = countryRepository.FindCountryCodes(instance);
            if (CheckExistance < 1)
            {
                var EditStateProvince = FindById(instance.Id);
                EditStateProvince.CountryId = instance.CountryId;
                EditStateProvince.Name = instance.Name;
                EditStateProvince.Latitude = instance.Latitude;
                EditStateProvince.Longitude = instance.Longitude;
                EditStateProvince.UpdatedBy = instance.UpdatedBy;
                EditStateProvince.UpdatedDate = instance.UpdatedDate;
                stateProvinceRepository.Update(EditStateProvince);
                Result = stateProvinceRepository.SaveChanges(); //1-Record Updated Successfully
            }
            else
            {
                Result = 2; //"2-State/Province Already Exits";
            }
            return Result;
        }

        public int Delete(decimal Id)
        {
            int Results = 0; //"State/Province Not Deleted."
            var checkCities = cityService.GetAllCitiesByStates(Id).Count();
            if (checkCities < 1)
            {
                stateProvinceRepository.Delete(FindById(Id));
                Results = stateProvinceRepository.SaveChanges(); // State/Province Deleted Successfully.
            }
            else
            {
                Results = 2; // Please Delete its Cities First.
            }
            return Results;
        }

        public int AddRange(List<StateProvince> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            stateProvinceRepository.AddRange(instance);
            return stateProvinceRepository.SaveChanges();
        }

        public int UpdateRange(List<StateProvince> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            stateProvinceRepository.UpdateRange(instance);
            return stateProvinceRepository.SaveChanges();
        }

        public int DeleteRange(List<StateProvince> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            stateProvinceRepository.DeleteRange(instance);
            return stateProvinceRepository.SaveChanges();
        }

        public StateProvince FindById(decimal id)
        {
            return stateProvinceRepository.Find(id);
        }

        public IEnumerable<StateProvince> GetAll()
        {
            return stateProvinceRepository.GetAll();
        }

        public IEnumerable<StateProvince> GetAllStatesByCountry(decimal CountryId)
        {
            return stateProvinceRepository.GetAllStatesByCountry(CountryId);
        }

        public StateProvinceResponse GetStateProvinces(StateProvinceRequestModel request)
        {
            return stateProvinceRepository.GetStateProvinces(request);
        }

        public IEnumerable<VMGenericKeyValuePair<decimal>> GetAllStatesKeyValue()
        {
            return stateProvinceRepository.GetAllStatesKeyValue();
        }
    }
}
