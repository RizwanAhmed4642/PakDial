using PAKDial.Common;
using PAKDial.Domains.Common;
using PAKDial.Domains.ViewModels;
using PAKDial.StoreProcdures.DatabaseConnections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PAKDial.StoreProcdures
{
    public static class AdminStoreProcdures
    {
        private static readonly string connectionString = DBConnections.connectionString;

        public static VMSubAdminOrdersUpdate SubAdminOrdersUpdate(decimal InvoiceId)
        {
            VMSubAdminOrdersUpdate response = new VMSubAdminOrdersUpdate();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.SpGetOrdersUpdateForSubAdmin", conn)
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
                        else if(CommonSpacing.RemoveSpacestoTrim(response.ModeName) == PaymentsMode.OnlinePayment.ToString().ToLower())
                        {
                            response.BankName = rdr.GetString(1);
                            response.ChequeNo = rdr.GetString(2);
                        }
                    }
                }
                conn.Close();
            }

            return response;
        }
    }
}
