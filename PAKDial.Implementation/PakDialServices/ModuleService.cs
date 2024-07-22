using PAKDial.Interfaces.PakDialServices;
using PAKDial.Interfaces.Repository;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PAKDial.Implementation.PakDialServices
{
    public class ModuleService : IModuleService
    {
        private readonly IModulesRepository modulesRepository;
        private readonly IRouteControlsRepository routeControlsRepository;

        public ModuleService(IModulesRepository modulesRepository, IRouteControlsRepository routeControlsRepository)
        {
            this.modulesRepository = modulesRepository;
            this.routeControlsRepository = routeControlsRepository;
        }
        public int Add(Modules instance)
        {
            int Save = 0; //"Module Not Added";
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            var CheckExistance = modulesRepository.CheckExistance(instance);
            if (!CheckExistance)
            {
                modulesRepository.Add(instance);
                Save = modulesRepository.SaveChanges(); //1 Module Added Successfully
            }
            else
            {
                Save = 2; // Module Already Exist.
            }
            return Save;
        }
        public int Update(Modules instance)
        {
            int Result = 0; //Record Not Updated Successfully
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            bool result = modulesRepository.CheckExistance(instance);
            if (!result)
            {
                var EditModule = FindById(instance.Id);
                EditModule.Name = instance.Name;
                EditModule.ClassName = instance.ClassName;
                EditModule.UpdatedBy = instance.UpdatedBy;
                EditModule.UpdatedDate = instance.UpdatedDate;
                modulesRepository.Update(EditModule);
                Result = modulesRepository.SaveChanges(); //Record Updated Successfully
            }
            else
            {
                Result = 2; //"Module Already Exits";
            }
            return Result;
        }
        public int Delete(decimal Id)
        {
            int Results = 0; //"Module Not Deleted."
            var checkStates = routeControlsRepository.GetRouteByModuleId(Id).Count() > 0 ? true : false;
            if (!checkStates)
            {
                modulesRepository.Delete(FindById(Id));
                Results = modulesRepository.SaveChanges(); // Module Deleted Successfully.
            }
            else
            {
                Results = 2; // Please Delete its Routes First.
            }
            return Results;
        }
        public int AddRange(List<Modules> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            modulesRepository.AddRange(instance);
            return modulesRepository.SaveChanges();
        }
        public int UpdateRange(List<Modules> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            modulesRepository.UpdateRange(instance);
            return modulesRepository.SaveChanges();
        }
        public int DeleteRange(List<Modules> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            modulesRepository.DeleteRange(instance);
            return modulesRepository.SaveChanges();
        }
        public Modules FindById(decimal id)
        {
            return modulesRepository.Find(id);
        }
        public IEnumerable<Modules> GetAll()
        {
            return modulesRepository.GetAll();
        }

        public ModulesResponse GetModules(ModulesRequestModel request)
        {
            return modulesRepository.GetModules(request);
        }

        public List<Modules> GetModulesNotInRoles(string RoleId)
        {
            return modulesRepository.GetModulesNotInRoles(RoleId);
        }

        public IEnumerable<Modules> GetAllIncludeRoutes()
        {
            return modulesRepository.GetAllIncludeRoutes();
        }
    }
}
