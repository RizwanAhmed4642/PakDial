using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Interfaces.Repository.Dashboard.Teller;
using PAKDial.Presentation.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.Dashboard.Teller
{
    public class TellerDashboardService: ITellerDashboardService
    {

        #region Prop

        private readonly ITellerDashboardRepository _ITellerDashboardRepository;
        #endregion
        #region Ctor

        public TellerDashboardService(ITellerDashboardRepository ITellerDashboardRepository)
        {
            _ITellerDashboardRepository = ITellerDashboardRepository;
        }


        #endregion
       
        #region Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public SpGetTellerDepositedOrderUId SpGetTellerDepositedOrderUId(string UserId)
        {
            return _ITellerDashboardRepository.SpGetTellerDepositedOrderUId(UserId);
        }
        #endregion

    }
}
