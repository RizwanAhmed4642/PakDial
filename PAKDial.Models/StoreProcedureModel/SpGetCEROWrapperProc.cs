using System.Collections.Generic;

namespace PAKDial.Domains.StoreProcedureModel
{
    public class SpGetCEROWrapperProc
    {
        public SpGetCEROWrapperProc()
        {
            ListingCount = new SpGetGenericCountYMWD();
            ApprovedOrderCount = new SpGetGenericCountYMWD();
            ApprovedOrderRevenueCount = new SpGetGenericRevenueCountYMWD();
            OrderMonthWiseRevnue = new List<SpGetGenericOrderRevenueMonthWise>();
            OrderYearWiseRevnue = new List<SpGetGenericOrderRevenueYearWise>();
            StateWiseRevenue = new List<SpGetGenericStateWiseRevnue>();
        }
        public SpGetGenericCountYMWD ListingCount { get; set; }
        public SpGetGenericCountYMWD ApprovedOrderCount { get; set; }
        public SpGetGenericRevenueCountYMWD ApprovedOrderRevenueCount { get; set; }
        public List<SpGetGenericOrderRevenueMonthWise> OrderMonthWiseRevnue { get; set; }
        public List<SpGetGenericOrderRevenueYearWise> OrderYearWiseRevnue { get; set; }
        public List<SpGetGenericStateWiseRevnue> StateWiseRevenue { get; set; }
    }
}
