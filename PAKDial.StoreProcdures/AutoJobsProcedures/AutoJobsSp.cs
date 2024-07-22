using PAKDial.Domains.ViewModels;
using PAKDial.StoreProcdures.DatabaseConnections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PAKDial.StoreProcdures.AutoJobsProcedures
{
    public class AutoJobsSp
    {
        private static readonly string connectionString = DBConnections.connectionString;

        public static void AutoDeleteUnVerfiedRating()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.Sp_DeleteUnVerfiedRating", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static List<AutoUpdateExpiredOrder> AutoUpdateExpiredOrders()
        {
            List<AutoUpdateExpiredOrder> response = new List<AutoUpdateExpiredOrder>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.Sp_UpdateExpiredOrders", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@CurrentDate", DateTime.Now));
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        AutoUpdateExpiredOrder auto = new AutoUpdateExpiredOrder
                        {
                            CompanyName = !rdr.IsDBNull(0) ? rdr.GetString(0) : null,
                            MobileNo = !rdr.IsDBNull(1) ? rdr.GetString(1):null
                        };
                        if(auto.CompanyName != null)
                        {
                            response.Add(auto);
                        }
                    }
                }
                conn.Close();
            }
            return response;
        }
    }
}
