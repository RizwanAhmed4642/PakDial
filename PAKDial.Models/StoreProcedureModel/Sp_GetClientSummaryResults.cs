namespace PAKDial.Domains.StoreProcedureModel
{
    public class Sp_GetClientSummaryResults
    {
        public Sp_GetClientSummaryResults()
        {
            TotalListings = 0;
            PremiumListings = 0;
            FreeListings = 0;
            TotalServed = 0;
            TotalRatings = 0;
        }
        public int TotalListings { get; set; }
        public int PremiumListings { get; set; }
        public int FreeListings { get; set; }
        public decimal TotalServed { get; set; }
        public int TotalRatings { get; set; }
        public decimal TotalRequests { get; set; }

    }
}
