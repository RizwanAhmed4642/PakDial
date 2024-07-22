using PAKDial.Domains.DomainModels;
using PAKDial.Domains.SqlViewModels;
using System.Collections.Generic;

namespace PAKDial.Interfaces.CommonServices
{
    public interface IRoleBasedPermissionService
    {
        int AddUpdatePermissions(List<RoleBasedPermission> instance);
        RoleBasedPermission FindById(decimal id);
        IEnumerable<RoleBasedPermission> GetAll();
        List<VRolePermissions> GetRolePermissionById(string RoleId);
    }
}
