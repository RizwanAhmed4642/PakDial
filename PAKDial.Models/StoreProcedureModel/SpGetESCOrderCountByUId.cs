namespace PAKDial.Domains.StoreProcedureModel
{
    public class SpGetESCOrderCountByUId
    {
        public SpGetESCOrderCountByUId()
        {
            TotalStatusCount = 0;
            PendingCount = 0;
            ProcessCount = 0;
            ApprovedCount = 0;
            RejectedCount = 0;
            ExpiredCount = 0;
        }
        public int TotalStatusCount { get; set; }
        public int PendingCount { get; set; }
        public int ProcessCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
        public int ExpiredCount { get; set; }
    }
}
