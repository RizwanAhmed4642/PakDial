using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Interfaces.Repository.Dashboard.CEO;
using PAKDial.StoreProcdures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Repository.Repositories.Dashboard.CEO
{
    public class CEODashboardRepository : ICEODashboardRepository
    {
        public SpGetCEROWrapperProc SpGetCEROWrapperProc()
        {
            return DashboardStoreProcedure.SpGetCEROWrapperProc();
        }
    }
}
