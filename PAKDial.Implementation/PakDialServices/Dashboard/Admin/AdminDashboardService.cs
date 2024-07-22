using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Interfaces.PakDialServices.Dashboard.Admin;
using PAKDial.Interfaces.Repository.Dashboard.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.Dashboard.Admin
{
    public class AdminDashboardService : IAdminDashboardService
    {

        #region Prop
        private readonly IAdminDashboardRepository _IAdminDashboardRepository;
        #endregion

        #region Ctor
        public AdminDashboardService(IAdminDashboardRepository IAdminDashboardRepository)
        {
            _IAdminDashboardRepository = IAdminDashboardRepository;
        }

        #endregion

        #region Method

        public SpGetAdminWrapperProc SpGetAdminWrapperProc()
        {
             return _IAdminDashboardRepository.SpGetAdminWrapperProc();
        }

        #endregion
    }
}
