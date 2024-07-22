namespace PAKDial.Domains.StoreProcedureModel
{
    public class SpGetGenericStateWiseRevnue
    {
        public SpGetGenericStateWiseRevnue()
        {
            OrderRevenue = 0;
        }
        public string StateName { get; set; }
        public decimal OrderRevenue { get; set; }
    }
}
