using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ViewModels;
using System;

namespace PAKDial.Interfaces.Repository
{
    public interface IActiveZoneRepository : IBaseRepository<ActiveZone, Decimal>
    {
        ActiveZonesResponse GetActiveZones(ActiveZonesRequestModel request);
        bool CheckExistance(ActiveZone instance);
        bool CheckZoneExist(decimal ZoneId);
        VMActiveZonesWrapper FindActiveZoneId(decimal id);

    }
}
