using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository
{
    public interface IModulesRepository : IBaseRepository<Modules,decimal>
    {
        ModulesResponse GetModules(ModulesRequestModel request);
        List<Modules> GetModulesNotInRoles(string RoleId);
        IEnumerable<Modules> GetAllIncludeRoutes();
        bool CheckExistance(Modules instance);
    }
}
