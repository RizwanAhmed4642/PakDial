using PAKDial.Domains.StoreProcedureModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.Dashboard.RegionalManager
{
    public interface IRegionalManagerDashboardRepository
    {
        SpGetRegionalMgrByAreaWrapper SpGetRegionalMgrByAreaWrapper(string UserId);
    }
}
