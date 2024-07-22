using PAKDial.Domains.SqlViewModels;
using PAKDial.Domains.StoreProcedureModel;
using PAKDial.StoreProcdures.DatabaseConnections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

namespace PAKDial.StoreProcdures.ListingsProc
{
    public static class ListingSearchProcdure
    {
        private static readonly string connectionString = DBConnections.connectionString;

        public static List<decimal> GetSearchListingByRecursion(decimal SbCId,string SbCName)
        {
            List<decimal> SbCatId = new List<decimal>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.GetSearchListingByRecursion", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@SubCatId", SbCId));
                cmd.Parameters.Add(new SqlParameter("@SubCatName", SbCName));
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        if (rdr.GetDecimal(0) > 0)
                        {
                            SbCatId.Add(rdr.GetDecimal(0));
                        }
                    }
                }
                conn.Close();
            }
            return SbCatId;
        }

        public static GetCompanyListingPaging GetCompanyListingPaging(decimal SubCatId, string SubCatName,string CtName,string ArName,string SortColumnName
            ,string Ratingstatus,int offset,int PageSize)
        {
            GetCompanyListingPaging entitylist = new GetCompanyListingPaging();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.GetCompanyListingPaging", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@SubCatId", SubCatId));
                cmd.Parameters.Add(new SqlParameter("@SubCatName", SubCatName));
                cmd.Parameters.Add(new SqlParameter("@CtName", CtName));
                cmd.Parameters.Add(new SqlParameter("@ArName", ArName ?? SqlString.Null));
                cmd.Parameters.Add(new SqlParameter("@SortColumnName", SortColumnName));
                cmd.Parameters.Add(new SqlParameter("@Ratingstatus", Ratingstatus ?? "Asc"));
                cmd.Parameters.Add(new SqlParameter("@offset", offset));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
                conn.Close();
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    VHomeListingSearch entity1 = new VHomeListingSearch()
                    {
                        ListingId = row["ListingId"] != DBNull.Value ? Convert.ToDecimal(row["ListingId"].ToString()) : 0,
                        CatId = row["CatId"] != DBNull.Value ? Convert.ToDecimal(row["CatId"].ToString()) : 0,
                        CompanyName = row["CompanyName"] != DBNull.Value ? row["CompanyName"].ToString() : null,
                        BannerImage = row["BannerImage"] != DBNull.Value ? row["BannerImage"].ToString() : null,
                        ContactNo = row["ContactNo"] != DBNull.Value ? row["ContactNo"].ToString() : null,
                        ListingAddress = row["ListingAddress"] != DBNull.Value ? row["ListingAddress"].ToString() : null,
                        CityArea = row["CityArea"] != DBNull.Value ? row["CityArea"].ToString() : null,
                        CityName = row["CityName"] != DBNull.Value ? row["CityName"].ToString() : null,
                        SpaceCityArea = row["SpaceCityArea"] != DBNull.Value ? row["SpaceCityArea"].ToString() : null,
                        SpaceCityName = row["SpaceCityName"] != DBNull.Value ? row["SpaceCityName"].ToString() : null,
                        AvgRating = row["AvgRating"] != DBNull.Value ? Convert.ToInt32(row["AvgRating"].ToString()) : 0,
                        TotalVotes = row["TotalVotes"] != DBNull.Value ? Convert.ToInt32(row["TotalVotes"].ToString()) : 0,
                        IsPremium = row["IsPremium"] != DBNull.Value ? Convert.ToBoolean(row["IsPremium"].ToString()) : false,
                        IsTrusted = row["IsTrusted"] != DBNull.Value ? Convert.ToInt32(row["IsTrusted"].ToString()) : 0,
                    };
                    entitylist.HomeListingSearch.Add(entity1);
                }
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                entitylist.RowCount = ds.Tables[1].Rows[0]["RowsCounts"] != DBNull.Value ? Convert.ToInt32(ds.Tables[1].Rows[0]["RowsCounts"].ToString()) : 0;
            }
            return entitylist;
        }
    }
}
