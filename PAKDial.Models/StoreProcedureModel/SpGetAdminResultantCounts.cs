namespace PAKDial.Domains.StoreProcedureModel
{
    public class SpGetAdminResultantCounts
    {
        public SpGetAdminResultantCounts()
        {
            TotalEmployeeCount = 0;
            TotalCustomerCount = 0;
            TotalListingCount = 0;
            TotalOrdersCount = 0;
            TotalAppCount = 0;
            TotalRevenue = 0;
        }
        public int TotalEmployeeCount { get; set; }

        public int TotalCustomerCount { get; set; }

        public int TotalListingCount { get; set; }

        public int TotalOrdersCount { get; set; }

        public int TotalAppCount { get; set; }

        public decimal TotalRevenue { get; set; }
    }
}
