using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.RequestModels
{
    public class RouteControlsRequestModel : GetPagedListRequest
    {
        public decimal? ModuleId { get; set; }
    }
}
