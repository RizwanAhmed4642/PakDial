using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Interfaces.PakDialServices.Dashboard;
using PAKDial.Interfaces.Repository.Dashboard;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.Dashboard
{
    public class CompanyListingCountByUIdService : ICompanyListingCountByUIdService
    {
        #region prop
         private readonly ICompanyListingCountByUIdRepository _ICompanyListingCountByUIdRepository;
        #endregion
        #region ctor
        public CompanyListingCountByUIdService(ICompanyListingCountByUIdRepository ICompanyListingCountByUIdRepository)
        {
            _ICompanyListingCountByUIdRepository = ICompanyListingCountByUIdRepository;
        }
        #endregion


        #region Method
        public SpGetESCListingOrderUserId getESCCompanyListingCountByUId(string UserID)
        {
            return _ICompanyListingCountByUIdRepository.getSpGetESCListingOrderUserId(UserID);
        }

        #endregion
    }
}
