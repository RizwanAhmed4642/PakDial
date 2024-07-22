using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System.Collections.Generic;

namespace PAKDial.Interfaces.CommonServices
{
    public interface ICountryService
    {
        int Update(Country instance);
        int Delete(decimal Id);
        int Add(Country instance);
        int UpdateRange(List<Country> instance);
        int DeleteRange(List<Country> instance);
        int AddRange(List<Country> instance);
        Country FindByCode(string CountryCode);
        Country FindById(decimal id);
        IEnumerable<Country> GetAll();
        CountryResponse GetCountries(CountryRequestModel request);
    }
}
