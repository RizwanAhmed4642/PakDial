using System.Collections.Generic;

namespace PAKDial.Domains.StoreProcedureModel
{
    public class SpGetRegionalMgrByAreaWrapper
    {
        public SpGetRegionalMgrByAreaWrapper()
        {
            ListingCount = new SpGetGenericCountYMWD();
            DepositedOrderCount = new SpGetGenericCountYMWD();
            ApprovedOrderCount = new SpGetGenericCountYMWD();
            ApprovedOrderMonthWise = new List<SpGetGenericOrderCountMonthWise>();
            ApprovedOrderYearWise = new List<SpGetGenericOrderCounYearWise>();
        }
        public SpGetGenericCountYMWD ListingCount { get; set; }
        public SpGetGenericCountYMWD DepositedOrderCount { get; set; }
        public SpGetGenericCountYMWD ApprovedOrderCount { get; set; }
        public List<SpGetGenericOrderCountMonthWise> ApprovedOrderMonthWise { get; set; }
        public List<SpGetGenericOrderCounYearWise> ApprovedOrderYearWise { get; set; }

    }
}
