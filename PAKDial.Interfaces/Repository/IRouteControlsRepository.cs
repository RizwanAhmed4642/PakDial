using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.SqlViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository
{
    public interface IRouteControlsRepository : IBaseRepository<RouteControls, decimal>
    {
        RouteControlsResponse GetRouteControls(RouteControlsRequestModel request);
        bool CheckExistance(RouteControls instance);
        bool CheckPermissionByRouteId(decimal Id);
        List<VRouteControl> GetRouteByModuleId(decimal ModuleId);

    }
}
