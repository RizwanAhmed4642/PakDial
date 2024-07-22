using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository
{
    public interface IBusinessTypesRepository : IBaseRepository<BusinessTypes,decimal>
    {
        BusinessTypesResponse GetBusinessTypes(BusinessTypesRequestModel request);
        IEnumerable<VMGenericKeyValuePair<decimal>> GetAllBusinessTypeKey();
        bool CheckExistance(BusinessTypes instance);
       
    }
}
