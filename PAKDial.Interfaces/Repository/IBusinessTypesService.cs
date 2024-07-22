using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository
{
    public interface IBusinessTypesService
    {
        decimal Add(BusinessTypes instance);
        decimal Update(BusinessTypes instance);
        decimal Delete(decimal Id);
        BusinessTypes FindById(decimal id);
        IEnumerable<BusinessTypes> GetAll();
        IEnumerable<VMGenericKeyValuePair<decimal>> GetAllBusinessTypeKey();
        BusinessTypesResponse GetBusinessTypes(BusinessTypesRequestModel request);
    }
}
