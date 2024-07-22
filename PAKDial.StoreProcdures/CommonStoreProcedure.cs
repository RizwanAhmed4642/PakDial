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
    public static class CommonStoreProcedure
    {
        private static readonly string connectionString = DBConnections.connectionString;

        public static VMRoleIdDesignationId GetRoleandDesignationByUserId(string UserId)
        {
            VMRoleIdDesignationId response = new VMRoleIdDesignationId();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.GetRoleandDesignationByUserId", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        response.RoleId = rdr.GetString(0);
                        response.DesignationId = rdr.GetDecimal(1);
                    }
                }
                conn.Close();
            }
            return response;
        }
    }
}
