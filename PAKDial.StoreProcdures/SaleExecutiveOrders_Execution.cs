using PAKDial.Common;
using PAKDial.Domains.Common;
using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Domains.ViewModels;
using PAKDial.StoreProcdures.DatabaseConnections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PAKDial.StoreProcdures
{
    public static class SaleExecutiveOrders_Execution
    {
        private static readonly string connectionString = DBConnections.connectionString;

        public static List<decimal> SpGetCityAreasByUserId(string UserId)
        {
            List<decimal> result = new List<decimal>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.SpGetCityAreasByUserId", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        if(rdr.GetDecimal(0) > 0)
                        {
                            result.Add(rdr.GetDecimal(0));
                        }
                    }
                }
                conn.Close();
            }
            return result;
        }

        //Not Include Expired,Rejected
        public static decimal GetCountActiveListingByListingId(decimal ListingId)
        {
            decimal ActivePackageCount = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.GetCountActiveListingByListingId", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@ListingId", ListingId));
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        ActivePackageCount = rdr.GetDecimal(0);
                    }
                }
                conn.Close();
            }
            return ActivePackageCount;
        }

        //Not Include Expired,Rejected,Pending
        public static decimal GetActiveOrderByListingId(decimal ListingId)
        {
            decimal ActivePackageCount = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.GetActiveOrderByListingId", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@ListingId", ListingId));
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        ActivePackageCount = rdr.GetDecimal(0);
                    }
                }
                conn.Close();
            }
            return ActivePackageCount;
        }

        public static List<SpGetSeOrderTracking> SpGetSeOrderTrackings(decimal PremiumId)
        {
            List<SpGetSeOrderTracking> response = new List<SpGetSeOrderTracking>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.SpGetSeOrderTracking", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@PremiumId", PremiumId));
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        SpGetSeOrderTracking spGetSe = new SpGetSeOrderTracking
                        {
                            Id = rdr.GetDecimal(0),
                            RoleName = rdr.GetString(1),
                            DesignationName = rdr.GetString(2),
                            EmpName = rdr.GetString(3),
                            AmountDeposited = rdr.GetString(6),
                            StatusName = rdr.GetString(7)
                        };
                        if (!rdr.IsDBNull(4))
                        {
                            spGetSe.CreatedDate = rdr.GetDateTime(4);
                        }
                        else if (!rdr.IsDBNull(5))
                        {
                            spGetSe.UpdatedDate = rdr.GetDateTime(5);
                        }
                        response.Add(spGetSe);
                    }
                }
                conn.Close();
            }
           
            return response;
        }
        public static SpGetEmailAndPhoneByOrderId SpGetEmailAndPhoneByOrderIds(decimal PremiumId)
        {
            SpGetEmailAndPhoneByOrderId response = new SpGetEmailAndPhoneByOrderId();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.SpGetEmailAndPhoneByOrderId", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@PremiumId", PremiumId));
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        if(!string.IsNullOrEmpty(rdr.GetString(0)))
                        {
                            response.Email = rdr.GetString(0);
                            response.ContactNo = rdr.GetString(1);
                        }
                    }
                }
                conn.Close();
            }

            return response;
        }

        public static List<SpGetSeOrderDetailByOrderId> SpGetSeOrderDetailByOrderIds(decimal PremiumId)
        {
            List<SpGetSeOrderDetailByOrderId> response = new List<SpGetSeOrderDetailByOrderId>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.SpGetSeOrderDetailByOrderId", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@PremiumId", PremiumId));
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        if (rdr.GetDecimal(0) > 0)
                        {
                            response.Add(new SpGetSeOrderDetailByOrderId
                            {
                                ListingId = rdr.GetDecimal(0),
                                CompanyDetail = rdr.GetString(1),
                                Months = rdr.GetInt32(2),
                                Price = rdr.GetDecimal(3),
                                 Discount =rdr.GetString(4),
                                 isDiscount =rdr.GetBoolean(5),
                            });
                        }
                    }
                }
                conn.Close();
            }

            return response;
        }

        public static VMSaleExCollect SpGetCollectPaymentByInvoiceId(decimal InvoiceId)
        {
            VMSaleExCollect response = new VMSaleExCollect();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.SpGetCollectPaymentByInvoiceId", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@InvoiceId", InvoiceId));
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        response.Id = rdr.GetDecimal(0);
                        response.PackageName = rdr.GetString(3);
                        response.ModeId = rdr.GetDecimal(4);
                        response.CombineAreas = rdr.GetString(5);
                        response.CompanyName = rdr.GetString(6);
                        response.ModeName = rdr.GetString(7);
                        if (CommonSpacing.RemoveSpacestoTrim(response.ModeName) == PaymentsMode.Cheque.ToString().ToLower()
                            || CommonSpacing.RemoveSpacestoTrim(response.ModeName) == PaymentsMode.PayOrder.ToString().ToLower())
                        {
                            response.BankName = rdr.GetString(1);
                            response.ChequeNo = rdr.GetString(3);
                        }
                    }
                }
                conn.Close();
            }

            return response;
        }
    }
}
