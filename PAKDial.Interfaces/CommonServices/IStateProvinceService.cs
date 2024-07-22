using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ViewModels;
using System.Collections.Generic;

namespace PAKDial.Interfaces.CommonServices
{
    public interface IStateProvinceService
    {
        int Update(StateProvince instance);
        int Delete(decimal Id);
        int Add(StateProvince instance);
        int UpdateRange(List<StateProvince> instance);
        int DeleteRange(List<StateProvince> instance);
        int AddRange(List<StateProvince> instance);
        StateProvince FindById(decimal id);
        IEnumerable<StateProvince> GetAll();
        IEnumerable<VMGenericKeyValuePair<decimal>> GetAllStatesKeyValue();
        IEnumerable<StateProvince> GetAllStatesByCountry(decimal CountryId);
        StateProvinceResponse GetStateProvinces(StateProvinceRequestModel request);
    }
}
