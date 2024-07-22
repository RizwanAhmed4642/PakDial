using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.Repository;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System.Collections.Generic;
using PAKDial.Domains.UserEndViewModel;

namespace PAKDial.Implementation.CommonServices
{
    public class SystemUserService : ISystemUserService
    {
        private readonly ISystemUserRepository systemUserRepository;

        public SystemUserService(ISystemUserRepository systemUserRepository)
        {
            this.systemUserRepository = systemUserRepository;
        }
        public AspNetUsers FindById(string id)
        {
            return systemUserRepository.Find(id);
        }

        public AspNetUsers FindByEmail(string Email)
        {
            return systemUserRepository.FindByEmail(Email);
        }

        public IEnumerable<AspNetUsers> GetAll()
        {
            return systemUserRepository.GetAll();
        }

        public SystemUserResponse GetAllSystemUsers(SystemUserRequestModel request)
        {
            return systemUserRepository.GetAllSystemUsers(request);
        }

        public bool IsExitUserByRoleId(string RoleId)
        {
            return systemUserRepository.IsExitUserByRoleId(RoleId);
        }

        public LoginMenuResponseModel GetRoleUserMenu(string UserId, string RoleId)
        {
            return systemUserRepository.GetRoleUserMenu(UserId,RoleId);
        }

        public CustomerRegistrationResponse RegisterCustomer(RegisterViewModels register)
        {
            return systemUserRepository.RegisterCustomer(register);
        }
    }
}
