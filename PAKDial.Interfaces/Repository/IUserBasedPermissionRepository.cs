using PAKDial.Domains.DomainModels;
using PAKDial.Domains.SqlViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository
{
    public interface IUserBasedPermissionRepository : IBaseRepository<UserBasedPermission,decimal>
    {
        List<VUserPermissions> GetUserPermissionById (string UserId);
        int AddUpdatePermissions(List<UserBasedPermission> instance);
        List<UserBasedPermission> GetAssignedPermissions(string UserId);
        void DeleteDuplicatePermission(string UserId,string RoleId);

    }
}
