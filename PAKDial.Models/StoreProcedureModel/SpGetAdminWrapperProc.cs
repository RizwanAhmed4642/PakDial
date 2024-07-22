using System.Collections.Generic;

namespace PAKDial.Domains.StoreProcedureModel
{
    public class SpGetAdminWrapperProc
    {
        public SpGetAdminWrapperProc()
        {
            SpGetAdminResultantCount = new SpGetAdminResultantCounts();
            OrderYearWise = new List<SpGetAdminYearlyOrders>();
            OrderYearWiseRevenue = new List<SpGetAdminYearlyOrdersRevenue>();
        }
        public SpGetAdminResultantCounts SpGetAdminResultantCount { get; set; }
        public List<SpGetAdminYearlyOrders> OrderYearWise { get; set; }
        public List<SpGetAdminYearlyOrdersRevenue> OrderYearWiseRevenue { get; set; }
    }
}
