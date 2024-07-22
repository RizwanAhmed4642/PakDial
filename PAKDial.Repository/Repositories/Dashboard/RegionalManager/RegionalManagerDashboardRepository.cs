using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Interfaces.Repository.Dashboard.RegionalManager;
using PAKDial.StoreProcdures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Repository.Repositories.Dashboard.RegionalManager
{
    public class RegionalManagerDashboardRepository : IRegionalManagerDashboardRepository
    {
        public SpGetRegionalMgrByAreaWrapper SpGetRegionalMgrByAreaWrapper(string UserId)
        {
          return  DashboardStoreProcedure.SpGetRegionalMgrByAreaWrapper(UserId);
        }
    }
}
