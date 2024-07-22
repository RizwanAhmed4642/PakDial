using PAKDial.Domains.SqlViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ResponseModels
{
    public class RouteControlsResponse
    {
        public int RowCount { get; set; }

        public IEnumerable<VRouteControl> RouteControl { get; set; }
    }
}
