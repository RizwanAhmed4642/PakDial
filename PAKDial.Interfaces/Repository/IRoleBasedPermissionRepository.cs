using PAKDial.Domains.DomainModels;
using PAKDial.Domains.SqlViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository
{
    public interface IRoleBasedPermissionRepository : IBaseRepository<RoleBasedPermission,decimal>
    {
        int AddUpdatePermissions(List<RoleBasedPermission> permissions);
        List<VRolePermissions> GetRolePermissionById(string RoleId);
    }
}
