using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Interfaces.Repository.Dashboard.Teller;
using PAKDial.StoreProcdures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Repository.Repositories.Dashboard.Teller
{
    public class TellerDashboardRepository : ITellerDashboardRepository
    {

        #region Prop
        
        #endregion

        #region Ctor

        public TellerDashboardRepository()
        {
        }

        #endregion

        #region Method

        public SpGetTellerDepositedOrderUId SpGetTellerDepositedOrderUId(string UserId)
        {
            return DashboardStoreProcedure.SpGetTellerDepositedOrderUId(UserId);
        }

        #endregion



    }
}
