using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System.Collections.Generic;

namespace PAKDial.Interfaces.PakDialServices
{
    public interface IModuleService
    {
        int Update(Modules instance);
        int Delete(decimal Id);
        int Add(Modules instance);
        int UpdateRange(List<Modules> instance);
        int DeleteRange(List<Modules> instance);
        int AddRange(List<Modules> instance);
        Modules FindById(decimal id);
        IEnumerable<Modules> GetAll();
        IEnumerable<Modules> GetAllIncludeRoutes();
        ModulesResponse GetModules(ModulesRequestModel request);
        List<Modules> GetModulesNotInRoles(string RoleId);
    }
}
