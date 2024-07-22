using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.Repository;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.SqlViewModels;
using System.Collections.Generic;

namespace PAKDial.Implementation.CommonServices
{
    public class RoleBasedPermissionService : IRoleBasedPermissionService
    {
        private readonly IRoleBasedPermissionRepository roleBasedPermissionRepository;

        public RoleBasedPermissionService(IRoleBasedPermissionRepository roleBasedPermissionRepository)
        {
            this.roleBasedPermissionRepository = roleBasedPermissionRepository;
        }
        public int AddUpdatePermissions(List<RoleBasedPermission> instance)
        {
            return roleBasedPermissionRepository.AddUpdatePermissions(instance);
        }
        public RoleBasedPermission FindById(decimal id)
        {
            return roleBasedPermissionRepository.Find(id);
        }

        public IEnumerable<RoleBasedPermission> GetAll()
        {
            return roleBasedPermissionRepository.GetAll();
        }

        public List<VRolePermissions> GetRolePermissionById(string RoleId)
        {
            return roleBasedPermissionRepository.GetRolePermissionById(RoleId);
        }


    }
}
