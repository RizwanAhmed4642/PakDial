using PAKDial.Domains.StoreProcedureModel;
using PAKDial.StoreProcdures.DatabaseConnections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PAKDial.StoreProcdures
{
    public static class DashboardStoreProcedure 
    {
        #region prop
        private static readonly string connectionString = DBConnections.connectionString;
        #endregion


        #region Method
        /// <summary>
        /// Teller Store Procedure For Dashboard
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>SpGetTellerDepositedOrderUId</returns>
        public static SpGetTellerDepositedOrderUId SpGetTellerDepositedOrderUId(string UserId)
        {

            SpGetTellerDepositedOrderUId Reponse = new SpGetTellerDepositedOrderUId();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.SpGetTellerDepositedOrderUId", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                cmd.Parameters.Add(new SqlParameter("@CurrentDate", DateTime.Now));

                using (var lObjRdr = cmd.ExecuteReader())
                {
                    while (lObjRdr.Read())
                    {
                        Reponse.TotalDepCount = lObjRdr.GetInt32(0);
                        Reponse.YearlyDepCount = lObjRdr.GetInt32(1);
                        Reponse.MonthlyDepCount = lObjRdr.GetInt32(2);
                        Reponse.WeeklyDepCount = lObjRdr.GetInt32(3);
                        Reponse.DailyDepCount = lObjRdr.GetInt32(4);
                    }
                }
                conn.Close();
            }
            return Reponse;
        }

        /// <summary>
        /// CallCenter,SaleManager and Executive DashBoard Store Procedure
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>SpGetESCListingOrderUserId</returns>
        public static SpGetESCListingOrderUserId SpGetESCListingOrderUserId(string UserId)
        {

            SpGetESCListingOrderUserId Reponse = new SpGetESCListingOrderUserId();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.SpGetESCListingOrderUserId", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                cmd.Parameters.Add(new SqlParameter("@CurrentDate", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@StartDate",new DateTime(DateTime.Now.Year, 1, 1)));
                cmd.Parameters.Add(new SqlParameter("@EndDate",new DateTime(DateTime.Now.Year, 12, 31)));

                using (var lObjRdr = cmd.ExecuteReader())
                {
                    while (lObjRdr.Read())
                    {
                        Reponse.ListingCount.TotalCount = lObjRdr.GetInt32(0);
                        Reponse.ListingCount.YearlyCount = lObjRdr.GetInt32(1);
                        Reponse.ListingCount.MonthlyCount = lObjRdr.GetInt32(2);
                        Reponse.ListingCount.WeeklyCount = lObjRdr.GetInt32(3);
                        Reponse.ListingCount.DailyCount = lObjRdr.GetInt32(4);
                    }
                    lObjRdr.NextResult();
                    while (lObjRdr.Read())
                    {
                        Reponse.ESCOrder.TotalStatusCount = lObjRdr.GetInt32(0);
                        Reponse.ESCOrder.PendingCount = lObjRdr.GetInt32(1);
                        Reponse.ESCOrder.ProcessCount = lObjRdr.GetInt32(2);
                        Reponse.ESCOrder.ApprovedCount = lObjRdr.GetInt32(3);
                        Reponse.ESCOrder.RejectedCount = lObjRdr.GetInt32(4);
                        Reponse.ESCOrder.ExpiredCount = lObjRdr.GetInt32(5);
                    }
                    lObjRdr.NextResult();
                    while (lObjRdr.Read())
                    {
                        Reponse.ESCDepOrder.TotalDepCount = lObjRdr.GetInt32(0);
                        Reponse.ESCDepOrder.YearlyDepCount = lObjRdr.GetInt32(1);
                        Reponse.ESCDepOrder.MonthlyDepCount = lObjRdr.GetInt32(2);
                        Reponse.ESCDepOrder.WeeklyDepCount = lObjRdr.GetInt32(3);
                        Reponse.ESCDepOrder.DailyDepCount = lObjRdr.GetInt32(4);
                    }
                    //lObjRdr.NextResult();
                    //while (lObjRdr.Read())
                    //{
                    //    SpGetESCOrderCountMonthWiseFullYear res = new SpGetESCOrderCountMonthWiseFullYear
                    //    {
                    //        Month = lObjRdr.GetString(0),
                    //        OrdersCount = lObjRdr.GetInt32(1)
                    //    };
                    //    Reponse.ESCYearlyOrder.Add(res);
                    //}
                    //lObjRdr.NextResult();
                    //while (lObjRdr.Read())
                    //{
                    //    SpGetESCListingCountMonthWiseFullYear res = new SpGetESCListingCountMonthWiseFullYear
                    //    {
                    //        Month = lObjRdr.GetString(0),
                    //        ListingCount = lObjRdr.GetInt32(1)
                    //    };
                    //    Reponse.ESCYearlyListing.Add(res);
                    //}
                }
                conn.Close();
            }
            return Reponse;
        }

        /// <summary>
        /// Zone Manager DashBoard Store Procedure
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static SpGetZoneMgrByAreaWrapper SpGetZoneMgrByAreaWrapper(string UserId)
        {

            SpGetZoneMgrByAreaWrapper Reponse = new SpGetZoneMgrByAreaWrapper();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.SpGetZoneMgrByAreaWrapper", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                cmd.Parameters.Add(new SqlParameter("@CurrentDate", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@StartDate", new DateTime(DateTime.Now.Year, 1, 1)));
                cmd.Parameters.Add(new SqlParameter("@EndDate", new DateTime(DateTime.Now.Year, 12, 31)));

                using (var lObjRdr = cmd.ExecuteReader())
                {
                    while (lObjRdr.Read())
                    {
                        Reponse.ListingCount.TotalCount = lObjRdr.GetInt32(0);
                        Reponse.ListingCount.YearlyCount = lObjRdr.GetInt32(1);
                        Reponse.ListingCount.MonthlyCount = lObjRdr.GetInt32(2);
                        Reponse.ListingCount.WeeklyCount = lObjRdr.GetInt32(3);
                        Reponse.ListingCount.DailyCount = lObjRdr.GetInt32(4);
                    }
                    lObjRdr.NextResult();
                    while (lObjRdr.Read())
                    {
                        Reponse.DepositedOrderCount.TotalCount = lObjRdr.GetInt32(0);
                        Reponse.DepositedOrderCount.YearlyCount = lObjRdr.GetInt32(1);
                        Reponse.DepositedOrderCount.MonthlyCount = lObjRdr.GetInt32(2);
                        Reponse.DepositedOrderCount.WeeklyCount = lObjRdr.GetInt32(3);
                        Reponse.DepositedOrderCount.DailyCount = lObjRdr.GetInt32(4);
                    }
                    lObjRdr.NextResult();
                    while (lObjRdr.Read())
                    {
                        Reponse.ApprovedOrderCount.TotalCount = lObjRdr.GetInt32(0);
                        Reponse.ApprovedOrderCount.YearlyCount = lObjRdr.GetInt32(1);
                        Reponse.ApprovedOrderCount.MonthlyCount = lObjRdr.GetInt32(2);
                        Reponse.ApprovedOrderCount.WeeklyCount = lObjRdr.GetInt32(3);
                        Reponse.ApprovedOrderCount.DailyCount = lObjRdr.GetInt32(4);
                    }
                    lObjRdr.NextResult();
                    while (lObjRdr.Read())
                    {
                        SpGetGenericOrderCountMonthWise resp = new SpGetGenericOrderCountMonthWise
                        {
                            MonthNo = lObjRdr.GetInt32(0),
                            Month = lObjRdr.GetString(1),
                            OrdersCount = lObjRdr.GetInt32(2)
                        };
                        Reponse.ApprovedOrderMonthWise.Add(resp);
                    }

                }
                conn.Close();
            }
            return Reponse;
        }

        /// <summary>
        /// Regional Manager DashBoard Store Procedure
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static SpGetRegionalMgrByAreaWrapper SpGetRegionalMgrByAreaWrapper(string UserId)
        {

            SpGetRegionalMgrByAreaWrapper Reponse = new SpGetRegionalMgrByAreaWrapper();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.SpGetRegionalMgrByAreaWrapper", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                cmd.Parameters.Add(new SqlParameter("@CurrentDate", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@StartDate", new DateTime(DateTime.Now.Year, 1, 1)));
                cmd.Parameters.Add(new SqlParameter("@EndDate", new DateTime(DateTime.Now.Year, 12, 31)));

                using (var lObjRdr = cmd.ExecuteReader())
                {
                    while (lObjRdr.Read())
                    {
                        Reponse.ListingCount.TotalCount = lObjRdr.GetInt32(0);
                        Reponse.ListingCount.YearlyCount = lObjRdr.GetInt32(1);
                        Reponse.ListingCount.MonthlyCount = lObjRdr.GetInt32(2);
                        Reponse.ListingCount.WeeklyCount = lObjRdr.GetInt32(3);
                        Reponse.ListingCount.DailyCount = lObjRdr.GetInt32(4);
                    }
                    lObjRdr.NextResult();
                    while (lObjRdr.Read())
                    {
                        Reponse.DepositedOrderCount.TotalCount = lObjRdr.GetInt32(0);
                        Reponse.DepositedOrderCount.YearlyCount = lObjRdr.GetInt32(1);
                        Reponse.DepositedOrderCount.MonthlyCount = lObjRdr.GetInt32(2);
                        Reponse.DepositedOrderCount.WeeklyCount = lObjRdr.GetInt32(3);
                        Reponse.DepositedOrderCount.DailyCount = lObjRdr.GetInt32(4);
                    }
                    lObjRdr.NextResult();
                    while (lObjRdr.Read())
                    {
                        Reponse.ApprovedOrderCount.TotalCount = lObjRdr.GetInt32(0);
                        Reponse.ApprovedOrderCount.YearlyCount = lObjRdr.GetInt32(1);
                        Reponse.ApprovedOrderCount.MonthlyCount = lObjRdr.GetInt32(2);
                        Reponse.ApprovedOrderCount.WeeklyCount = lObjRdr.GetInt32(3);
                        Reponse.ApprovedOrderCount.DailyCount = lObjRdr.GetInt32(4);
                    }
                    lObjRdr.NextResult();
                    while (lObjRdr.Read())
                    {
                        SpGetGenericOrderCountMonthWise resp = new SpGetGenericOrderCountMonthWise
                        {
                            MonthNo = lObjRdr.GetInt32(0),
                            Month = lObjRdr.GetString(1),
                            OrdersCount = lObjRdr.GetInt32(2)
                        };
                        Reponse.ApprovedOrderMonthWise.Add(resp);
                    }
                    lObjRdr.NextResult();
                    while (lObjRdr.Read())
                    {
                        SpGetGenericOrderCounYearWise resps = new SpGetGenericOrderCounYearWise
                        {
                            YearNo = lObjRdr.GetInt32(0),
                            OrdersCount = lObjRdr.GetInt32(1)
                        };
                        Reponse.ApprovedOrderYearWise.Add(resps);
                    }

                }
                conn.Close();
            }
            return Reponse;
        }

        /// <summary>
        /// CEO and CRO DashBoard Store Procedure
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static SpGetCEROWrapperProc SpGetCEROWrapperProc()
        {

            SpGetCEROWrapperProc Reponse = new SpGetCEROWrapperProc();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.SpGetCEROWrapperProc", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@CurrentDate", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@StartDate", new DateTime(DateTime.Now.Year, 1, 1)));
                cmd.Parameters.Add(new SqlParameter("@EndDate", new DateTime(DateTime.Now.Year, 12, 31)));

                using (var lObjRdr = cmd.ExecuteReader())
                {
                    while (lObjRdr.Read())
                    {
                        Reponse.ListingCount.TotalCount = lObjRdr.GetInt32(0);
                        Reponse.ListingCount.YearlyCount = lObjRdr.GetInt32(1);
                        Reponse.ListingCount.MonthlyCount = lObjRdr.GetInt32(2);
                        Reponse.ListingCount.WeeklyCount = lObjRdr.GetInt32(3);
                        Reponse.ListingCount.DailyCount = lObjRdr.GetInt32(4);
                    }
                    lObjRdr.NextResult();
                    while (lObjRdr.Read())
                    {
                        Reponse.ApprovedOrderCount.TotalCount = lObjRdr.GetInt32(0);
                        Reponse.ApprovedOrderCount.YearlyCount = lObjRdr.GetInt32(1);
                        Reponse.ApprovedOrderCount.MonthlyCount = lObjRdr.GetInt32(2);
                        Reponse.ApprovedOrderCount.WeeklyCount = lObjRdr.GetInt32(3);
                        Reponse.ApprovedOrderCount.DailyCount = lObjRdr.GetInt32(4);
                    }
                    lObjRdr.NextResult();
                    while (lObjRdr.Read())
                    {
                        Reponse.ApprovedOrderRevenueCount.TotalRevenue = lObjRdr.GetDecimal(0);
                        Reponse.ApprovedOrderRevenueCount.YearlyRevenue = lObjRdr.GetDecimal(1);
                        Reponse.ApprovedOrderRevenueCount.MonthlyRevenue= lObjRdr.GetDecimal(2);
                        Reponse.ApprovedOrderRevenueCount.WeeklyRevenue = lObjRdr.GetDecimal(3);
                        Reponse.ApprovedOrderRevenueCount.DailyRevenue = lObjRdr.GetDecimal(4);
                    }

                    lObjRdr.NextResult();
                    while (lObjRdr.Read())
                    {
                        SpGetGenericOrderRevenueMonthWise resp = new SpGetGenericOrderRevenueMonthWise
                        {
                            MonthNo = lObjRdr.GetInt32(0),
                            Month = lObjRdr.GetString(1),
                            OrderRevenue = lObjRdr.GetDecimal(2)
                        };
                        Reponse.OrderMonthWiseRevnue.Add(resp);
                    }
                    lObjRdr.NextResult();
                    while (lObjRdr.Read())
                    {
                        SpGetGenericOrderRevenueYearWise resps = new SpGetGenericOrderRevenueYearWise
                        {
                            YearNo = lObjRdr.GetInt32(0),
                            OrderRevenue = lObjRdr.GetDecimal(1)
                        };
                        Reponse.OrderYearWiseRevnue.Add(resps);
                    }
                    lObjRdr.NextResult();
                    while (lObjRdr.Read())
                    {
                        SpGetGenericStateWiseRevnue state = new SpGetGenericStateWiseRevnue
                        {
                            StateName = lObjRdr.GetString(0),
                            OrderRevenue = lObjRdr.GetDecimal(1)
                        };
                        Reponse.StateWiseRevenue.Add(state);
                    }
                }
                conn.Close();
            }
            return Reponse;
        }


        /// <summary>
        /// Admin DashBoard Store Procedure
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static SpGetAdminWrapperProc SpGetAdminWrapperProc()
        {
            SpGetAdminWrapperProc Reponse = new SpGetAdminWrapperProc();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.SpGetAdminWrapperProc", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@CurrentDate", DateTime.Now));

                using (var lObjRdr = cmd.ExecuteReader())
                {
                    while (lObjRdr.Read())
                    {
                        Reponse.SpGetAdminResultantCount.TotalEmployeeCount = lObjRdr.GetInt32(0);
                        Reponse.SpGetAdminResultantCount.TotalCustomerCount = lObjRdr.GetInt32(1);
                        Reponse.SpGetAdminResultantCount.TotalListingCount = lObjRdr.GetInt32(2);
                        Reponse.SpGetAdminResultantCount.TotalOrdersCount = lObjRdr.GetInt32(3);
                        Reponse.SpGetAdminResultantCount.TotalAppCount = lObjRdr.GetInt32(4);
                        Reponse.SpGetAdminResultantCount.TotalRevenue = lObjRdr.GetDecimal(5);
                    }
                    lObjRdr.NextResult();
                    while (lObjRdr.Read())
                    {
                        SpGetAdminYearlyOrders yearlyOrders = new SpGetAdminYearlyOrders
                        {
                            YearNo = lObjRdr.GetInt32(0),
                            OrdersCount = lObjRdr.GetInt32(1)
                        };
                        Reponse.OrderYearWise.Add(yearlyOrders);
                    }
                    lObjRdr.NextResult();
                    while (lObjRdr.Read())
                    {
                        SpGetAdminYearlyOrdersRevenue OrdersRevnue = new SpGetAdminYearlyOrdersRevenue
                        {
                            YearNo = lObjRdr.GetInt32(0),
                            OrderRevenue = lObjRdr.GetDecimal(1)
                        };
                        Reponse.OrderYearWiseRevenue.Add(OrdersRevnue);
                    }
                }
                conn.Close();
            }
            return Reponse;
        }
        #endregion

        #region General
        /// <summary>
        /// Get Customer,Employee, and Listing  Total Record...
        /// </summary>
        /// <returns></returns>
        public static SpGetGeneralResultantProc SpGetGeneralResultantProc()
        {
            SpGetGeneralResultantProc Reponse = new SpGetGeneralResultantProc();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.SpGetGeneralResultantCounts", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
               

                using (var lObjRdr = cmd.ExecuteReader())
                {
                    while (lObjRdr.Read())
                    {

                        Reponse.TotalEmployeeCount = lObjRdr.GetInt32(0);
                        Reponse.TotalCustomerCount = lObjRdr.GetInt32(1);
                        Reponse.TotalListingCount = lObjRdr.GetInt32(2);
                                                                     
                    }
                    lObjRdr.NextResult();
                   
                }
                conn.Close();
            }
            return Reponse;
        }

        
        #endregion
    }
}
