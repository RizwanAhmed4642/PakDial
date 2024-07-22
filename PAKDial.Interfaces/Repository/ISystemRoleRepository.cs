using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAKDial.Interfaces.Repository
{
    public interface ISystemRoleRepository : IBaseRepository<AspNetRoles,string>
    {
        RoleResponse GetRoles(RoleRequestModel request);
        string GetRoleByUserId(string UserId);
    }
}
