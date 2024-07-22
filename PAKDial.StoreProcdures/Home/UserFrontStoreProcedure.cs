using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Domains.StoreProcedureModel.Home;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.StoreProcdures.DatabaseConnections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

namespace PAKDial.StoreProcdures.Home
{
  public static class UserFrontStoreProcedure
    {
        #region Connstring
        private static readonly string connectionString = DBConnections.connectionString;
        #endregion

        #region Method
        public static SPGetCompnayDetailById SPGetCompnayDetailById(decimal ListingId)
        {
            SPGetCompnayDetailById Spresponse = new SPGetCompnayDetailById();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.SP_GetCompnayDetailById", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@ListingId", ListingId));
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        Spresponse.ListingId = rdr.GetDecimal(0);
                        Spresponse.CompanyName = !rdr.IsDBNull(1) ? rdr.GetString(1) : "";
                        Spresponse.Email = !rdr.IsDBNull(2) ? rdr.GetString(2) : "";
                        Spresponse.Website = !rdr.IsDBNull(3) ? rdr.GetString(3) : "";
                        Spresponse.BannerImage = !rdr.IsDBNull(4) ? rdr.GetString(4) : "";
                        Spresponse.MobileNo = !rdr.IsDBNull(5) ? rdr.GetString(5) : "";
                        Spresponse.ImageUrl = !rdr.IsDBNull(6) ? rdr.GetString(6) : "";
                        Spresponse.Address = !rdr.IsDBNull(7) ? rdr.GetString(7) : "";
                        Spresponse.AvgRating = rdr.GetInt32(8);
                        Spresponse.TotalVotes = rdr.GetInt32(9);
                        Spresponse.IsPremium = rdr.GetBoolean(10);
                        Spresponse.IsTrusted = rdr.GetInt32(11);
                    }
                }
                conn.Close();
            }
            return Spresponse;
        }

        public static string GetAddressByListingId(decimal ListingId)
        {
            var Address = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.Sp_GetCompanyListingAddress", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@ListingId", ListingId));
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        Address = rdr.GetString(0);
                    }
                }
                conn.Close();
            }
            return Address;
        }

        public static List<GetBulkQueryFormSubmittion> GetBulkQueryFormSubmittion(ListingQueryRequest request)
        {
            List<GetBulkQueryFormSubmittion> response = new List<GetBulkQueryFormSubmittion>();
            if(request.ListingId > 0)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("dbo.GetQueryFormSubmittion", conn)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    cmd.Parameters.Add(new SqlParameter("@RequestName", request.RequestName));
                    cmd.Parameters.Add(new SqlParameter("@RequestMobNo", request.RequestMobNo));
                    cmd.Parameters.Add(new SqlParameter("@ListingId", request.ListingId));
                    cmd.Parameters.Add(new SqlParameter("@ProductName", request.ProductName));
                    cmd.Parameters.Add(new SqlParameter("@SubCatId", request.SubCatId));
                    cmd.Parameters.Add(new SqlParameter("@SubCatName", request.SubCatName));
                    cmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now));

                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            GetBulkQueryFormSubmittion res = new GetBulkQueryFormSubmittion
                            {
                                ReceiverNo = rdr.GetString(0),
                                ReceiverSubject = rdr.GetString(1),
                                ReceiverMessage = rdr.GetString(2),
                                RequestNo = rdr.GetString(3),
                                RequestSubject = rdr.GetString(4),
                                RequestMessage = rdr.GetString(5),
                                CompanyName = rdr.GetString(6)
                            };
                            response.Add(res);
                        }
                    }
                    conn.Close();
                }
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("dbo.GetBulkQueryFormSubmittion", conn)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    cmd.Parameters.Add(new SqlParameter("@RequestName", request.RequestName));
                    cmd.Parameters.Add(new SqlParameter("@RequestMobNo", request.RequestMobNo));
                    cmd.Parameters.Add(new SqlParameter("@SubCatId", request.SubCatId));
                    cmd.Parameters.Add(new SqlParameter("@AreaName", request.AreaName ?? SqlString.Null));
                    cmd.Parameters.Add(new SqlParameter("@CityName", request.CityName ?? SqlString.Null));
                    cmd.Parameters.Add(new SqlParameter("@ProductName", request.ProductName));
                    cmd.Parameters.Add(new SqlParameter("@SubCatName", request.SubCatName));
                    cmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now));

                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            GetBulkQueryFormSubmittion res = new GetBulkQueryFormSubmittion
                            {
                                ReceiverNo = rdr.GetString(0),
                                ReceiverSubject = rdr.GetString(1),
                                ReceiverMessage = rdr.GetString(2),
                                RequestNo = rdr.GetString(3),
                                RequestSubject = rdr.GetString(4),
                                RequestMessage = rdr.GetString(5),
                                CompanyName = rdr.GetString(6)
                            };
                            response.Add(res);
                        }
                    }
                    conn.Close();
                }
            }
            return response;
        }


        public static Sp_GetClientSummaryResults GetClientSummaryResults(decimal CustomerId)
        {
            Sp_GetClientSummaryResults response = new Sp_GetClientSummaryResults();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.Sp_GetClientSummaryResults", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        response.TotalListings = !rdr.IsDBNull(0) ? rdr.GetInt32(0) : 0;
                        response.PremiumListings = !rdr.IsDBNull(1) ? rdr.GetInt32(1) : 0;
                        response.FreeListings = !rdr.IsDBNull(2) ? rdr.GetInt32(2) : 0;
                        response.TotalServed = !rdr.IsDBNull(3) ? rdr.GetDecimal(3) : 0;
                        response.TotalRatings = !rdr.IsDBNull(4) ? rdr.GetInt32(4) : 0;
                        response.TotalRequests = !rdr.IsDBNull(5) ? rdr.GetDecimal(5) : 0;
                    }
                }
                conn.Close();
            }
            return response;
        }

        public static List<SearchSbCategories> Sp_SubCategoryKeywordsSearch(string SubCatName)
        {
            List<SearchSbCategories> response = new List<SearchSbCategories>();
            if (!string.IsNullOrEmpty(SubCatName))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("dbo.Sp_SubCategoryKeywordsSearch", conn)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    cmd.Parameters.Add(new SqlParameter("@SubCatName", SubCatName));
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (rdr.GetDecimal(0) > 0)
                            {
                                SearchSbCategories res = new SearchSbCategories
                                {
                                    Id = rdr.GetDecimal(0),
                                    SbCatName = rdr.GetString(1),
                                };
                                response.Add(res);
                            }
                        }
                    }
                    conn.Close();
                }
            }
            return response;
        }


        #endregion

    }
}
