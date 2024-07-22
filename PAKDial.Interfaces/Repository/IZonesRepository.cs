using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ViewModels;
using System;
using System.Collections.Generic;

namespace PAKDial.Interfaces.Repository
{
    public interface IZonesRepository : IBaseRepository<Zones, Decimal>
    {
        ZonesResponse GetZones(ZonesRequestModel request);
        bool CheckExistance(Zones instance);
        IEnumerable<VMGenericKeyValuePair<decimal>> GetAllZonesKey();
        GetZonesResponse GetSearchZones(ZonesRequestModel request);

    }
}
