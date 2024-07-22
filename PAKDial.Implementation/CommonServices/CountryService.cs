using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.Repository;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PAKDial.Implementation.CommonServices
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository countryRepository;
        private readonly IStateProvinceService stateProvinceService;


        public CountryService(ICountryRepository countryRepository, IStateProvinceService stateProvinceService)
        {
            this.countryRepository = countryRepository;
            this.stateProvinceService = stateProvinceService;
        }
        public int Add(Country instance)
        {
            if(instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            countryRepository.Add(instance);
            return countryRepository.SaveChanges();
        }

        public int Update(Country instance)
        {
            int Result = 0; //Record Not Updated Successfully
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            bool result = countryRepository.FindCountryCodes(instance);
            if(result == true)
            {
                var Editcountry = FindById(instance.Id);
                Editcountry.Name = instance.Name;
                Editcountry.CountryCode = instance.CountryCode;
                Editcountry.Latitude = instance.Latitude;
                Editcountry.Longitude = instance.Longitude;
                Editcountry.UpdatedBy = instance.UpdatedBy;
                Editcountry.UpdatedDate = instance.UpdatedDate;
                countryRepository.Update(Editcountry);
                Result = countryRepository.SaveChanges(); //Record Updated Successfully
            }
            else
            {
                Result = 2; //"Country Code Already Exits";
            }
            return Result;
        }

        public int Delete(decimal Id)
        {
            int Results = 0; //"Country Not Deleted."
            var checkStates = stateProvinceService.GetAllStatesByCountry(Id).Count();
            if(checkStates < 1)
            {
                countryRepository.Delete(FindById(Id));
                Results = countryRepository.SaveChanges(); // Country Deleted Successfully.
            }
            else
            {
                Results = 2; // Please Delete its States/Provinces First.
            }
            return Results;
        }

        public int AddRange(List<Country> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            countryRepository.AddRange(instance);
            return countryRepository.SaveChanges();
        }

        public int UpdateRange(List<Country> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            countryRepository.UpdateRange(instance);
            return countryRepository.SaveChanges();
        }

        public int DeleteRange(List<Country> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            countryRepository.DeleteRange(instance);
            return countryRepository.SaveChanges();
        }

        public Country FindById(decimal id)
        {
            return countryRepository.Find(id);
        }

        public IEnumerable<Country> GetAll()
        {
            return countryRepository.GetAll();
        }

        public CountryResponse GetCountries(CountryRequestModel request)
        {
            return countryRepository.GetCountries(request);
        }

        public Country FindByCode(string CountryCode)
        {
            return countryRepository.FindByCode(CountryCode);
        }
    }
}
