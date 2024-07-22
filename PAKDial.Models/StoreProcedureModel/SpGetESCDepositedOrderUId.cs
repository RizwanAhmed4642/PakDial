namespace PAKDial.Domains.StoreProcedureModel
{
    public class SpGetESCDepositedOrderUId
    {
        public SpGetESCDepositedOrderUId()
        {
            TotalDepCount = 0;
            YearlyDepCount = 0;
            MonthlyDepCount = 0;
            WeeklyDepCount = 0;
            DailyDepCount = 0;
        }
        public int TotalDepCount { get; set; }

        public int YearlyDepCount { get; set; }

        public int MonthlyDepCount { get; set; }

        public int WeeklyDepCount { get; set; }

        public int DailyDepCount { get; set; }
    }
}
