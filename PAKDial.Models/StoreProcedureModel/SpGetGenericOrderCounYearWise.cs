namespace PAKDial.Domains.StoreProcedureModel
{
    public class SpGetGenericOrderCounYearWise
    {
        public SpGetGenericOrderCounYearWise()
        {
            YearNo = 0;
            OrdersCount = 0;
        }
        public int YearNo { get; set; }
        public int OrdersCount { get; set; }
    }
}
