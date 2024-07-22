using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels.Configuration;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ResponseModels.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.Configuration
{
    public interface ITypeOfServicesRepository : IBaseRepository<TypeOfServices, decimal>
    {
        decimal UpdateTypeOfServices(TypeOfServices Instance);
        decimal AddTypeOfServices(TypeOfServices Instance);
        decimal DeleteTypeOfServices(int Id);
        TypeOfServicesResponse GetTypeOfServices(TypeOfServicesRequestModel request);
        GetServicesTypeResponse GetTypeofServicesList(TypeOfServicesRequestModel request);
        TypeOfServices GetByName(string Name);
        bool CheckExistance(TypeOfServices instance);
    }
}
