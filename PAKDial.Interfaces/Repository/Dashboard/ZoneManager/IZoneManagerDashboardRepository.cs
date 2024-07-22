using PAKDial.Domains.StoreProcedureModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.Dashboard.ZoneManager
{
    public interface IZoneManagerDashboardRepository
    {
        SpGetZoneMgrByAreaWrapper SpGetZoneMgrByAreaWrapper(string UserId);
    }
}
