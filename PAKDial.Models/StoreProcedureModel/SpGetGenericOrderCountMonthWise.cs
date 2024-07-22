namespace PAKDial.Domains.StoreProcedureModel
{
    public class SpGetGenericOrderCountMonthWise
    {
        public SpGetGenericOrderCountMonthWise()
        {
            MonthNo = 0;
            OrdersCount = 0;
        }
        public int MonthNo { get; set; }
        public string Month { get; set; }
        public int OrdersCount { get; set; }
    }
}
