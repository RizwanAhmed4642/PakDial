using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository
{
    public interface IStateProvinceRepository : IBaseRepository <StateProvince,decimal>
    {
        IEnumerable<StateProvince> GetAllStatesByCountry(decimal CountryId);
        StateProvinceResponse GetStateProvinces(StateProvinceRequestModel request);
        IEnumerable<VMGenericKeyValuePair<decimal>> GetAllStatesKeyValue();

    }
}
