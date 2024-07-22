using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository
{
    public interface ISystemUserRepository : IBaseRepository<AspNetUsers, string>
    {
        SystemUserResponse GetAllSystemUsers(SystemUserRequestModel request);
        bool IsExitUserByRoleId(string RoleId);
        AspNetUsers FindByEmail(string Email);
        LoginMenuResponseModel GetRoleUserMenu(string UserId, string RoleId);
        CustomerRegistrationResponse RegisterCustomer(RegisterViewModels register);
    }
}
