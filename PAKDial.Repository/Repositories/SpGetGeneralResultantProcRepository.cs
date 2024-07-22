using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Interfaces.Repository.Dashboard.Others;
using PAKDial.StoreProcdures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Repository.Repositories
{
    public class SpGetGeneralResultantProcRepository : ISpGetGeneralResultantProcRepository
    {
        public SpGetGeneralResultantProc SpGetGeneralResultantProc()
        {
            return DashboardStoreProcedure.SpGetGeneralResultantProc();
        }
    }
}
