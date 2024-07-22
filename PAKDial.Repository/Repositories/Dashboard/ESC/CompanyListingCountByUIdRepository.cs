using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Interfaces.Repository.Dashboard;
using PAKDial.StoreProcdures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Repository.Repositories.Dashboard
{
    public class CompanyListingCountByUIdRepository : ICompanyListingCountByUIdRepository
    {
        public SpGetESCListingOrderUserId getSpGetESCListingOrderUserId(string UserID)
        {
          return  DashboardStoreProcedure.SpGetESCListingOrderUserId(UserID);
            
        }
    }
}
