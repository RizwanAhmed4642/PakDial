using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository
{
    public interface ICountryRepository : IBaseRepository<Country,Decimal>
    {
        CountryResponse GetCountries(CountryRequestModel request);
        Country FindByCode(string CountryCode);
        bool FindCountryCodes(Country instance);
    }
}
