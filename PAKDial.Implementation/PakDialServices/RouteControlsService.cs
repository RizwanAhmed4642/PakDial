using PAKDial.Interfaces.PakDialServices;
using PAKDial.Interfaces.Repository;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.SqlViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices
{
    public class RouteControlsService : IRouteControlsService
    {
        private readonly IRouteControlsRepository routeControlsRepository;

        public RouteControlsService(IRouteControlsRepository routeControlsRepository)
        {
            this.routeControlsRepository = routeControlsRepository;
        }
        public int Add(RouteControls instance)
        {
            int Save = 0; //"Routes Not Added";
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            var CheckExistance = routeControlsRepository.CheckExistance(instance);
            if (!CheckExistance)
            {
                routeControlsRepository.Add(instance);
                Save = routeControlsRepository.SaveChanges(); //1 Routes Added Successfully
            }
            else
            {
                Save = 2; // Routes Already Exist.
            }
            return Save;
        }
        public int Update(RouteControls instance)
        {
            int Result = 0; //Record Not Updated Successfully
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            bool result = routeControlsRepository.CheckExistance(instance);
            if (!result)
            {
                var EditRoute = FindById(instance.Id);
                EditRoute.ModuleId = instance.ModuleId;
                EditRoute.IsShown = instance.IsShown;
                EditRoute.MenuName = instance.MenuName;
                EditRoute.Description = instance.Description;
                EditRoute.Controller = instance.Controller;
                EditRoute.Action = instance.Action;
                EditRoute.Status = instance.Status;
                EditRoute.UpdatedBy = instance.UpdatedBy;
                EditRoute.UpdatedDate = instance.UpdatedDate;
                routeControlsRepository.Update(EditRoute);
                Result = routeControlsRepository.SaveChanges(); //Record Updated Successfully
            }
            else
            {
                Result = 2; //"Routes Already Exits";
            }
            return Result;
        }
        public int Delete(decimal Id)
        {
            int Results = 0; //"Routes Not Deleted."
            var checkRoutes = routeControlsRepository.CheckPermissionByRouteId(Id);
            if (!checkRoutes)
            {
                routeControlsRepository.Delete(FindById(Id));
                Results = routeControlsRepository.SaveChanges(); // Routes Deleted Successfully.
            }
            else
            {
                Results = 2; // Please Delete its Permissions First.
            }
            return Results;
        }
        public int AddRange(List<RouteControls> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            routeControlsRepository.AddRange(instance);
            return routeControlsRepository.SaveChanges();
        }
        public int UpdateRange(List<RouteControls> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            routeControlsRepository.UpdateRange(instance);
            return routeControlsRepository.SaveChanges();
        }
        public int DeleteRange(List<RouteControls> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            routeControlsRepository.DeleteRange(instance);
            return routeControlsRepository.SaveChanges();
        }
        public RouteControls FindById(decimal id)
        {
            return routeControlsRepository.Find(id);
        }
        public IEnumerable<RouteControls> GetAll()
        {
            return routeControlsRepository.GetAll();
        }
        public RouteControlsResponse GetRouteControls(RouteControlsRequestModel request)
        {
            return routeControlsRepository.GetRouteControls(request);
        }
        public List<VRouteControl> GetRouteByModuleId(decimal ModuleId)
        {
            return routeControlsRepository.GetRouteByModuleId(ModuleId);
        }
    }
}
