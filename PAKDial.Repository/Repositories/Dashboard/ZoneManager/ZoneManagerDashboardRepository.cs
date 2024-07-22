using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Interfaces.Repository.Dashboard.ZoneManager;
using PAKDial.StoreProcdures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Repository.Repositories.Dashboard.ZoneManager
{
    public class ZoneManagerDashboardRepository : IZoneManagerDashboardRepository
    {
        /// <summary>
        /// Get ListingCount Area By Zone Manger
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public SpGetZoneMgrByAreaWrapper SpGetZoneMgrByAreaWrapper(string UserId)
        {
          return  DashboardStoreProcedure.SpGetZoneMgrByAreaWrapper(UserId);
        }
    }
}
