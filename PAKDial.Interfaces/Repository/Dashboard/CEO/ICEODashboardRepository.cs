﻿using PAKDial.Domains.StoreProcedureModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.Dashboard.CEO
{
    public interface ICEODashboardRepository
    {
        SpGetCEROWrapperProc SpGetCEROWrapperProc();
    }
}
