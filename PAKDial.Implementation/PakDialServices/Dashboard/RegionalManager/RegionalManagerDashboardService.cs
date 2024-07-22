using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Interfaces.PakDialServices.Dashboard.RegionalManger;
using PAKDial.Interfaces.Repository.Dashboard.RegionalManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.Dashboard.RegionalManager
{
    public class RegionalManagerDashboardService : IRegionalManagerDashboardService
    {
        #region Prop

        private readonly IRegionalManagerDashboardRepository _IRegionalManagerDashboardRepository;
        #endregion

        #region MyRegion
        public RegionalManagerDashboardService(IRegionalManagerDashboardRepository IRegionalManagerDashboardRepository)
        {
            _IRegionalManagerDashboardRepository = IRegionalManagerDashboardRepository;
        }
        #endregion
        public SpGetRegionalMgrByAreaWrapper SpGetRegionalMgrByAreaWrapper(string UserId)
        {
          return  _IRegionalManagerDashboardRepository.SpGetRegionalMgrByAreaWrapper(UserId);
        }
    }
}
