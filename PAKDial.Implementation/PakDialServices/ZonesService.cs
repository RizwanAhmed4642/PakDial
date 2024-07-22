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
    public class ZonesService : IZonesService
    {
        private readonly IZonesRepository zonesRepository;
        private readonly IActiveZoneRepository activeZoneRepository;

        public ZonesService(IZonesRepository zonesRepository,
            IActiveZoneRepository activeZoneRepository)
        {
            this.zonesRepository = zonesRepository;
            this.activeZoneRepository = activeZoneRepository;
        }
        public decimal Add(Zones instance)
        {
            decimal Save = 0; //"Zone Not Added";
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            var CheckExistance = zonesRepository.CheckExistance(instance);
            if (!CheckExistance)
            {
                zonesRepository.Add(instance);
                Save = zonesRepository.SaveChanges(); //1 Zone Added Successfully
            }
            else
            {
                Save = -2; // Zone Already Exist.
            }
            return Save;
        }
        public decimal Update(Zones instance)
        {
            decimal Result = 0; //Record Not Updated Successfully
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            bool result = zonesRepository.CheckExistance(instance);
            if (!result)
            {
                var EditZone = FindById(instance.Id);
                EditZone.Name = instance.Name;
                EditZone.UpdatedBy = instance.UpdatedBy;
                EditZone.UpdatedDate = instance.UpdatedDate;
                zonesRepository.Update(EditZone);
                Result = zonesRepository.SaveChanges(); //Record Updated Successfully
            }
            else
            {
                Result = -2; //"Zone Already Exits";
            }
            return Result;
        }
        public decimal Delete(decimal Id)
        {
            decimal Results = 0; //"Zone Not Deleted."
            var checkStates = activeZoneRepository.CheckZoneExist(Id);
            if (!checkStates)
            {
                zonesRepository.Delete(FindById(Id));
                Results = zonesRepository.SaveChanges(); // Zone Deleted Successfully.
            }
            else
            {
                Results = -2; // Please Delete its Active Zone First.
            }
            return Results;
        }

        public Zones FindById(decimal id)
        {
            return zonesRepository.Find(id);
        }

        public IEnumerable<Zones> GetAll()
        {
            return zonesRepository.GetAll();
        }

        public ZonesResponse GetZones(ZonesRequestModel request)
        {
            return zonesRepository.GetZones(request);
        }

        public IEnumerable<VMGenericKeyValuePair<decimal>> GetAllZonesKey()
        {
            return zonesRepository.GetAllZonesKey();
        }

        public GetZonesResponse GetSearchZones(ZonesRequestModel request)
        {
            return zonesRepository.GetSearchZones(request);
        }
    }
}
