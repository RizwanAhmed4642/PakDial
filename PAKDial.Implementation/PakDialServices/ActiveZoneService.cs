using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices
{
    public class ActiveZoneService : IActiveZoneService
    {
        private readonly IActiveZoneRepository activeZoneRepository;
        public ActiveZoneService(IActiveZoneRepository activeZoneRepository)
        {
            this.activeZoneRepository = activeZoneRepository;
        }
        public decimal Add(ActiveZone instance)
        {
            decimal Save = 0; //"Active Zone Not Added";
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            var CheckExistance = activeZoneRepository.CheckExistance(instance);
            if (!CheckExistance)
            {
                activeZoneRepository.Add(instance);
                Save = activeZoneRepository.SaveChanges(); //1 Active Zone Added Successfully
            }
            else
            {
                Save = -2; // Active Zone Already Exist.
            }
            return Save;
        }
        public decimal Update(ActiveZone instance)
        {
            decimal Result = 0; //Record Not Updated Successfully
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            bool result = activeZoneRepository.CheckExistance(instance);
            if (!result)
            {
                var EditActiveZone = FindById(instance.Id);
                EditActiveZone.StateId = instance.StateId;
                EditActiveZone.CityId = instance.CityId;
                EditActiveZone.ZoneId = instance.ZoneId;
                EditActiveZone.CityAreaId = instance.ZoneId;
                EditActiveZone.UpdatedBy = instance.UpdatedBy;
                EditActiveZone.UpdatedDate = instance.UpdatedDate;
                activeZoneRepository.Update(EditActiveZone);
                Result = activeZoneRepository.SaveChanges(); //Record Updated Successfully
            }
            else
            {
                Result = -2; //"Active Zone Already Exits";
            }
            return Result;
        }
        public decimal Delete(decimal Id)
        {
            decimal Results = 0; //"Active Zone Not Deleted."
            //var checkStates = activeZoneRepository.CheckZoneExist(Id);
            //if (!checkStates)
            //{
            //    zonesRepository.Delete(FindById(Id));
            //    Results = zonesRepository.SaveChanges(); // Active Zone Deleted Successfully.
            //}
            //else
            //{
            //    Results = -2; // Please Delete its Active Zone First.
            //}
            activeZoneRepository.Delete(FindById(Id));
            Results = activeZoneRepository.SaveChanges(); // Active Zone Deleted Successfully.
            return Results;
        }

        public ActiveZone FindById(decimal id)
        {
            return activeZoneRepository.Find(id); ;
        }
        public IEnumerable<ActiveZone> GetAll()
        {
            return activeZoneRepository.GetAll();
        }
        public ActiveZonesResponse GetActiveZones(ActiveZonesRequestModel request)
        {
            return activeZoneRepository.GetActiveZones(request);
        }

        public VMActiveZonesWrapper FindActiveZoneId(decimal id)
        {
            return activeZoneRepository.FindActiveZoneId(id);
        }
    }
}
