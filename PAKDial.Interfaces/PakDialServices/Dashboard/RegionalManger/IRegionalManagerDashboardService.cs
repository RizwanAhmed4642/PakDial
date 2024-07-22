using PAKDial.Domains.StoreProcedureModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.Dashboard.RegionalManger
{
    public interface  IRegionalManagerDashboardService
    {
        SpGetRegionalMgrByAreaWrapper SpGetRegionalMgrByAreaWrapper(string UserId);
    }
}
