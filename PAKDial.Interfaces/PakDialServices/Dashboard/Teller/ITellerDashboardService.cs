using PAKDial.Domains.StoreProcedureModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Controllers
{
    public interface ITellerDashboardService
    {
        SpGetTellerDepositedOrderUId SpGetTellerDepositedOrderUId(string UserId);
    }
}
