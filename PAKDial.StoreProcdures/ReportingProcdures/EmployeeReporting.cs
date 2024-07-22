using PAKDial.Domains.StoreProcedureModel;
using PAKDial.StoreProcdures.DatabaseConnections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PAKDial.StoreProcdures.ReportingProcdures
{
    public static class EmployeeReporting
    {
        private static readonly string connectionString = DBConnections.connectionString;

        public static List<EmployeeCountStateReport> EmployeeReportings(string UserId)
        {
            List<EmployeeCountStateReport> response = new List<EmployeeCountStateReport>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.Sp_WrapperCountReportState", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                cmd.Parameters.Add(new SqlParameter("@CurrentDate", DateTime.Now));

                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        EmployeeCountStateReport sp = new EmployeeCountStateReport
                        {
                            TotalCount = rdr.GetInt32(0),
                            YearlyCount = rdr.GetInt32(1),
                            MonthyCount = rdr.GetInt32(2),
                            WeeklyCount = rdr.GetInt32(3),
                            DailyCount = rdr.GetInt32(4),
                            TableName = rdr.GetString(5)
                        };
                        response.Add(sp);
                        sp = null;
                    }
                    rdr.NextResult();
                    while (rdr.Read())
                    {
                        EmployeeCountStateReport sp = new EmployeeCountStateReport
                        {
                            TotalCount = rdr.GetInt32(0),
                            YearlyCount = rdr.GetInt32(1),
                            MonthyCount = rdr.GetInt32(2),
                            WeeklyCount = rdr.GetInt32(3),
                            DailyCount = rdr.GetInt32(4),
                            TableName = rdr.GetString(5)
                        };
                        response.Add(sp);
                        sp = null;
                    }
                    rdr.NextResult();
                    while (rdr.Read())
                    {
                        EmployeeCountStateReport sp = new EmployeeCountStateReport
                        {
                            TotalCount = rdr.GetInt32(0),
                            YearlyCount = rdr.GetInt32(1),
                            MonthyCount = rdr.GetInt32(2),
                            WeeklyCount = rdr.GetInt32(3),
                            DailyCount = rdr.GetInt32(4),
                            TableName = rdr.GetString(5)
                        };
                        response.Add(sp);
                        sp = null;
                    }
                    rdr.NextResult();
                    while (rdr.Read())
                    {
                        EmployeeCountStateReport sp = new EmployeeCountStateReport
                        {
                            TotalCount = rdr.GetInt32(0),
                            YearlyCount = rdr.GetInt32(1),
                            MonthyCount = rdr.GetInt32(2),
                            WeeklyCount = rdr.GetInt32(3),
                            DailyCount = rdr.GetInt32(4),
                            TableName = rdr.GetString(5)
                        };
                        response.Add(sp);
                        sp = null;
                    }

                }
                conn.Close();
            }

            return response;
        }
    }
}
