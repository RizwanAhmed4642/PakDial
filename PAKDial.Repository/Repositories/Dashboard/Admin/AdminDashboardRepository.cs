using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Interfaces.Repository.Dashboard.Admin;
using PAKDial.StoreProcdures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Repository.Repositories.Dashboard.Admin
{
    public class AdminDashboardRepository : IAdminDashboardRepository
    {
        public SpGetAdminWrapperProc SpGetAdminWrapperProc()
        {
            return DashboardStoreProcedure.SpGetAdminWrapperProc();
        }
    }
}
