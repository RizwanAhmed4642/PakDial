using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Interfaces.PakDialServices.Dashboard.ZoneManager;
using PAKDial.Interfaces.Repository.Dashboard.ZoneManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.Dashboard.ZoneManager
{
    public class ZoneManagerDashboardService : IZoneManagerDashboardService
    {
        #region Members
        private readonly IZoneManagerDashboardRepository _IZoneManagerDashboardRepository;
        #endregion
        #region Ctor
        public ZoneManagerDashboardService(IZoneManagerDashboardRepository IZoneManagerDashboardRepository)
        {
            _IZoneManagerDashboardRepository = IZoneManagerDashboardRepository;
        }
        #endregion

        #region Method
        /// <summary>
        /// Get Listing Count from city Area by zone Manager 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public SpGetZoneMgrByAreaWrapper SpGetZoneMgrByAreaWrapper(string UserId)
        {
            return _IZoneManagerDashboardRepository.SpGetZoneMgrByAreaWrapper(UserId);
        }
        #endregion
        
    }
}
