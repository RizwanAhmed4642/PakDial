namespace PAKDial.Domains.StoreProcedureModel
{
    public class SpGetGenericOrderRevenueMonthWise
    {
        public SpGetGenericOrderRevenueMonthWise()
        {
            MonthNo = 0;
            OrderRevenue = 0;
        }
        public int MonthNo { get; set; }
        public string Month { get; set; }
        public decimal OrderRevenue { get; set; }
    }
}
