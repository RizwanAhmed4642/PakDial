using PAKDial.Domains.DomainModels;
using PAKDial.Domains.SqlViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.CommonServices
{
    public interface IUserBasedPermissionService
    {
        int AddUpdatePermissions(List<UserBasedPermission> instance);
        UserBasedPermission FindById(decimal id);
        IEnumerable<UserBasedPermission> GetAll();

        List<VUserPermissions> GetUserPermissionById(string UserId);
        List<UserBasedPermission> GetAssignedPermissions(string UserId);
        void DeleteDuplicatePermission(string UserId, string RoleId);
    }
}
