using PAKDial.Domains.StoreProcedureModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.Dashboard.Teller
{
    public interface ITellerDashboardRepository
    {
        SpGetTellerDepositedOrderUId SpGetTellerDepositedOrderUId(string UserId);
    }
}
