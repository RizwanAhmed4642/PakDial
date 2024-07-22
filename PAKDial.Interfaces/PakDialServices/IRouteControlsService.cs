using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.SqlViewModels;
using System.Collections.Generic;

namespace PAKDial.Interfaces.PakDialServices
{
    public interface IRouteControlsService
    {
        int Update(RouteControls instance);
        int Delete(decimal Id);
        int Add(RouteControls instance);
        int UpdateRange(List<RouteControls> instance);
        int DeleteRange(List<RouteControls> instance);
        int AddRange(List<RouteControls> instance);
        RouteControls FindById(decimal id);
        IEnumerable<RouteControls> GetAll();
        RouteControlsResponse GetRouteControls(RouteControlsRequestModel request);
        List<VRouteControl> GetRouteByModuleId(decimal ModuleId);
    }
}
