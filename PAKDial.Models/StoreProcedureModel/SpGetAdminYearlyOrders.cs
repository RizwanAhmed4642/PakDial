namespace PAKDial.Domains.StoreProcedureModel
{
    public class SpGetAdminYearlyOrders
    {
        public SpGetAdminYearlyOrders()
        {
            YearNo = 0;
            OrdersCount = 0;
        }
        public int YearNo { get; set; }
        public int OrdersCount { get; set; }
    }
}
