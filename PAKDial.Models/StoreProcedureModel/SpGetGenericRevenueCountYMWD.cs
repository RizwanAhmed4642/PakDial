namespace PAKDial.Domains.StoreProcedureModel
{
    public class SpGetGenericRevenueCountYMWD
    {
        public SpGetGenericRevenueCountYMWD()
        {
            TotalRevenue = 0;
            YearlyRevenue = 0;
            MonthlyRevenue = 0;
            WeeklyRevenue = 0;
            DailyRevenue = 0;
        }
        public decimal TotalRevenue { get; set; }

        public decimal YearlyRevenue { get; set; }

        public decimal MonthlyRevenue { get; set; }

        public decimal WeeklyRevenue { get; set; }

        public decimal DailyRevenue { get; set; }

    }
}
