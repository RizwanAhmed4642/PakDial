using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.Repository;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.SqlViewModels;
using System.Collections.Generic;

namespace PAKDial.Implementation.CommonServices
{
    public class UserBasedPermissionService : IUserBasedPermissionService
    {
        private readonly IUserBasedPermissionRepository userBasedPermissionRepository;

        public UserBasedPermissionService(IUserBasedPermissionRepository userBasedPermissionRepository)
        {
            this.userBasedPermissionRepository = userBasedPermissionRepository;
        }
        public int AddUpdatePermissions(List<UserBasedPermission> instance)
        {
           return userBasedPermissionRepository.AddUpdatePermissions(instance);
        }

        public UserBasedPermission FindById(decimal id)
        {
            return userBasedPermissionRepository.Find(id);
        }

        public IEnumerable<UserBasedPermission> GetAll()
        {
            return userBasedPermissionRepository.GetAll();
        }

        public List<UserBasedPermission> GetAssignedPermissions(string UserId)
        {
            return userBasedPermissionRepository.GetAssignedPermissions(UserId);
        }

        public List<VUserPermissions> GetUserPermissionById(string UserId)
        {
            return userBasedPermissionRepository.GetUserPermissionById(UserId);
        }

        public void DeleteDuplicatePermission(string UserId, string RoleId)
        {
            userBasedPermissionRepository.DeleteDuplicatePermission(UserId, RoleId);
        }
    }
}
