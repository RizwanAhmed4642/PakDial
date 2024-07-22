namespace PAKDial.Domains.StoreProcedureModel
{
    public class SpGetESCListingCountMonthWiseFullYear
    {
        public SpGetESCListingCountMonthWiseFullYear()
        {
            ListingCount = 0;
        }
        public string Month { get; set; }
        public int ListingCount { get; set; }
    }
}
