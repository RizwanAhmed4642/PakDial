using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Interfaces.PakDialServices.Dashboard.CEO;
using PAKDial.Interfaces.Repository.Dashboard.CEO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.Dashboard.CEO
{
    public class CEODashboardService : ICEODashboardService
    {
        #region Prop
        private readonly ICEODashboardRepository _ICEODashboardRepository;
        #endregion

        #region Ctor
        public CEODashboardService(ICEODashboardRepository ICEODashboardRepository)
        {
            _ICEODashboardRepository = ICEODashboardRepository;
        }
        #endregion

        #region Method
        public SpGetCEROWrapperProc SpGetCEROWrapperProc()
        {
            return _ICEODashboardRepository.SpGetCEROWrapperProc();
        }
        #endregion

    }
}
