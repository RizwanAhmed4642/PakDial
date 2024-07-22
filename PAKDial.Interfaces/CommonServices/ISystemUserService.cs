using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.UserEndViewModel;
using System.Collections.Generic;

namespace PAKDial.Interfaces.CommonServices
{
    public interface ISystemUserService
    {
        AspNetUsers FindById(string id);
        bool IsExitUserByRoleId(string RoleId);
        IEnumerable<AspNetUsers> GetAll();
        AspNetUsers FindByEmail(string Email);
        LoginMenuResponseModel GetRoleUserMenu(string UserId, string RoleId);
        SystemUserResponse GetAllSystemUsers(SystemUserRequestModel request);

        CustomerRegistrationResponse RegisterCustomer(RegisterViewModels register);

    }
}
