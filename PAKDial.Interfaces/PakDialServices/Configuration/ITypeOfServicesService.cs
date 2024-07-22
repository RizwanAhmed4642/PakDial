using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels.Configuration;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ResponseModels.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.Configuration
{
    public interface ITypeOfServicesService
    {
        decimal Update(TypeOfServices Instance);
        decimal Add(TypeOfServices Instance);
        decimal Delete(int Id);
        TypeOfServices GetById(int Id);
        TypeOfServices GetByName(string Name);
        IEnumerable<TypeOfServices> GetAll();
        TypeOfServicesResponse GetTypeOfService(TypeOfServicesRequestModel request);
        GetServicesTypeResponse GetTypeofServicesList(TypeOfServicesRequestModel request);
    }
}
