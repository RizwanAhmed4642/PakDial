﻿using PAKDial.Domains.StoreProcedureModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.Dashboard.Admin
{
   public interface IAdminDashboardService
    {
        SpGetAdminWrapperProc SpGetAdminWrapperProc();
    }
}