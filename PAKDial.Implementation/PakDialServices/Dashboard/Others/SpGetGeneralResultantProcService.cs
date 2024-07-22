using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Interfaces.PakDialServices.Dashboard.Others;
using PAKDial.Interfaces.Repository.Dashboard.Admin;
using PAKDial.Interfaces.Repository.Dashboard.Others;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.Dashboard.Others
{
   public class SpGetGeneralResultantProcService : ISpGetGeneralResultantProcService
    {
        #region Prop
        private readonly ISpGetGeneralResultantProcRepository _ISpGetGeneralResultantProcRepository;
        #endregion

        #region Ctor
        public SpGetGeneralResultantProcService(ISpGetGeneralResultantProcRepository ISpGetGeneralResultantProcRepository)
        {
            _ISpGetGeneralResultantProcRepository = ISpGetGeneralResultantProcRepository;
        }

        public SpGetGeneralResultantProc SpGetGeneralResultantProc()
        {
        return _ISpGetGeneralResultantProcRepository.SpGetGeneralResultantProc();
        }

        #endregion
    }
}
