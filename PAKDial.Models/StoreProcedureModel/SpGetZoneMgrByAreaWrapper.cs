using System.Collections.Generic;

namespace PAKDial.Domains.StoreProcedureModel
{
    public class SpGetZoneMgrByAreaWrapper
    {
        public SpGetZoneMgrByAreaWrapper()
        {
            ListingCount = new SpGetGenericCountYMWD();
            DepositedOrderCount = new SpGetGenericCountYMWD();
            ApprovedOrderCount = new SpGetGenericCountYMWD();
            ApprovedOrderMonthWise = new List<SpGetGenericOrderCountMonthWise>();
        }
        public SpGetGenericCountYMWD ListingCount { get; set; }
        public SpGetGenericCountYMWD DepositedOrderCount { get; set; }
        public SpGetGenericCountYMWD ApprovedOrderCount { get; set; }
        public List<SpGetGenericOrderCountMonthWise> ApprovedOrderMonthWise { get; set; }

    }
}
