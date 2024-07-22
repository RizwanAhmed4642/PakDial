using PAKDial.Domains.StoreProcedureModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.Dashboard.Admin
{
    public interface IAdminDashboardRepository
    {
        SpGetAdminWrapperProc SpGetAdminWrapperProc();
    }
}
