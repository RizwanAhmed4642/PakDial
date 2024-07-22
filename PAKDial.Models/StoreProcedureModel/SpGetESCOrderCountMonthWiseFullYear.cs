namespace PAKDial.Domains.StoreProcedureModel
{
    public class SpGetESCOrderCountMonthWiseFullYear
    {
        public SpGetESCOrderCountMonthWiseFullYear()
        {
            OrdersCount = 0;
        }
        public string Month { get; set; }
        public int OrdersCount { get; set; }
    }
}
