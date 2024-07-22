namespace PAKDial.Domains.StoreProcedureModel
{
    public class SpGetGenericCountYMWD
    {
        public SpGetGenericCountYMWD()
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
