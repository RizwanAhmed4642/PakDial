using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices
{
    public interface IZonesService
    {
        decimal Update(Zones instance);
        decimal Delete(decimal Id);
        decimal Add(Zones instance);
        Zones FindById(decimal id);
        IEnumerable<Zones> GetAll();
        IEnumerable<VMGenericKeyValuePair<decimal>> GetAllZonesKey();
        ZonesResponse GetZones(ZonesRequestModel request);
        GetZonesResponse GetSearchZones(ZonesRequestModel request);
    }
}
