using PAKDial.Domains.StoreProcedureModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.Dashboard.ZoneManager
{
    public interface IZoneManagerDashboardService
    {
        SpGetZoneMgrByAreaWrapper SpGetZoneMgrByAreaWrapper(string UserId);
    }
}
