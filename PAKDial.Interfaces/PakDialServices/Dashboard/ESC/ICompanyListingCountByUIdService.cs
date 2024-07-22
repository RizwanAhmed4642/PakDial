using PAKDial.Domains.StoreProcedureModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.Dashboard
{
  public  interface ICompanyListingCountByUIdService
    {
        SpGetESCListingOrderUserId getESCCompanyListingCountByUId(string UserID);
    }
}
