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
    public static class ZoneManagerStoreProcedures
    {
        private static readonly string connectionString = DBConnections.connectionString;

        public static List<decimal> SpGetCityAreasByZoneManagerUserId(string UserId)
        {
            List<decimal> result = new List<decimal>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.SpGetCityAreasByZoneManagerUserId", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        if (rdr.GetDecimal(0) > 0)
                        {
                            result.Add(rdr.GetDecimal(0));
                        }
                    }
                }
                conn.Close();
            }
            return result;
        }

        public static VMZoneManagerTransfer SpGetOpenForManagerTransfer(decimal InvoiceId)
        {
            VMZoneManagerTransfer response = new VMZoneManagerTransfer();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.SpGetOpenForManagerTransfer", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@InvoiceId", InvoiceId));
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        response.Id = rdr.GetDecimal(0);
                        response.PackageName = rdr.GetString(4);
                        response.ModeName = rdr.GetString(5);
                        response.CombineAreas = rdr.GetString(6);
                        response.CompanyName = rdr.GetString(7);

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

        public static List<SpGetEmployeeByZoneManager> SpGetEmployeeByZoneManagerId(decimal ManagerId)
        {
            List<SpGetEmployeeByZoneManager> response = new List<SpGetEmployeeByZoneManager>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.SpGetEmployeeByZoneManager", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@ManagerId", ManagerId));
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        SpGetEmployeeByZoneManager res = new SpGetEmployeeByZoneManager
                        {
                            Id = rdr.GetString(0),
                            Email = rdr.GetString(1),
                        };
                        response.Add(res);
                    }
                }
                conn.Close();
            }
            return response;
        }
    }
}
