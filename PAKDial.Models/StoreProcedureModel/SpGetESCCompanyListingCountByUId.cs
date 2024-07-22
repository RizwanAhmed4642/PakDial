using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.StoreProcedureModel
{
    public class SpGetESCCompanyListingCountByUId
    {
        public SpGetESCCompanyListingCountByUId()
        {
            TotalCount = 0;
            YearlyCount = 0;
            MonthlyCount = 0;
            WeeklyCount = 0;
            DailyCount = 0;
        }
        public int TotalCount { get; set; }

        public int YearlyCount { get; set; }

        public int MonthlyCount { get; set; }

        public int WeeklyCount { get; set; }

        public int DailyCount { get; set; }

    }
}
