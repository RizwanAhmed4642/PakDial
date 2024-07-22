using System.Collections.Generic;

namespace PAKDial.Domains.StoreProcedureModel
{
    public class SpGetESCListingOrderUserId
    {
        public SpGetESCListingOrderUserId()
        {
            ListingCount = new SpGetESCCompanyListingCountByUId();
            ESCOrder = new SpGetESCOrderCountByUId();
            ESCDepOrder = new SpGetESCDepositedOrderUId();
            //ESCYearlyOrder = new List<SpGetESCOrderCountMonthWiseFullYear>();
            //ESCYearlyListing = new List<SpGetESCListingCountMonthWiseFullYear>();
        }
        public SpGetESCCompanyListingCountByUId ListingCount { get; set; }
        public SpGetESCOrderCountByUId ESCOrder { get; set; }
        public SpGetESCDepositedOrderUId ESCDepOrder { get; set; }
        //public List<SpGetESCOrderCountMonthWiseFullYear> ESCYearlyOrder { get; set; }
        //public List<SpGetESCListingCountMonthWiseFullYear> ESCYearlyListing { get; set; }

    }
}
