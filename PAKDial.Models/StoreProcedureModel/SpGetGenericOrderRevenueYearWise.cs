namespace PAKDial.Domains.StoreProcedureModel
{
    public class SpGetGenericOrderRevenueYearWise
    {
        public SpGetGenericOrderRevenueYearWise()
        {
            YearNo = 0;
            OrderRevenue = 0;
        }
        public int YearNo { get; set; }
        public decimal OrderRevenue { get; set; }
    }
}
