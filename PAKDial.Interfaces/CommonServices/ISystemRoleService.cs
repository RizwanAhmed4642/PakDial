using PAKDial.Domains.DomainModels;
using PAKDial.Domains.IdentityManagement;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PAKDial.Interfaces.CommonServices
{
    public interface ISystemRoleService
    {
        int Update(ApplicationRole instance);
        Task<int> Delete(string Id);

        Task<int> Add(ApplicationRole instance);
        int UpdateRange(List<AspNetRoles> instance);
        int DeleteRange(List<AspNetRoles> instance);
        int AddRange(List<AspNetRoles> instance);
        AspNetRoles FindById(string id);
        IEnumerable<AspNetRoles> GetAll();
        RoleResponse GetRoles(RoleRequestModel request);
        string GetRoleByUserId(string UserId);
    }
}
