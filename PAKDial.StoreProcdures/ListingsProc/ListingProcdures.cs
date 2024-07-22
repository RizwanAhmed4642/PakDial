using PAKDial.Domains.ViewModels;
using PAKDial.StoreProcdures.DatabaseConnections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

namespace PAKDial.StoreProcdures.ListingsProc
{
    public static class ListingProcdures
    {
        private static readonly string connectionString = DBConnections.connectionString;

        public static VMCompanyListings Sp_Find2ListingListingById(decimal ListingId)
        {
            VMCompanyListings response = new VMCompanyListings();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.Sp_Find2ListingListingById", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@ListingId", ListingId));
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
                conn.Close();
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                response.CompanyListings.Id = ds.Tables[0].Rows[0]["Id"] != DBNull.Value ?  Convert.ToDecimal(ds.Tables[0].Rows[0]["Id"].ToString()) : 0;
                response.CompanyListings.BannerImage = ds.Tables[0].Rows[0]["BannerImage"] != DBNull.Value ? ds.Tables[0].Rows[0]["BannerImage"].ToString() : null;
                response.CompanyListings.BannerImageUrl = ds.Tables[0].Rows[0]["BannerImageUrl"] != DBNull.Value ? ds.Tables[0].Rows[0]["BannerImageUrl"].ToString() : null;
                response.CompanyListings.CompanyName = ds.Tables[0].Rows[0]["CompanyName"] != DBNull.Value ? ds.Tables[0].Rows[0]["CompanyName"].ToString() : null;
                response.CompanyListings.Email = ds.Tables[0].Rows[0]["Email"] != DBNull.Value ? ds.Tables[0].Rows[0]["Email"].ToString() : null;
                response.CompanyListings.FirstName = ds.Tables[0].Rows[0]["FirstName"] != DBNull.Value ? ds.Tables[0].Rows[0]["FirstName"].ToString() : null;
                response.CompanyListings.LastName = ds.Tables[0].Rows[0]["LastName"] != DBNull.Value ? ds.Tables[0].Rows[0]["LastName"].ToString() : null;
                response.CompanyListings.ListingStatus = ds.Tables[0].Rows[0]["ListingStatus"] != DBNull.Value ? Convert.ToBoolean(ds.Tables[0].Rows[0]["ListingStatus"].ToString()) : false;
                response.CompanyListings.ListingTypeId = ds.Tables[0].Rows[0]["ListingTypeId"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["ListingTypeId"].ToString()) : 0;
                response.CompanyListings.MetaDescription = ds.Tables[0].Rows[0]["MetaDescription"] != DBNull.Value ? ds.Tables[0].Rows[0]["MetaDescription"].ToString() : null;
                response.CompanyListings.MetaKeyword = ds.Tables[0].Rows[0]["MetaKeyword"] != DBNull.Value ? ds.Tables[0].Rows[0]["MetaKeyword"].ToString() : null;
                response.CompanyListings.MetaTitle = ds.Tables[0].Rows[0]["MetaTitle"] != DBNull.Value ? ds.Tables[0].Rows[0]["MetaTitle"].ToString() : null;
                response.CompanyListings.OtpCode = ds.Tables[0].Rows[0]["OtpCode"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["OtpCode"].ToString()) : 0;
                response.CompanyListings.UpdatedBy = ds.Tables[0].Rows[0]["UpdatedBy"] != DBNull.Value ? ds.Tables[0].Rows[0]["UpdatedBy"].ToString() : null;
                response.CompanyListings.UpdatedDate = ds.Tables[0].Rows[0]["UpdatedDate"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["UpdatedDate"].ToString()) : (DateTime?)null;
                response.CompanyListings.Website = ds.Tables[0].Rows[0]["Website"] != DBNull.Value ? ds.Tables[0].Rows[0]["Website"].ToString() : null;
                response.CompanyListings.CreatedBy = ds.Tables[0].Rows[0]["CreatedBy"] != DBNull.Value ? ds.Tables[0].Rows[0]["CreatedBy"].ToString() : null;
                response.CompanyListings.CreatedDate = ds.Tables[0].Rows[0]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedDate"].ToString()) : (DateTime?)null;

            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                response.CompanyListingProfile.Id = ds.Tables[1].Rows[0]["Id"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[1].Rows[0]["Id"].ToString()) : 0;
                response.CompanyListingProfile.ListingId = ds.Tables[1].Rows[0]["ListingId"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[1].Rows[0]["ListingId"].ToString()) : 0;
                response.CompanyListingProfile.AnnualTurnOver = ds.Tables[1].Rows[0]["AnnualTurnOver"] != DBNull.Value ? ds.Tables[1].Rows[0]["AnnualTurnOver"].ToString() : null;
                response.CompanyListingProfile.BriefAbout = ds.Tables[1].Rows[0]["BriefAbout"] != DBNull.Value ? ds.Tables[1].Rows[0]["BriefAbout"].ToString() : null;
                response.CompanyListingProfile.Certification = ds.Tables[1].Rows[0]["Certification"] != DBNull.Value ? ds.Tables[1].Rows[0]["Certification"].ToString() : null;
                response.CompanyListingProfile.CreatedBy = ds.Tables[1].Rows[0]["CreatedBy"] != DBNull.Value ? ds.Tables[1].Rows[0]["CreatedBy"].ToString() : null;
                response.CompanyListingProfile.CreatedDate = ds.Tables[1].Rows[0]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[1].Rows[0]["CreatedDate"].ToString()) : (DateTime?)null;
                response.CompanyListingProfile.LocationOverview = ds.Tables[1].Rows[0]["LocationOverview"] != DBNull.Value ? ds.Tables[1].Rows[0]["LocationOverview"].ToString() : null;
                response.CompanyListingProfile.NumberofEmployees = ds.Tables[1].Rows[0]["NumberofEmployees"] != DBNull.Value ? ds.Tables[1].Rows[0]["NumberofEmployees"].ToString() : null;
                response.CompanyListingProfile.ProductAndServices = ds.Tables[1].Rows[0]["ProductAndServices"] != DBNull.Value ? ds.Tables[1].Rows[0]["ProductAndServices"].ToString() : null;
                response.CompanyListingProfile.ProfessionalAssociation = ds.Tables[1].Rows[0]["ProfessionalAssociation"] != DBNull.Value ? ds.Tables[1].Rows[0]["ProfessionalAssociation"].ToString() : null;
                response.CompanyListingProfile.UpdatedBy = ds.Tables[1].Rows[0]["UpdatedBy"] != DBNull.Value ? ds.Tables[1].Rows[0]["UpdatedBy"].ToString() : null;
                response.CompanyListingProfile.UpdatedDate = ds.Tables[1].Rows[0]["UpdatedDate"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[1].Rows[0]["UpdatedDate"].ToString()) : (DateTime?)null;
                response.CompanyListingProfile.YearEstablished = ds.Tables[1].Rows[0]["YearEstablished"] != DBNull.Value ? Convert.ToInt32(ds.Tables[1].Rows[0]["YearEstablished"].ToString()) : 0;
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                response.ListingAddress.Area = ds.Tables[2].Rows[0]["Area"] != DBNull.Value ? ds.Tables[2].Rows[0]["Area"].ToString() : null;
                response.ListingAddress.UpdatedDate = ds.Tables[2].Rows[0]["UpdatedDate"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[2].Rows[0]["UpdatedDate"].ToString()) : (DateTime?)null;
                response.ListingAddress.BuildingAddress = ds.Tables[2].Rows[0]["BuildingAddress"] != DBNull.Value ? ds.Tables[2].Rows[0]["BuildingAddress"].ToString() : null;
                response.ListingAddress.CityId = ds.Tables[2].Rows[0]["CityId"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[2].Rows[0]["CityId"].ToString()) : 0;
                response.ListingAddress.CountryId = ds.Tables[2].Rows[0]["CountryId"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[2].Rows[0]["CountryId"].ToString()) : 0;
                response.ListingAddress.CityAreaId = ds.Tables[2].Rows[0]["CityAreaId"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[2].Rows[0]["CityAreaId"].ToString()) : 0;
                response.ListingAddress.CityAreaName = ds.Tables[2].Rows[0]["CityAreaName"] != DBNull.Value ? ds.Tables[2].Rows[0]["CityAreaName"].ToString() : null;
                response.ListingAddress.CreatedBy = ds.Tables[2].Rows[0]["CreatedBy"] != DBNull.Value ? ds.Tables[2].Rows[0]["CreatedBy"].ToString() : null;
                response.ListingAddress.CreatedDate = ds.Tables[2].Rows[0]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[2].Rows[0]["CreatedDate"].ToString()) : (DateTime?)null;
                response.ListingAddress.LandMark = ds.Tables[2].Rows[0]["LandMark"] != DBNull.Value ? ds.Tables[2].Rows[0]["LandMark"].ToString() : null;
                response.ListingAddress.Id = ds.Tables[2].Rows[0]["Id"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[2].Rows[0]["Id"].ToString()) : 0;
                response.ListingAddress.CityName = ds.Tables[2].Rows[0]["CityName"] != DBNull.Value ? ds.Tables[2].Rows[0]["CityName"].ToString() : null;
                response.ListingAddress.Latitude = ds.Tables[2].Rows[0]["Latitude"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[2].Rows[0]["Latitude"].ToString()) : 0;
                response.ListingAddress.LatLogAddress = ds.Tables[2].Rows[0]["LatLogAddress"] != DBNull.Value ? ds.Tables[2].Rows[0]["LatLogAddress"].ToString() : null;
                response.ListingAddress.ListingId = ds.Tables[2].Rows[0]["ListingId"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[2].Rows[0]["ListingId"].ToString()) : 0;
                response.ListingAddress.Longitude = ds.Tables[2].Rows[0]["Longitude"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[2].Rows[0]["Longitude"].ToString()) : 0;
                response.ListingAddress.StateId = ds.Tables[2].Rows[0]["StateId"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[2].Rows[0]["StateId"].ToString()) : 0;
                response.ListingAddress.StreetAddress = ds.Tables[2].Rows[0]["StreetAddress"] != DBNull.Value ? ds.Tables[2].Rows[0]["StreetAddress"].ToString() : null;
                response.ListingAddress.UpdateBy = ds.Tables[2].Rows[0]["UpdateBy"] != DBNull.Value ? ds.Tables[2].Rows[0]["UpdateBy"].ToString() : null;
            }
            if (ds.Tables[3].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[3].Rows)
                {
                    VMCompanyListingTimming _VMCompanyListingTimming = new VMCompanyListingTimming()
                    {
                        CreatedBy = row["CreatedBy"] != DBNull.Value ? row["CreatedBy"].ToString() : null,
                        CreatedDate = row["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDate"].ToString()) : (DateTime?)null,
                        DaysName = row["DaysName"] != DBNull.Value ? row["DaysName"].ToString() : null,
                        Id = row["Id"] != DBNull.Value ? Convert.ToDecimal(row["Id"].ToString()) : 0,
                        IsClosed = row["IsClosed"] != DBNull.Value ? Convert.ToBoolean(row["IsClosed"].ToString()) : false,
                        ListingId = row["ListingId"] != DBNull.Value ? Convert.ToDecimal(row["ListingId"].ToString()) : 0,
                        TimeFrom = row["TimeFrom"] != DBNull.Value ? row["TimeFrom"].ToString() : null,
                        TimeTo = row["TimeTo"] != DBNull.Value ? row["TimeTo"].ToString() : null,
                        UpdatedBy = row["UpdatedBy"] != DBNull.Value ? row["UpdatedBy"].ToString() : null,
                        UpdatedDate = row["UpdatedDate"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedDate"].ToString()) : (DateTime?)null,
                        WeekDayNo = row["WeekDayNo"] != DBNull.Value ? Convert.ToInt32(row["WeekDayNo"].ToString()) : 0,
                    };
                    response.CompanyListingTimming.Add(_VMCompanyListingTimming);
                }
            }
            if (ds.Tables[4].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[4].Rows)
                {
                    VMListingCategory _VMListingCategory = new VMListingCategory()
                    {
                        CreatedBy = row["CreatedBy"] != DBNull.Value ? row["CreatedBy"].ToString() : null,
                        CreatedDate = row["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDate"].ToString()) : (DateTime?)null,
                        Id = row["Id"] != DBNull.Value ? Convert.ToDecimal(row["Id"].ToString()) : 0,
                        ListingId = row["ListingId"] != DBNull.Value ? Convert.ToDecimal(row["ListingId"].ToString()) : 0,
                        MainCategoryId = row["MainCategoryId"] != DBNull.Value ? Convert.ToDecimal(row["MainCategoryId"].ToString()) : 0,
                        SubCategoryId = row["SubCategoryId"] != DBNull.Value ? Convert.ToDecimal(row["SubCategoryId"].ToString()) : 0,
                        SubCategoryName = row["SubCategoryName"] != DBNull.Value ? row["SubCategoryName"].ToString() : null,
                        MainCategoryName = row["MainCategoryName"] != DBNull.Value ? row["MainCategoryName"].ToString() : null,
                        UpdatedBy = row["UpdatedBy"] != DBNull.Value ? row["UpdatedBy"].ToString() : null,
                        UpdatedDate = row["UpdatedDate"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedDate"].ToString()) : (DateTime?)null,

                    };
                    response.ListingCategory.Add(_VMListingCategory);
                }
            }
            if (ds.Tables[5].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[5].Rows)
                {
                    VMListingGallery _VMListingGallery = new VMListingGallery()
                    {
                        CreatedBy = row["CreatedBy"] != DBNull.Value ? row["CreatedBy"].ToString() : null,
                        CreatedDate = row["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDate"].ToString()) : (DateTime?)null,
                        FileName = row["FileName"] != DBNull.Value ? row["FileName"].ToString() : null,
                        UpdatedDate = row["UpdatedDate"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedDate"].ToString()) : (DateTime?)null,
                        FileType = row["FileType"] != DBNull.Value ? row["FileType"].ToString() : null,
                        FileUrl = row["FileUrl"] != DBNull.Value ? row["FileUrl"].ToString() : null,
                        Id = row["Id"] != DBNull.Value ? Convert.ToDecimal(row["Id"].ToString()) : 0,
                        ListingId = row["ListingId"] != DBNull.Value ? Convert.ToDecimal(row["ListingId"].ToString()) : 0,
                        UpdatedBy = row["UpdatedBy"] != DBNull.Value ? row["UpdatedBy"].ToString() : null,
                        UploadDir = row["UploadDir"] != DBNull.Value ? row["UploadDir"].ToString() : null,
                    };
                    response.ListingGallery.Add(_VMListingGallery);

                }
            }
            if (ds.Tables[6].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[6].Rows)
                {
                    VMListingLandlineNo _VMListingLandlineNo = new VMListingLandlineNo()
                    {
                        CreatedBy = row["CreatedBy"] != DBNull.Value ? row["CreatedBy"].ToString() : null,
                        CreatedDate = row["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDate"].ToString()) : (DateTime?)null,
                        Id = row["Id"] != DBNull.Value ? Convert.ToDecimal(row["Id"].ToString()) : 0,
                        LandlineNumber = row["LandlineNumber"] != DBNull.Value ? row["LandlineNumber"].ToString() : null,
                        ListingId = row["ListingId"] != DBNull.Value ? Convert.ToDecimal(row["ListingId"].ToString()) : 0,
                        UpdatedBy = row["UpdatedBy"] != DBNull.Value ? row["UpdatedBy"].ToString() : null,
                        UpdatedDate = row["UpdatedDate"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedDate"].ToString()) : (DateTime?)null,

                    };
                    response.ListingLandlineNo.Add(_VMListingLandlineNo);
                }
            }
            if (ds.Tables[7].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[7].Rows)
                {
                    VMListingMobileNo _VMListingMobileNo = new VMListingMobileNo()
                    {
                        CreatedBy = row["CreatedBy"] != DBNull.Value ? row["CreatedBy"].ToString() : null,
                        CreatedDate = row["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDate"].ToString()) : (DateTime?)null,
                        Id = row["Id"] != DBNull.Value ? Convert.ToDecimal(row["Id"].ToString()) : 0,
                        ListingId = row["ListingId"] != DBNull.Value ? Convert.ToDecimal(row["ListingId"].ToString()) : 0,
                        MobileNo = row["MobileNo"] != DBNull.Value ? row["MobileNo"].ToString() : null,
                        OptCode = row["OptCode"] != DBNull.Value ? Convert.ToInt32(row["OptCode"].ToString()) : 0,
                        UpdatedBy = row["UpdatedBy"] != DBNull.Value ? row["UpdatedBy"].ToString() : null,
                        UpdatedDate = row["UpdatedDate"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedDate"].ToString()) : (DateTime?)null,

                    };
                    response.ListingMobileNo.Add(_VMListingMobileNo);
                }
            }
            if (ds.Tables[8].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[8].Rows)
                {
                    VMListingPaymentsMode _VMListingPaymentsMode = new VMListingPaymentsMode()
                    {
                        CreatedBy = row["CreatedBy"] != DBNull.Value ? row["CreatedBy"].ToString() : null,
                        CreatedDate = row["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDate"].ToString()) : (DateTime?)null,
                        Id = row["Id"] != DBNull.Value ? Convert.ToDecimal(row["Id"].ToString()) : 0,
                        ListingId = row["ListingId"] != DBNull.Value ? Convert.ToDecimal(row["ListingId"].ToString()) : 0,
                        ModeId = row["ModeId"] != DBNull.Value ? Convert.ToDecimal(row["ModeId"].ToString()) : 0,
                        ModeName = row["ModeName"] != DBNull.Value ? row["ModeName"].ToString() : null,
                        UpdateDate = row["UpdateDate"] != DBNull.Value ? Convert.ToDateTime(row["UpdateDate"].ToString()) : (DateTime?)null,
                        UpdatedBy = row["UpdatedBy"] != DBNull.Value ? row["UpdatedBy"].ToString() : null
                    };
                    response.ListingPaymentsMode.Add(_VMListingPaymentsMode);
                }
            }
            if (ds.Tables[9].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[9].Rows)
                {
                    VMListingsBusinessTypes _VMListingsBusinessTypes = new VMListingsBusinessTypes()
                    {
                        CreatedBy = row["CreatedBy"] != DBNull.Value ? row["CreatedBy"].ToString() : null,
                        CreatedDate = row["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDate"].ToString()) : (DateTime?)null,
                        Id = row["Id"] != DBNull.Value ? Convert.ToDecimal(row["Id"].ToString()) : 0,
                        BusinessId = row["BusinessId"] != DBNull.Value ? Convert.ToDecimal(row["BusinessId"].ToString()) : 0,
                        Text = row["Text"] != DBNull.Value ? row["Text"].ToString() : null,
                        ListingId = row["ListingId"] != DBNull.Value ? Convert.ToDecimal(row["ListingId"].ToString()) : 0,
                        UpdatedDate = row["UpdatedDate"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedDate"].ToString()) : (DateTime?)null,
                        UpdatedBy = row["UpdatedBy"] != DBNull.Value ? row["UpdatedBy"].ToString() : null
                    };
                    response.ListingsBusinessTypes.Add(_VMListingsBusinessTypes);
                }
            }
            if (ds.Tables[10].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[10].Rows)
                {
                    VMListingServices _VMListingServices = new VMListingServices()
                    {
                        CreatedBy = row["CreatedBy"] != DBNull.Value ? row["CreatedBy"].ToString() : null,
                        CreatedDate = row["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDate"].ToString()) : (DateTime?)null,
                        Id = row["Id"] != DBNull.Value ? Convert.ToDecimal(row["Id"].ToString()) : 0,
                        ListingId = row["ListingId"] != DBNull.Value ? Convert.ToDecimal(row["ListingId"].ToString()) : 0,
                        ServiceName = row["ServiceName"] != DBNull.Value ? row["ServiceName"].ToString() : null,
                        ServiceTypeId = row["ServiceTypeId"] != DBNull.Value ? Convert.ToDecimal(row["ServiceTypeId"].ToString()) : 0,
                        UpdatedBy = row["UpdatedBy"] != DBNull.Value ? row["UpdatedBy"].ToString() : null,
                        UpdatedDate = row["UpdatedDate"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedDate"].ToString()) : (DateTime?)null
                    };
                    response.ListingServices.Add(_VMListingServices);
                }
            }
            if (ds.Tables[11].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[11].Rows)
                {
                    VMSocialMediaModes _VMSocialMediaModes = new VMSocialMediaModes()
                    {
                        CreatedBy = row["CreatedBy"] != DBNull.Value ? row["CreatedBy"].ToString() : null,
                        CreatedDate = row["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDate"].ToString()) : (DateTime?)null,
                        Id = row["Id"] != DBNull.Value ? Convert.ToDecimal(row["Id"].ToString()) : 0,
                        MediaId = row["MediaId"] != DBNull.Value ? Convert.ToDecimal(row["MediaId"].ToString()) : 0,
                        Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : null,
                        SitePath = row["SitePath"] != DBNull.Value ? row["SitePath"].ToString() : null,
                        Imagedir = row["Imagedir"] != DBNull.Value ? row["Imagedir"].ToString() : null,
                        ImageDir = row["Imagedir"] != DBNull.Value ? row["Imagedir"].ToString() : null,
                        ImageUrl = row["ImageUrl"] != DBNull.Value ? row["ImageUrl"].ToString() : null

                    };
                    response.SocialMediaModes.Add(_VMSocialMediaModes);
                }
            }


            return response;
        }



        public static decimal Sp_AddListingFromBackEnd(VMAddCompanyListingModel Instance)
        {
            decimal Result = 0;
            SqlTransaction transaction = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();
                    if (!string.IsNullOrEmpty(Instance.Registration.Id))
                    {
                        SqlCommand cmd = new SqlCommand("dbo.Sp_CreateCompanyListingUserBE", conn, transaction)
                        {
                            CommandType = CommandType.StoredProcedure,
                        };
                        //Also Saved Data in AspNetUserRoles 
                        cmd.Parameters.Add(new SqlParameter("@Id", Instance.Registration.Id));
                        cmd.Parameters.Add(new SqlParameter("@UserName", Instance.Registration.UserName));
                        cmd.Parameters.Add(new SqlParameter("@NormalizedUserName", Instance.Registration.NormalizedUserName));
                        cmd.Parameters.Add(new SqlParameter("@Email", Instance.Registration.Email));
                        cmd.Parameters.Add(new SqlParameter("@NormalizedEmail", Instance.Registration.NormalizedEmail));
                        cmd.Parameters.Add(new SqlParameter("@EmailConfirmed", Instance.Registration.EmailConfirmed));
                        cmd.Parameters.Add(new SqlParameter("@PasswordHash", Instance.Registration.PasswordHash));
                        cmd.Parameters.Add(new SqlParameter("@SecurityStamp", Instance.Registration.SecurityStamp));
                        cmd.Parameters.Add(new SqlParameter("@PhoneNumberConfirmed", Instance.Registration.PhoneNumberConfirmed));
                        cmd.Parameters.Add(new SqlParameter("@LockoutEnabled", Instance.Registration.LockoutEnabled));
                        cmd.Parameters.Add(new SqlParameter("@AccessFailedCount", Instance.Registration.AccessFailedCount));
                        cmd.Parameters.Add(new SqlParameter("@CreatedBy", Instance.Registration.CreatedBy));
                        cmd.Parameters.Add(new SqlParameter("@CreatedDate", Instance.Registration.CreatedDate));
                        cmd.Parameters.Add(new SqlParameter("@UpdatedBy", Instance.Registration.UpdatedBy));
                        cmd.Parameters.Add(new SqlParameter("@UpdatedDate", Instance.Registration.UpdatedDate));
                        cmd.Parameters.Add(new SqlParameter("@UserTypeId", Instance.Registration.UserTypeId));
                        cmd.ExecuteNonQuery();
                        SqlCommand cmd1 = new SqlCommand("dbo.Sp_CreateCompanyListingCustomerBE", conn, transaction)
                        {
                            CommandType = CommandType.StoredProcedure,
                        };
                        cmd1.Parameters.Add(new SqlParameter("@FirstName", Instance.CompanyListings.FirstName));
                        cmd1.Parameters.Add(new SqlParameter("@LastName", Instance.CompanyListings.LastName));
                        cmd1.Parameters.Add(new SqlParameter("@IsActive", true));
                        cmd1.Parameters.Add(new SqlParameter("@IsDefault", false));
                        cmd1.Parameters.Add(new SqlParameter("@UserId", Instance.Registration.Id));
                        cmd1.Parameters.Add(new SqlParameter("@PhoneNumber", Instance.ListingMobileNo.Select(c => c.MobileNo).FirstOrDefault().ToString()));
                        cmd1.Parameters.Add(new SqlParameter("@CreatedBy", Instance.CompanyListings.CreatedBy));
                        cmd1.Parameters.Add(new SqlParameter("@CreatedDate", Instance.CompanyListings.CreatedDate));
                        cmd1.Parameters.Add(new SqlParameter("@UpdatedBy", Instance.CompanyListings.UpdatedBy));
                        cmd1.Parameters.Add(new SqlParameter("@UpdatedDate", Instance.CompanyListings.UpdatedDate));
                        using (var rdr = cmd1.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                Instance.CompanyListings.CustomerId = rdr.GetDecimal(0);
                            }
                        }
                    }
                    if (Instance.CustomerRegistration != null)
                    {
                        if (Instance.CustomerRegistration.Email == null)
                        {
                            SqlCommand cmd = new SqlCommand("dbo.Sp_GetEmailByCustomerIdBE", conn,transaction)
                            {
                                CommandType = CommandType.StoredProcedure,
                            };
                            cmd.Parameters.Add(new SqlParameter("@CustomerId", Instance.CompanyListings.CustomerId));
                            using (var rdr = cmd.ExecuteReader())
                            {
                                while (rdr.Read())
                                {
                                    Instance.CompanyListings.Email = rdr.GetString(0);
                                }
                            }
                        }
                    }
                    SqlCommand cmd2 = new SqlCommand("dbo.Sp_CreateCompanyListingBE", conn, transaction)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    cmd2.Parameters.Add(new SqlParameter("@CompanyName", Instance.CompanyListings.CompanyName ?? SqlString.Null));
                    cmd2.Parameters.Add(new SqlParameter("@FirstName", Instance.CompanyListings.FirstName ?? SqlString.Null));
                    cmd2.Parameters.Add(new SqlParameter("@LastName", Instance.CompanyListings.LastName ?? SqlString.Null));
                    cmd2.Parameters.Add(new SqlParameter("@Email", Instance.CompanyListings.Email ?? SqlString.Null));
                    cmd2.Parameters.Add(new SqlParameter("@Website", Instance.CompanyListings.Website != null? Instance.CompanyListings.Website:SqlString.Null));
                    cmd2.Parameters.Add(new SqlParameter("@MetaTitle", Instance.CompanyListings.MetaTitle ?? SqlString.Null));
                    cmd2.Parameters.Add(new SqlParameter("@MetaDescription", Instance.CompanyListings.MetaDescription ?? SqlString.Null));
                    cmd2.Parameters.Add(new SqlParameter("@MetaKeyword", Instance.CompanyListings.MetaKeyword ?? SqlString.Null));
                    cmd2.Parameters.Add(new SqlParameter("@ListingStatus", Instance.CompanyListings.ListingStatus));
                    cmd2.Parameters.Add(new SqlParameter("@BannerImage", Instance.CompanyListings.BannerImage ?? SqlString.Null));
                    cmd2.Parameters.Add(new SqlParameter("@BannerImageUrl", Instance.CompanyListings.BannerImageUrl ?? SqlString.Null));
                    cmd2.Parameters.Add(new SqlParameter("@ListingTypeId", Instance.CompanyListings.ListingTypeId));
                    cmd2.Parameters.Add(new SqlParameter("@CreatedBy", Instance.CompanyListings.CreatedBy ?? SqlString.Null));
                    cmd2.Parameters.Add(new SqlParameter("@CreatedDate", Instance.CompanyListings.CreatedDate ?? SqlDateTime.Null));
                    cmd2.Parameters.Add(new SqlParameter("@UpdatedBy", Instance.CompanyListings.UpdatedBy ?? SqlString.Null));
                    cmd2.Parameters.Add(new SqlParameter("@UpdatedDate", Instance.CompanyListings.UpdatedDate ?? SqlDateTime.Null));
                    cmd2.Parameters.Add(new SqlParameter("@CustomerId", Instance.CompanyListings.CustomerId));
                    using (var rdr = cmd2.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Instance.CompanyListings.Id = rdr.GetDecimal(0);
                            Result = rdr.GetDecimal(0);
                        }
                    }
                    // ListingAddress
                    if (Instance.ListingAddress.CityId > 0)
                    {
                        SqlCommand cmd = new SqlCommand("dbo.Sp_CreateListingAddressBE", conn, transaction)
                        {
                            CommandType = CommandType.StoredProcedure,
                        };
                        cmd.Parameters.Add(new SqlParameter("@BuildingAddress", Instance.ListingAddress.BuildingAddress ?? SqlString.Null));
                        cmd.Parameters.Add(new SqlParameter("@StreetAddress", Instance.ListingAddress.StreetAddress ?? SqlString.Null));
                        cmd.Parameters.Add(new SqlParameter("@LandMark", Instance.ListingAddress.LandMark ?? SqlString.Null));
                        cmd.Parameters.Add(new SqlParameter("@Area", Instance.ListingAddress.Area ?? SqlString.Null));
                        cmd.Parameters.Add(new SqlParameter("@Latitude", Instance.ListingAddress.Latitude ?? SqlDecimal.Null));
                        cmd.Parameters.Add(new SqlParameter("@Longitude", Instance.ListingAddress.Longitude ?? SqlDecimal.Null));
                        cmd.Parameters.Add(new SqlParameter("@LatLogAddress", Instance.ListingAddress.LatLogAddress ?? SqlString.Null));
                        cmd.Parameters.Add(new SqlParameter("@CityAreaId", Instance.ListingAddress.CityAreaId));
                        cmd.Parameters.Add(new SqlParameter("@CityId", Instance.ListingAddress.CityId));
                        cmd.Parameters.Add(new SqlParameter("@StateId", Instance.ListingAddress.StateId));
                        cmd.Parameters.Add(new SqlParameter("@CountryId", Instance.ListingAddress.CountryId));
                        cmd.Parameters.Add(new SqlParameter("@ListingId", Instance.CompanyListings.Id));
                        cmd.Parameters.Add(new SqlParameter("@CreatedBy", Instance.CompanyListings.CreatedBy ?? SqlString.Null));
                        cmd.Parameters.Add(new SqlParameter("@CreatedDate", Instance.CompanyListings.CreatedDate ?? SqlDateTime.Null));
                        cmd.Parameters.Add(new SqlParameter("@UpdateBy", Instance.CompanyListings.UpdatedBy ?? SqlString.Null));
                        cmd.Parameters.Add(new SqlParameter("@UpdatedDate", Instance.CompanyListings.UpdatedDate ?? SqlDateTime.Null));
                        cmd.ExecuteNonQuery();
                    }
                    // CompanyListingProfile
                    if (Instance.CompanyListingProfile != null)
                    {
                        SqlCommand cmd = new SqlCommand("dbo.Sp_CreateListingProfileBE", conn, transaction)
                        {
                            CommandType = CommandType.StoredProcedure,
                        };
                        cmd.Parameters.Add(new SqlParameter("@YearEstablished", Instance.CompanyListingProfile.YearEstablished ?? SqlInt32.Null));
                        cmd.Parameters.Add(new SqlParameter("@AnnualTurnOver", Instance.CompanyListingProfile.AnnualTurnOver ?? SqlString.Null));
                        cmd.Parameters.Add(new SqlParameter("@NumberofEmployees", Instance.CompanyListingProfile.NumberofEmployees ?? SqlString.Null));
                        cmd.Parameters.Add(new SqlParameter("@ProfessionalAssociation", Instance.CompanyListingProfile.ProfessionalAssociation ?? SqlString.Null));
                        cmd.Parameters.Add(new SqlParameter("@Certification", Instance.CompanyListingProfile.Certification ?? SqlString.Null));
                        cmd.Parameters.Add(new SqlParameter("@BriefAbout", Instance.CompanyListingProfile.BriefAbout ?? SqlString.Null));
                        cmd.Parameters.Add(new SqlParameter("@LocationOverview", Instance.CompanyListingProfile.LocationOverview ?? SqlString.Null));
                        cmd.Parameters.Add(new SqlParameter("@ProductAndServices", Instance.CompanyListingProfile.ProductAndServices ?? SqlString.Null));
                        cmd.Parameters.Add(new SqlParameter("@ListingId", Instance.CompanyListings.Id));
                        cmd.Parameters.Add(new SqlParameter("@CreatedBy", Instance.CompanyListings.CreatedBy ?? SqlString.Null));
                        cmd.Parameters.Add(new SqlParameter("@CreatedDate", Instance.CompanyListings.CreatedDate ?? SqlDateTime.Null));
                        cmd.Parameters.Add(new SqlParameter("@UpdatedBy", Instance.CompanyListings.UpdatedBy ?? SqlString.Null));
                        cmd.Parameters.Add(new SqlParameter("@UpdatedDate", Instance.CompanyListings.UpdatedDate ?? SqlDateTime.Null));
                        cmd.ExecuteNonQuery();
                    }
                    //CompanyListingCategory
                    if (Instance.ListingCategory != null && Instance.ListingCategory.Count() > 0)
                    {
                        foreach (var Lc in Instance.ListingCategory)
                        {
                            SqlCommand cmd = new SqlCommand("dbo.Sp_CreateListingCategoryBE", conn, transaction)
                            {
                                CommandType = CommandType.StoredProcedure,
                            };
                            cmd.Parameters.Add(new SqlParameter("@MainCategoryId", Lc.MainCategoryId));
                            cmd.Parameters.Add(new SqlParameter("@SubCategoryId", Lc.SubCategoryId));
                            cmd.Parameters.Add(new SqlParameter("@ListingId", Instance.CompanyListings.Id));
                            cmd.Parameters.Add(new SqlParameter("@CreatedBy", Instance.CompanyListings.CreatedBy ?? SqlString.Null));
                            cmd.Parameters.Add(new SqlParameter("@CreatedDate", Instance.CompanyListings.CreatedDate ?? SqlDateTime.Null));
                            cmd.Parameters.Add(new SqlParameter("@UpdatedBy", Instance.CompanyListings.UpdatedBy ?? SqlString.Null));
                            cmd.Parameters.Add(new SqlParameter("@UpdatedDate", Instance.CompanyListings.UpdatedDate ?? SqlDateTime.Null));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    //ListingPaymentsMode
                    if (Instance.ListingPaymentsMode != null && Instance.ListingPaymentsMode.Count() > 0)
                    {
                        foreach (var LPM in Instance.ListingPaymentsMode)
                        {
                            SqlCommand cmd = new SqlCommand("dbo.Sp_CreateListingPaymentsModeBE", conn, transaction)
                            {
                                CommandType = CommandType.StoredProcedure,
                            };
                            cmd.Parameters.Add(new SqlParameter("@ModeId", LPM.ModeId));
                            cmd.Parameters.Add(new SqlParameter("@ListingId", Instance.CompanyListings.Id));
                            cmd.Parameters.Add(new SqlParameter("@CreatedBy", Instance.CompanyListings.CreatedBy ?? SqlString.Null));
                            cmd.Parameters.Add(new SqlParameter("@CreatedDate", Instance.CompanyListings.CreatedDate ?? SqlDateTime.Null));
                            cmd.Parameters.Add(new SqlParameter("@UpdatedBy", Instance.CompanyListings.UpdatedBy ?? SqlString.Null));
                            cmd.Parameters.Add(new SqlParameter("@UpdateDate", Instance.CompanyListings.UpdatedDate ?? SqlDateTime.Null));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    //CompanyListingTimming
                    if (Instance.CompanyListingTimming != null && Instance.CompanyListingTimming.Count() > 0)
                    {
                        foreach (var CLT in Instance.CompanyListingTimming)
                        {
                            SqlCommand cmd = new SqlCommand("dbo.Sp_CreateCompanyListingTimmingBE", conn, transaction)
                            {
                                CommandType = CommandType.StoredProcedure,
                            };
                            cmd.Parameters.Add(new SqlParameter("@WeekDayNo", CLT.WeekDayNo ?? SqlInt32.Null));
                            cmd.Parameters.Add(new SqlParameter("@DaysName", CLT.DaysName ?? SqlString.Null));
                            cmd.Parameters.Add(new SqlParameter("@TimeFrom", CLT.TimeFrom ?? SqlString.Null));
                            cmd.Parameters.Add(new SqlParameter("@TimeTo", CLT.TimeTo ?? SqlString.Null));
                            cmd.Parameters.Add(new SqlParameter("@IsClosed", CLT.IsClosed));

                            cmd.Parameters.Add(new SqlParameter("@ListingId", Instance.CompanyListings.Id));
                            cmd.Parameters.Add(new SqlParameter("@CreatedBy", Instance.CompanyListings.CreatedBy ?? SqlString.Null));
                            cmd.Parameters.Add(new SqlParameter("@CreatedDate", Instance.CompanyListings.CreatedDate ?? SqlDateTime.Null));
                            cmd.Parameters.Add(new SqlParameter("@UpdatedBy", Instance.CompanyListings.UpdatedBy ?? SqlString.Null));
                            cmd.Parameters.Add(new SqlParameter("@UpdatedDate", Instance.CompanyListings.UpdatedDate ?? SqlDateTime.Null));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    //ListingLandlineNo
                    if (Instance.ListingLandlineNo != null && Instance.ListingLandlineNo.Count() > 0)
                    {
                        foreach (var LLN in Instance.ListingLandlineNo)
                        {
                            SqlCommand cmd = new SqlCommand("dbo.Sp_CreateListingLandlineNoBE", conn, transaction)
                            {
                                CommandType = CommandType.StoredProcedure,
                            };
                            cmd.Parameters.Add(new SqlParameter("@LandlineNumber", LLN.LandlineNumber ?? SqlString.Null));

                            cmd.Parameters.Add(new SqlParameter("@ListingId", Instance.CompanyListings.Id));
                            cmd.Parameters.Add(new SqlParameter("@CreatedBy", Instance.CompanyListings.CreatedBy ?? SqlString.Null));
                            cmd.Parameters.Add(new SqlParameter("@CreatedDate", Instance.CompanyListings.CreatedDate ?? SqlDateTime.Null));
                            cmd.Parameters.Add(new SqlParameter("@UpdatedBy", Instance.CompanyListings.UpdatedBy ?? SqlString.Null));
                            cmd.Parameters.Add(new SqlParameter("@UpdatedDate", Instance.CompanyListings.UpdatedDate ?? SqlDateTime.Null));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    //ListingMobileNo
                    if (Instance.ListingMobileNo != null && Instance.ListingMobileNo.Count() > 0)
                    {
                        foreach (var LMN in Instance.ListingMobileNo)
                        {
                            SqlCommand cmd = new SqlCommand("dbo.Sp_CreateListingMobileNoBE", conn, transaction)
                            {
                                CommandType = CommandType.StoredProcedure,
                            };
                            cmd.Parameters.Add(new SqlParameter("@MobileNo", LMN.MobileNo));
                            cmd.Parameters.Add(new SqlParameter("@ListingId", Instance.CompanyListings.Id));
                            cmd.Parameters.Add(new SqlParameter("@CreatedBy", Instance.CompanyListings.CreatedBy ?? SqlString.Null));
                            cmd.Parameters.Add(new SqlParameter("@CreatedDate", Instance.CompanyListings.CreatedDate ?? SqlDateTime.Null));
                            cmd.Parameters.Add(new SqlParameter("@UpdatedBy", Instance.CompanyListings.UpdatedBy ?? SqlString.Null));
                            cmd.Parameters.Add(new SqlParameter("@UpdatedDate", Instance.CompanyListings.UpdatedDate ?? SqlDateTime.Null));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    //ListingServices
                    if (Instance.ListingServices != null && Instance.ListingServices.Count > 0)
                    {
                        foreach (var LS in Instance.ListingServices)
                        {
                            SqlCommand cmd = new SqlCommand("dbo.Sp_CreateListingServicesBE", conn, transaction)
                            {
                                CommandType = CommandType.StoredProcedure,
                            };
                            cmd.Parameters.Add(new SqlParameter("@ServiceTypeId", LS.ServiceTypeId));
                            cmd.Parameters.Add(new SqlParameter("@ListingId", Instance.CompanyListings.Id));

                            cmd.Parameters.Add(new SqlParameter("@CreatedBy", Instance.CompanyListings.CreatedBy ?? SqlString.Null));
                            cmd.Parameters.Add(new SqlParameter("@CreatedDate", Instance.CompanyListings.CreatedDate ?? SqlDateTime.Null));
                            cmd.Parameters.Add(new SqlParameter("@UpdatedBy", Instance.CompanyListings.UpdatedBy ?? SqlString.Null));
                            cmd.Parameters.Add(new SqlParameter("@UpdatedDate", Instance.CompanyListings.UpdatedDate ?? SqlDateTime.Null));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    //ListingSocialMedia
                    if (Instance.ListingSocialMedia != null && Instance.ListingSocialMedia.Count > 0)
                    {
                        foreach (var LSM in Instance.ListingSocialMedia)
                        {
                            SqlCommand cmd = new SqlCommand("dbo.Sp_CreateListingSocialMediaBE", conn, transaction)
                            {
                                CommandType = CommandType.StoredProcedure,
                            };
                            cmd.Parameters.Add(new SqlParameter("@MediaNameId", LSM.Name));
                            cmd.Parameters.Add(new SqlParameter("@SitePath", LSM.SitePath ?? SqlString.Null));

                            cmd.Parameters.Add(new SqlParameter("@ListingId", Instance.CompanyListings.Id));
                            cmd.Parameters.Add(new SqlParameter("@CreatedBy", Instance.CompanyListings.CreatedBy ?? SqlString.Null));
                            cmd.Parameters.Add(new SqlParameter("@CreatedDate", Instance.CompanyListings.CreatedDate ?? SqlDateTime.Null));
                            cmd.Parameters.Add(new SqlParameter("@UpdatedBy", Instance.CompanyListings.UpdatedBy ?? SqlString.Null));
                            cmd.Parameters.Add(new SqlParameter("@UpdateDate", Instance.CompanyListings.UpdatedDate ?? SqlDateTime.Null));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    if (Instance.ListingsBusinessTypes != null && Instance.ListingsBusinessTypes.Count > 0)
                    {
                        foreach (var BT in Instance.ListingsBusinessTypes)
                        {
                            SqlCommand cmd = new SqlCommand("dbo.Sp_CreateListingsBusinessTypesBE", conn, transaction)
                            {
                                CommandType = CommandType.StoredProcedure,
                            };
                            cmd.Parameters.Add(new SqlParameter("@BusinessId", BT.BusinessId));

                            cmd.Parameters.Add(new SqlParameter("@ListingId", Instance.CompanyListings.Id));
                            cmd.Parameters.Add(new SqlParameter("@CreatedBy", Instance.CompanyListings.CreatedBy ?? SqlString.Null));
                            cmd.Parameters.Add(new SqlParameter("@CreatedDate", Instance.CompanyListings.CreatedDate ?? SqlDateTime.Null));
                            cmd.Parameters.Add(new SqlParameter("@UpdatedBy", Instance.CompanyListings.UpdatedBy ?? SqlString.Null));
                            cmd.Parameters.Add(new SqlParameter("@UpdatedDate", Instance.CompanyListings.UpdatedDate ?? SqlDateTime.Null));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Result = 0;
                }
                conn.Close();
            }
            return Result;
        }
    }
}
