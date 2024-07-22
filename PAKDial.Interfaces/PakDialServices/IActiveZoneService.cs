using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ViewModels;
using System.Collections.Generic;

namespace PAKDial.Interfaces.PakDialServices
{
    public interface IActiveZoneService
    {
        decimal Update(ActiveZone instance);
        decimal Delete(decimal Id);
        decimal Add(ActiveZone instance);
        ActiveZone FindById(decimal id);
        VMActiveZonesWrapper FindActiveZoneId(decimal id);

        IEnumerable<ActiveZone> GetAll();
        ActiveZonesResponse GetActiveZones(ActiveZonesRequestModel request);
    }
}
