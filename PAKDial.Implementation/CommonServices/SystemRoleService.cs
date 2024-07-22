using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.Repository;
using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using PAKDial.Domains.IdentityManagement;
using System.Threading.Tasks;
using System.Linq;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;

namespace PAKDial.Implementation.CommonServices
{
    public class SystemRoleService : ISystemRoleService
    {
        private readonly ISystemRoleRepository systemRoleRepository;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ISystemUserRepository systemUserRepository;

        public SystemRoleService(ISystemRoleRepository systemRoleRepository, RoleManager<ApplicationRole> _roleManager
            , ISystemUserRepository systemUserRepository)
        {
            this.systemRoleRepository = systemRoleRepository;
            this._roleManager = _roleManager;
            this.systemUserRepository = systemUserRepository;
        }
        public async Task<int> Add(ApplicationRole instance)
        {
            int Save = 0; //"State Not Added";
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            var CheckExistance = CheckRoleExistance(instance);
            if (!CheckExistance)
            {
                var result = await _roleManager.CreateAsync(instance);
                if(result.Succeeded == true)
                {
                    Save = 1; //1 State Added Successfully
                }
            }
            else
            {
                Save = 2; // State Already Exist.
            }
            return Save;
        }
        public int Update(ApplicationRole instance)
        {

            int Result = 0; //0-Record Not Updated Successfully
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            var CheckExistance = CheckRoleExistance(instance);
            if (!CheckExistance)
            {
                var EditRoles = FindById(instance.Id);
                EditRoles.Name = instance.Name;
                EditRoles.NormalizedName = instance.NormalizedName;
                EditRoles.UpdatedBy = instance.UpdatedBy;
                EditRoles.UpdatedDate = instance.UpdatedDate;
                EditRoles.ConcurrencyStamp = instance.ConcurrencyStamp;
                systemRoleRepository.Update(EditRoles);
                Result = systemRoleRepository.SaveChanges(); //1 Role Updated Successfully
            }
            else
            {
                Result = 2; //"2-Role Already Exits";
            }
            return Result;
        }

        public async Task<int> Delete(string Id)
        {
            int Results = 0; //"Role Not Deleted."
            var UserExists = systemUserRepository.IsExitUserByRoleId(Id);
            if (!UserExists)
            {
                var result = await _roleManager.DeleteAsync(await _roleManager.FindByIdAsync(Id));
                if (result.Succeeded)
                {
                    Results = 1; //1 Role Deleted Successfully
                }
            }
            else
            {
                Results = 2; // Please Delete its Users First.
            }
            return Results;
        }

        public int AddRange(List<AspNetRoles> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            systemRoleRepository.AddRange(instance);
            return systemRoleRepository.SaveChanges();
        }

        public int UpdateRange(List<AspNetRoles> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            systemRoleRepository.UpdateRange(instance);
            return systemRoleRepository.SaveChanges();
        }

        public int DeleteRange(List<AspNetRoles> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            systemRoleRepository.DeleteRange(instance);
            return systemRoleRepository.SaveChanges();
        }

        public AspNetRoles FindById(string id)
        {
            return systemRoleRepository.Find(id);
        }

        public IEnumerable<AspNetRoles> GetAll()
        {
            return systemRoleRepository.GetAll();
        }


        public bool CheckRoleExistance(ApplicationRole instance)
        {
            bool Results = false;
            if(!string.IsNullOrEmpty(instance.Id))
            {
                Results = systemRoleRepository.GetAll().Where(c => c.Name.ToLower().Trim() == instance.Name.ToLower().Trim() && c.Id != instance.Id).Count() > 0 ? true : false;

            }
            else
            {
                Results = systemRoleRepository.GetAll().Where(c => c.Name.ToLower().Trim() == instance.Name.ToLower().Trim()).Count() > 0 ? true : false;

            }
            return Results;
        }

        public RoleResponse GetRoles(RoleRequestModel request)
        {
            return systemRoleRepository.GetRoles(request);
        }
        public string GetRoleByUserId(string UserId)
        {
            return systemRoleRepository.GetRoleByUserId(UserId);
        }
    }
}
