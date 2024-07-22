using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PAKDial.StoreProcdures;
using PAKDial.Domains.Common;
using PAKDial.Domains.ResponseModels.SalePersonsOrders;
using PAKDial.Common;
using PAKDial.Domains.StoreProcedureModel;
using PAKDial.StoreProcdures.AutoJobsProcedures;

namespace PAKDial.Repository.Repositories.CompaniesListingsRepo
{
    public class ListingPremiumRepository : BaseRepository<ListingPremium, decimal>, IListingPremiumRepository
    {
        public ListingPremiumRepository(PAKDialSolutionsContext context)
            : base(context)
        {

        }

        /// Primary database set
        protected override DbSet<ListingPremium> DbSet
        {
            get
            {
                return db.ListingPremium;
            }
        }

        public decimal AddAssignPackage(ListingPremium listingPremium)
        {
            decimal Results = 0;//Not Package Assigned To Listing
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (CheckPackageAssigned(listingPremium.ListingId) == false)
                    {
                        var GetTimehour = DateTime.Now.Hour + 1;
                        var GetCurrentDate = DateTime.Now;
                        if (listingPremium.ListingFrom.Value.Date >= GetCurrentDate.Date)
                        {
                            listingPremium.ListingFrom = listingPremium.ListingFrom + new TimeSpan(GetTimehour, 00, 00);
                            listingPremium.ListingTo = listingPremium.ListingTo + new TimeSpan(GetTimehour, 00, 00);
                            listingPremium.IsActive = true;

                            var GetListing = db.CompanyListings.Where(c => c.Id == listingPremium.ListingId).FirstOrDefault();
                            if (GetListing.ListingTypeId == 1)
                            {
                                GetListing.ListingTypeId = 2;
                                db.CompanyListings.Update(GetListing);
                            }
                            db.ListingPremium.Add(listingPremium);
                            if (db.SaveChanges() > 0)
                            {
                                Results = 1; //Package Assigned and Activated Successfully
                                transaction.Commit();
                            }
                        }
                    }
                    else
                    {
                        Results = -1; //Already Package Assigned
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            return Results;
        }

        public decimal UpdateAssignPackage(ListingPremium listingPremium)
        {
            decimal Results = 0;//Not Package Assigned To Listing
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (DbSet.Where(c => c.ListingId == listingPremium.ListingId && c.IsActive == true && c.Id != listingPremium.Id).Count() < 1)
                    {
                        var GetCurrentDate = DateTime.Now;
                        if (listingPremium.ListingTo.Value.Date > GetCurrentDate.Date)
                        {
                            var listingPrem = db.ListingPremium.Where(c => c.Id == listingPremium.Id).FirstOrDefault();
                            if (listingPrem.IsActive == listingPremium.IsActive)
                            {
                                Results = 1; //Package Assigned and Activated Successfully
                            }
                            else
                            {
                                var GetListing = db.CompanyListings.Where(c => c.Id == listingPremium.ListingId).FirstOrDefault();
                                if (listingPremium.IsActive == true)
                                {
                                    if (GetListing.ListingTypeId == 1)
                                    {
                                        GetListing.ListingTypeId = 2;
                                        db.CompanyListings.Update(GetListing);
                                    }
                                    listingPrem.IsActive = true;
                                    listingPrem.UpdatedBy = listingPremium.UpdatedBy;
                                    listingPrem.UpdatedDate = listingPremium.UpdatedDate;
                                    db.ListingPremium.Update(listingPrem);
                                }
                                else
                                {
                                    if (GetListing.ListingTypeId == 2)
                                    {
                                        GetListing.ListingTypeId = 1;
                                        db.CompanyListings.Update(GetListing);
                                    }
                                    listingPrem.IsActive = false;
                                    listingPrem.UpdatedBy = listingPremium.UpdatedBy;
                                    listingPrem.UpdatedDate = listingPremium.UpdatedDate;
                                    db.ListingPremium.Update(listingPrem);
                                }

                                if (db.SaveChanges() > 0)
                                {
                                    Results = 1; //Package Assigned and Activated Successfully
                                }
                            }
                        }

                    }
                    else
                    {
                        Results = -1; //Already Package Assigned
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            return Results;
        }

        public decimal DeleteAssignPackage(decimal Id)
        {
            decimal Results = 0;//Package Assigned To Listing Deleted
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var GetAssignedPackage = db.ListingPremium.Where(c => c.Id == Id).FirstOrDefault();
                    if (db.ListingPremium.Where(c => c.Id != Id && c.IsActive == true && c.ListingId == GetAssignedPackage.ListingId).Count() > 0)
                    {
                        db.ListingPremium.Remove(GetAssignedPackage);
                    }
                    else
                    {
                        var Listings = db.CompanyListings.Where(c => c.Id == GetAssignedPackage.ListingId).FirstOrDefault();
                        if (Listings.ListingTypeId == 2)
                        {
                            Listings.ListingTypeId = 1;
                            db.CompanyListings.Update(Listings);
                        }
                        db.ListingPremium.Remove(GetAssignedPackage);
                    }
                    db.SaveChanges();
                    transaction.Commit();
                    Results = 1; //Assigned Package Not Deleted Successfully
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            return Results;
        }

        public bool CheckPackageAssigned(decimal ListingId)
        {
            return DbSet.Where(c => c.ListingId == ListingId && c.IsActive == true).FirstOrDefault() != null ? true : false;
        }

        public AssignListingPackageResponse Get(AssignListingPackageRequestModel request)
        {
            try
            {
                int fromRow = 0;
                if (request.PageNo == 1)
                {
                    fromRow = (request.PageNo - 1) * request.PageSize;
                }
                else
                {
                    fromRow = request.PageNo;
                }
                int toRow = request.PageSize;
                bool isSearchFilterSpecified = !string.IsNullOrEmpty(request.SearchString);

                Expression<Func<VAssignListingPackages, bool>> query = null;
                if (request.CustomerId > 0)
                {
                    query =
                    exp =>
                         (!isSearchFilterSpecified && ((exp.ModeName.Contains(request.SearchString)) ||
                         (exp.PackageName.Contains(request.SearchString))) || isSearchFilterSpecified) && exp.ListingId == request.ListingId
                         && exp.CustomerId == request.CustomerId;
                }
                else
                {
                    query =
                    exp =>
                         (!isSearchFilterSpecified && ((exp.ModeName.Contains(request.SearchString)) ||
                         (exp.PackageName.Contains(request.SearchString))) || isSearchFilterSpecified) && exp.ListingId == request.ListingId;
                }



                int rowCount = db.VAssignListingPackages.Count(query);
                // Server Side Pager
                IEnumerable<VAssignListingPackages> vAssignListings = request.IsAsc
                    ? db.VAssignListingPackages.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList()
                    : db.VAssignListingPackages.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList();
                return new AssignListingPackageResponse
                {
                    RowCount = rowCount,
                    AssignListingPackages = vAssignListings
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public List<ListingPremium> GetByListingId(decimal ListingId)
        {
            return DbSet.Where(c => c.ListingId == ListingId).ToList();
        }

        public List<ListingPremium> GetListingPremium(decimal PackageId)
        {
            return DbSet.Where(c => c.PackageId == PackageId).ToList();

        }

        public List<ListingPremium> GetListingPremium(decimal ModeId, decimal PackageId)
        {
            return DbSet.Where(c => c.ModeId == ModeId && c.PackageId == PackageId).ToList();
        }

        public ListingPremium GetListingPremium(decimal ModeId, decimal PackageId, decimal ListingId)
        {
            return DbSet.Where(c => c.ModeId == ModeId && c.PackageId == PackageId && c.ListingId == ListingId).FirstOrDefault();
        }

        public List<ListingPremium> GetListingPremiumModeId(decimal ModeId)
        {
            return DbSet.Where(c => c.ModeId == ModeId).ToList();
        }

        public GetPackagesandModesResponse GetPakagesModes()
        {
            GetPackagesandModesResponse response = new GetPackagesandModesResponse
            {
                Packages = db.ListingPackages.Where(c => c.IsActive == true).Select(c => new VMKeyValuePair { id = c.Id, text = c.Name }).ToList(),
                PaymentsModes = db.PaymentModes.Where(c => c.IsActive == true).Select(c => new VMKeyValuePair { id = c.Id, text = c.Name }).ToList(),
            };
            return response;
        }

        // Sales Executive //Sale Manager Packages List On Load
        public VLoadSalesExectivePackagesResponse GetLoadSalesManagers(VLoadSalesExectivePackagesRequest request)
        {
            try
            {
                request.CityAreasId = SaleExecutiveOrders_Execution.SpGetCityAreasByUserId(request.UserId);
                int fromRow = 0;
                if (request.PageNo == 1)
                {
                    fromRow = (request.PageNo - 1) * request.PageSize;
                }
                else
                {
                    fromRow = request.PageNo;
                }
                int toRow = request.PageSize;
                bool isSearchFilterSpecified = !string.IsNullOrEmpty(request.SearchString);

                Expression<Func<VLoadSalesExectivePackages, bool>> query = null;
                if (request.CityAreasId.Count > 0)
                {
                    query =
                    exp =>
                    (isSearchFilterSpecified &&
                    ((exp.CompanyName.Contains(request.SearchString)) || (exp.FullName.Contains(request.SearchString)) ||
                    (exp.PackageName.Contains(request.SearchString)) || (exp.ModeName.Contains(request.SearchString)) ||
                    (exp.StatusName.Contains(request.SearchString)) || (Convert.ToString(exp.Id) == Convert.ToString(request.SearchString)))
                    || !isSearchFilterSpecified) &&
                    (exp.CreatedBy == request.UserId || exp.UpdatedBy == request.UserId || (exp.AssignedTo == request.UserId && exp.StatusName == PremiumStatuses.Pending.ToString())) &&
                    request.CityAreasId.Contains(exp.CityAreaId);
                }
                else
                {
                    query =
                    exp =>
                    (isSearchFilterSpecified &&
                    ((exp.CompanyName.Contains(request.SearchString)) || (exp.FullName.Contains(request.SearchString)) ||
                    (exp.PackageName.Contains(request.SearchString)) || (exp.ModeName.Contains(request.SearchString)) ||
                    (exp.StatusName.Contains(request.SearchString)) || (Convert.ToString(exp.Id) == Convert.ToString(request.SearchString)))
                    || !isSearchFilterSpecified) &&
                    (exp.CreatedBy == request.UserId || exp.UpdatedBy == request.UserId || (exp.AssignedTo == request.UserId && exp.StatusName == PremiumStatuses.Pending.ToString()));
                }

                int rowCount = db.VLoadSalesExectivePackages.Count(query);
                // Server Side Pager
                IEnumerable<VLoadSalesExectivePackages> vSEOrdersListings = request.IsAsc
                    ? db.VLoadSalesExectivePackages.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList()
                    : db.VLoadSalesExectivePackages.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList();
                return new VLoadSalesExectivePackagesResponse
                {
                    RowCount = rowCount,
                    Vloadsalesexectivepackages = vSEOrdersListings
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public AddSeOrderResponse AddOrdersSaleManager(VMSaleExCreate instance)
        {
            AddSeOrderResponse response = new AddSeOrderResponse();
            if (SaleExecutiveOrders_Execution.GetCountActiveListingByListingId(instance.ListingId) < 1)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var Statues = db.PremiumStatus.ToList();
                        var GetPaymentMode = db.PaymentModes.Where(c => c.Id == instance.ModeId).FirstOrDefault();
                        var GetPackage = db.VPackagePrices.Where(c => c.Id == instance.PackageId && c.PriceActive == true).FirstOrDefault();
                        var RoleIdDesignationId = CommonStoreProcedure.GetRoleandDesignationByUserId(instance.CreatedBy);
                        if (CommonSpacing.RemoveSpacestoTrim(GetPaymentMode.Name) == PaymentsMode.Cash.ToString().Trim().ToLower())
                        {
                            ListingPremium premium = new ListingPremium()
                            {
                                ListingId = instance.ListingId,
                                PackageId = instance.PackageId,
                                ModeId = instance.ModeId,
                                ListingFrom = DateTime.Now,
                                ListingTo = DateTime.Now.AddMonths((int)GetPackage.Months),
                                IsActive = true,
                                Deposited = false,
                                StatusId = Statues.Where(c => c.Name == PremiumStatuses.Approved.ToString()).FirstOrDefault().Id,
                                Discount = instance.Discount,
                                IsDiscount = instance.IsDiscount

                            };
                            if((bool)instance.IsDiscount)
                            {
                                if (instance.DiscountType.Trim().ToLower() == "per".ToString().ToLower())
                                {
                                   
                                    premium.Discount = instance.Discount + " %";
                                    premium.Price = (decimal)GetPackage.Price - ((decimal)GetPackage.Price * Convert.ToDecimal(instance.Discount) / 100);
                                }
                                else
                                {
                                    premium.Discount = instance.Discount + " Rs";
                                    premium.Price = (decimal)GetPackage.Price -  Convert.ToDecimal(instance.Discount);
                                }

                            }
                            else
                            {
                                premium.Price = (decimal)GetPackage.Price;
                                
                                
                            }
                         
                            var Listing = db.CompanyListings.Where(c => c.Id == premium.ListingId).FirstOrDefault();
                            Listing.ListingTypeId = 2; //Premium
                            db.CompanyListings.Update(Listing);
                            db.ListingPremium.Add(premium);
                            db.SaveChanges();
                            PremiumManageStatus pstates = new PremiumManageStatus
                            {
                                PremiumId = premium.Id,
                                RoleId = RoleIdDesignationId.RoleId,
                                DesignationId = RoleIdDesignationId.DesignationId,
                                StatusId = premium.StatusId,
                                Deposited = false,
                                CreatedBy = instance.CreatedBy,
                                CreatedDate = instance.CreatedDate,
                            };
                            db.PremiumManageStatus.Add(pstates);
                            db.SaveChanges();
                            transaction.Commit();
                            response.InvoiceNo = premium.Id;
                            response.AssignedPackage = GetPackage.Name;
                            if ((bool)instance.IsDiscount)
                            {
                                response.PackageCost = (decimal)GetPackage.Price -Convert.ToDecimal(instance.Discount);
                            }
                            else
                            {
                                response.PackageCost = (decimal)GetPackage.Price;
                            }
                               
                            response.PackageMonths = (int)GetPackage.Months;
                            response.PaymentMode = GetPaymentMode.Name;
                            response.ProcessMessage = "Package Assigned and Activated Successfully";
                            response.ListingId = instance.ListingId;
                            response.CompanyName = db.CompanyListings.Where(c => c.Id == instance.ListingId).FirstOrDefault().CompanyName;
                            response.CompanyMobileNo = db.ListingMobileNo.Where(c => c.ListingId == instance.ListingId).FirstOrDefault().MobileNo;
                            var employee = db.Employee.Where(c => c.UserId == instance.CreatedBy).FirstOrDefault();
                            response.SalePersonNo = db.EmployeeContact.Where(c => c.EmployeeId == employee.Id).FirstOrDefault().ContactNo;
                        }

                        else if (CommonSpacing.RemoveSpacestoTrim(GetPaymentMode.Name) == PaymentsMode.OnlinePayment.ToString().Trim().ToLower())
                        {
                            ListingPremium premium = new ListingPremium()
                            {
                                AccountNo = instance.AccountNo,
                                BankName = instance.BankName,
                                ListingId = instance.ListingId,
                                PackageId = instance.PackageId,
                                ModeId = instance.ModeId,
                               // Price = (decimal)GetPackage.Price,
                                IsActive = false,
                                Deposited = true,
                                StatusId = Statues.Where(c => c.Name == PremiumStatuses.Process.ToString()).FirstOrDefault().Id,
                                Discount = instance.Discount,
                                IsDiscount = instance.IsDiscount
                            };
                            if ((bool)instance.IsDiscount)
                            {
                                if (instance.DiscountType.Trim().ToLower() == "per".ToString().ToLower())
                                {
                                    premium.Discount = instance.Discount + " %";
                                    premium.Price = (decimal)GetPackage.Price - ((decimal)GetPackage.Price * Convert.ToDecimal(instance.Discount) / 100);
                                }
                                else
                                {
                                    premium.Discount = instance.Discount + " Rs";
                                    premium.Price = (decimal)GetPackage.Price - Convert.ToDecimal(instance.Discount);
                                }



                            }
                            else
                            {
                                premium.Price = (decimal)GetPackage.Price;
                                
                                
                            }
                            db.ListingPremium.Add(premium);
                            db.SaveChanges();
                            PremiumManageStatus pstates = new PremiumManageStatus
                            {
                                PremiumId = premium.Id,
                                RoleId = RoleIdDesignationId.RoleId,
                                DesignationId = RoleIdDesignationId.DesignationId,
                                StatusId = premium.StatusId,
                                Deposited = true,
                                CreatedBy = instance.CreatedBy,
                                CreatedDate = instance.CreatedDate,
                            };
                            db.PremiumManageStatus.Add(pstates);
                            db.SaveChanges();
                            transaction.Commit();
                            response.InvoiceNo = premium.Id;
                            response.BankName = instance.BankName;
                            response.AccountNo = instance.AccountNo;
                            response.AssignedPackage = GetPackage.Name;
                            if ((bool)instance.IsDiscount)
                            {
                                response.PackageCost = (decimal)GetPackage.Price - Convert.ToDecimal(instance.Discount);
                            }
                            else
                            {
                                response.PackageCost = (decimal)GetPackage.Price;
                            }

                            // response.PackageCost = (decimal)GetPackage.Price;
                            response.PackageMonths = (int)GetPackage.Months;
                            response.PaymentMode = GetPaymentMode.Name;
                            response.ProcessMessage = "Package Assigned But in Process For Clearence";
                            response.ListingId = instance.ListingId;
                            response.CompanyName = db.CompanyListings.Where(c => c.Id == instance.ListingId).FirstOrDefault().CompanyName;
                            response.CompanyMobileNo = db.ListingMobileNo.Where(c => c.ListingId == instance.ListingId).FirstOrDefault().MobileNo;
                            var employee = db.Employee.Where(c => c.UserId == instance.CreatedBy).FirstOrDefault();
                            response.SalePersonNo = db.EmployeeContact.Where(c => c.EmployeeId == employee.Id).FirstOrDefault().ContactNo;
                        }

                        else if (CommonSpacing.RemoveSpacestoTrim(GetPaymentMode.Name) == PaymentsMode.PayOrder.ToString().Trim().ToLower()
                            || CommonSpacing.RemoveSpacestoTrim(GetPaymentMode.Name) == PaymentsMode.Cheque.ToString().Trim().ToLower())
                        {
                            ListingPremium premium = new ListingPremium()
                            {
                                BankName = instance.BankName,
                                ChequeNo = instance.ChequeNo,
                                ListingId = instance.ListingId,
                                PackageId = instance.PackageId,
                                ModeId = instance.ModeId,
                               // Price = (decimal)GetPackage.Price,
                                IsActive = false,
                                Deposited = false,
                                StatusId = Statues.Where(c => c.Name == PremiumStatuses.Process.ToString()).FirstOrDefault().Id,
                                Discount = instance.Discount,
                                IsDiscount = instance.IsDiscount
                        };

                            if ((bool)instance.IsDiscount)
                            {
                                if (instance.DiscountType.Trim().ToLower() == "per".ToString().ToLower())
                                {
                                    premium.Discount = instance.Discount + " %";
                                    premium.Price = (decimal)GetPackage.Price - ((decimal)GetPackage.Price * Convert.ToDecimal(instance.Discount) / 100);
                                }
                                else
                                {
                                    premium.Discount = instance.Discount + " Rs";
                                    premium.Price = (decimal)GetPackage.Price - Convert.ToDecimal(instance.Discount);
                                }



                            }
                            else
                            {
                                premium.Price = (decimal)GetPackage.Price;
                            
                            
                            }

                            db.ListingPremium.Add(premium);
                            db.SaveChanges();
                            PremiumManageStatus pstates = new PremiumManageStatus
                            {
                                PremiumId = premium.Id,
                                RoleId = RoleIdDesignationId.RoleId,
                                DesignationId = RoleIdDesignationId.DesignationId,
                                StatusId = premium.StatusId,
                                Deposited = false,
                                CreatedBy = instance.CreatedBy,
                                CreatedDate = instance.CreatedDate,
                            };
                            db.PremiumManageStatus.Add(pstates);
                            db.SaveChanges();
                            transaction.Commit();
                            response.InvoiceNo = premium.Id;
                            response.BankName = instance.BankName;
                            response.ChequeNo = instance.ChequeNo;
                            response.AssignedPackage = GetPackage.Name;
                            if ((bool)instance.IsDiscount)
                            {
                                response.PackageCost = (decimal)GetPackage.Price - Convert.ToDecimal(instance.Discount);
                            }
                            else
                            {
                                response.PackageCost = (decimal)GetPackage.Price;
                            }

                            //response.PackageCost = (decimal)GetPackage.Price;
                            response.PackageMonths = (int)GetPackage.Months;
                            response.PaymentMode = GetPaymentMode.Name;
                            response.ProcessMessage = "Package Assigned But in Process For Clearence";
                            response.ListingId = instance.ListingId;
                            response.CompanyName = db.CompanyListings.Where(c => c.Id == instance.ListingId).FirstOrDefault().CompanyName;
                            response.CompanyMobileNo = db.ListingMobileNo.Where(c => c.ListingId == instance.ListingId).FirstOrDefault().MobileNo;
                            var employee = db.Employee.Where(c => c.UserId == instance.CreatedBy).FirstOrDefault();
                            response.SalePersonNo = db.EmployeeContact.Where(c => c.EmployeeId == employee.Id).FirstOrDefault().ContactNo;
                        }
                    }
                    catch (Exception ex)
                    {
                        response.ProcessMessage = ex.Message;
                        transaction.Rollback();
                    }
                }
            }
            else
            {
                response.ProcessMessage = "Package Already Exists";
            }
            return response;
        }
        public GetModeandPackagesResponse GetModeandPackagesResponses()
        {
            GetModeandPackagesResponse response = new GetModeandPackagesResponse
            {
                PaymentModeList = db.PaymentModes.Where(c => CommonSpacing.RemoveSpacestoTrim(c.Name) == PaymentsMode.Cash.ToString().ToLower() || CommonSpacing.RemoveSpacestoTrim(c.Name) == PaymentsMode.Cheque.ToString().ToLower()
                || CommonSpacing.RemoveSpacestoTrim(c.Name) == PaymentsMode.PayOrder.ToString().ToLower() || CommonSpacing.RemoveSpacestoTrim(c.Name) == PaymentsMode.OnlinePayment.ToString().ToLower())
                .Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.Name }).ToList(),
                PackagesList = db.ListingPackages.Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.Name }).ToList()
            };
            return response;
        }
        public GetModeandPackagesResponse GetPaymentsModeResponses()
        {
            GetModeandPackagesResponse response = new GetModeandPackagesResponse
            {
                PaymentModeList = db.PaymentModes.Where(c => CommonSpacing.RemoveSpacestoTrim(c.Name) == PaymentsMode.Cash.ToString().ToLower() || CommonSpacing.RemoveSpacestoTrim(c.Name) == PaymentsMode.Cheque.ToString().ToLower()
                || CommonSpacing.RemoveSpacestoTrim(c.Name) == PaymentsMode.PayOrder.ToString().ToLower())
                .Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.Name }).ToList(),
            };
            return response;
        }
        public SeOrderReportResponse GetSEReportSale(decimal InvoiceId)
        {
            SeOrderReportResponse response = new SeOrderReportResponse();
            var GetListingPremium = db.VLoadSaleRptandCcRpt.Where(c => c.Id == InvoiceId && c.CreatedDate != null).FirstOrDefault();
            if (GetListingPremium != null)
            {
                response.InvoiceNo = GetListingPremium.Id;
                response.BankName = GetListingPremium.BankName;
                response.AccountNo = GetListingPremium.Account_No;
                response.ChequeNo = GetListingPremium.ChequeNo;
                response.PaymentMode = CommonSpacing.RemoveSpacestoTrim(GetListingPremium.ModeName);
                response.BusinessPersonName = GetListingPremium.FullName;
                response.ListedAddress = GetListingPremium.ListedAddress;
                response.CreatedDate = CommonDateFormat.GetDateInMonthString((DateTime)GetListingPremium.CreatedDate);
                response.Discount = GetListingPremium.Discount;
                response.isDiscount = Convert.ToBoolean(GetListingPremium.IsDiscount);
                response.OrderTracking = SaleExecutiveOrders_Execution.SpGetSeOrderTrackings(GetListingPremium.Id);
                if (response.OrderTracking.Count() > 0)
                {
                    foreach (var item in response.OrderTracking)
                    {
                        if (item.CreatedDate != null)
                        {
                            item.CreatedDateVm = CommonDateFormat.GetDateInMonthString((DateTime)item.CreatedDate);
                        }
                        else
                        {
                            item.UpdatedDateVm = CommonDateFormat.GetDateInMonthString((DateTime)item.UpdatedDate);
                        }
                    }
                }
                response.EmailPhone = SaleExecutiveOrders_Execution.SpGetEmailAndPhoneByOrderIds(GetListingPremium.Id);
                response.SEOrderDetails = SaleExecutiveOrders_Execution.SpGetSeOrderDetailByOrderIds(GetListingPremium.Id);
            }
            return response;
        }
        public VMSaleExCollect GetSePendingOrdersfor(decimal InvoiceId)
        {
            return SaleExecutiveOrders_Execution.SpGetCollectPaymentByInvoiceId(InvoiceId);
        }
        public UpdateSeOrderResponse CollectOrdersSaleManager(VMSaleExCollect instance)
        {
            UpdateSeOrderResponse response = new UpdateSeOrderResponse();
            var GetSaleOrder = db.ListingPremium.Where(c => c.Id == instance.Id).FirstOrDefault();
            if (GetSaleOrder != null)
            {
                if (SaleExecutiveOrders_Execution.GetActiveOrderByListingId(GetSaleOrder.ListingId) < 1)
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var Statues = db.PremiumStatus.ToList();
                            var GetAssigned = db.PackageTransfer.Where(c => c.AssignedTo == instance.UpdatedBy && c.PremiumId == instance.Id).FirstOrDefault();
                            if (GetAssigned != null && Statues.Where(c => c.Id == GetSaleOrder.StatusId).FirstOrDefault().Name == PremiumStatuses.Pending.ToString())
                            {
                                var GetPaymentMode = db.PaymentModes.Where(c => c.Id == instance.ModeId).FirstOrDefault();
                                var GetPackage = db.VPackagePrices.Where(c => c.Id == GetSaleOrder.PackageId && c.PriceActive == true).FirstOrDefault();
                                var RoleIdDesignationId = CommonStoreProcedure.GetRoleandDesignationByUserId(instance.UpdatedBy);
                                if (CommonSpacing.RemoveSpacestoTrim(GetPaymentMode.Name) == PaymentsMode.Cash.ToString().Trim().ToLower())
                                {
                                    GetSaleOrder.IsActive = true;
                                    GetSaleOrder.ModeId = instance.ModeId;
                                    GetSaleOrder.ListingFrom = DateTime.Now;
                                    GetSaleOrder.ListingTo = DateTime.Now.AddMonths((int)GetPackage.Months);
                                    GetSaleOrder.StatusId = Statues.Where(c => c.Name == PremiumStatuses.Approved.ToString()).FirstOrDefault().Id;
                                    db.ListingPremium.Update(GetSaleOrder);
                                    PremiumManageStatus pstates = new PremiumManageStatus
                                    {
                                        PremiumId = GetSaleOrder.Id,
                                        RoleId = RoleIdDesignationId.RoleId,
                                        DesignationId = RoleIdDesignationId.DesignationId,
                                        StatusId = GetSaleOrder.StatusId,
                                        Deposited = false,
                                        UpdatedBy = instance.UpdatedBy,
                                        UpdatedDate = instance.UpdatedDate,
                                    };
                                    var Listing = db.CompanyListings.Where(c => c.Id == GetSaleOrder.ListingId).FirstOrDefault();
                                    Listing.ListingTypeId = 2; //Premium
                                    db.CompanyListings.Update(Listing);
                                    db.PremiumManageStatus.Add(pstates);
                                    db.SaveChanges();
                                    transaction.Commit();
                                    response.InvoiceNo = GetSaleOrder.Id;
                                    response.AssignedPackage = GetPackage.Name;
                                    response.PackageCost = GetSaleOrder.Price;
                                    response.PackageMonths = (int)GetPackage.Months;
                                    response.PaymentMode = GetPaymentMode.Name;
                                    response.ProcessMessage = "Package Activated Successfully";
                                    response.ListingId = GetSaleOrder.ListingId;
                                    response.CompanyName = db.CompanyListings.Where(c => c.Id == GetSaleOrder.ListingId).FirstOrDefault().CompanyName;
                                    response.CompanyMobileNo = db.ListingMobileNo.Where(c => c.ListingId == GetSaleOrder.ListingId).FirstOrDefault().MobileNo;
                                    var employee = db.Employee.Where(c => c.UserId == instance.UpdatedBy).FirstOrDefault();
                                    response.SalePersonNo = db.EmployeeContact.Where(c => c.EmployeeId == employee.Id).FirstOrDefault().ContactNo;
                                }

                                else if (CommonSpacing.RemoveSpacestoTrim(GetPaymentMode.Name) == PaymentsMode.PayOrder.ToString().Trim().ToLower()
                                    || CommonSpacing.RemoveSpacestoTrim(GetPaymentMode.Name) == PaymentsMode.Cheque.ToString().Trim().ToLower())
                                {
                                    GetSaleOrder.BankName = instance.BankName;
                                    GetSaleOrder.ChequeNo = instance.ChequeNo;
                                    GetSaleOrder.IsActive = false;
                                    GetSaleOrder.ModeId = instance.ModeId;
                                    GetSaleOrder.StatusId = Statues.Where(c => c.Name == PremiumStatuses.Process.ToString()).FirstOrDefault().Id;
                                    db.ListingPremium.Update(GetSaleOrder);
                                    PremiumManageStatus pstates = new PremiumManageStatus
                                    {
                                        PremiumId = GetSaleOrder.Id,
                                        RoleId = RoleIdDesignationId.RoleId,
                                        DesignationId = RoleIdDesignationId.DesignationId,
                                        StatusId = GetSaleOrder.StatusId,
                                        Deposited = false,
                                        UpdatedBy = instance.UpdatedBy,
                                        UpdatedDate = instance.UpdatedDate,
                                    };
                                    db.PremiumManageStatus.Add(pstates);
                                    db.SaveChanges();
                                    transaction.Commit();
                                    response.InvoiceNo = GetSaleOrder.Id;
                                    response.BankName = GetSaleOrder.BankName;
                                    response.ChequeNo = GetSaleOrder.ChequeNo;
                                    response.AssignedPackage = GetPackage.Name;
                                    response.PackageCost = GetSaleOrder.Price;
                                    response.PackageMonths = (int)GetPackage.Months;
                                    response.PaymentMode = GetPaymentMode.Name;
                                    response.ProcessMessage = "Package in Process For Clearence";
                                    response.ListingId = GetSaleOrder.ListingId;
                                    response.CompanyName = db.CompanyListings.Where(c => c.Id == GetSaleOrder.ListingId).FirstOrDefault().CompanyName;
                                    response.CompanyMobileNo = db.ListingMobileNo.Where(c => c.ListingId == GetSaleOrder.ListingId).FirstOrDefault().MobileNo;
                                    var employee = db.Employee.Where(c => c.UserId == instance.UpdatedBy).FirstOrDefault();
                                    response.SalePersonNo = db.EmployeeContact.Where(c => c.EmployeeId == employee.Id).FirstOrDefault().ContactNo;
                                }
                            }
                            else
                            {
                                response.ProcessMessage = "Record Not Exits";
                                transaction.Rollback();
                            }
                        }
                        catch (Exception ex)
                        {
                            response.ProcessMessage = ex.Message;
                            transaction.Rollback();
                        }
                    }
                }
                else
                {
                    response.ProcessMessage = "Package Already Exists";
                }
            }
            else
            {
                response.ProcessMessage = "Record Not Exits";
            }
            return response;
        }

        //Teller Order List On Load
        public VLoadTellerPackagesResponse GetLoadTellers(VLoadTellerPackagesRequest request)
        {
            try
            {
                request.CityAreasId = SaleExecutiveOrders_Execution.SpGetCityAreasByUserId(request.UserId);
                int fromRow = 0;
                if (request.PageNo == 1)
                {
                    fromRow = (request.PageNo - 1) * request.PageSize;
                }
                else
                {
                    fromRow = request.PageNo;
                }
                int toRow = request.PageSize;
                bool isSearchFilterSpecified = !string.IsNullOrEmpty(request.SearchString);

                Expression<Func<VLoadTellerOrdersDeposited, bool>> query = null;
                if (request.CityAreasId.Count > 0)
                {
                    query =
                    exp =>
                    (isSearchFilterSpecified &&
                    ((exp.CompanyName.Contains(request.SearchString)) || (exp.FullName.Contains(request.SearchString)) ||
                    (exp.PackageName.Contains(request.SearchString)) || (exp.ModeName.Contains(request.SearchString)) ||
                    (exp.StatusName.Contains(request.SearchString)) || (Convert.ToString(exp.Id) == Convert.ToString(request.SearchString)))
                    || !isSearchFilterSpecified) && ((!string.IsNullOrEmpty(exp.CreatedBy) && exp.Deposited == false) ||
                    (!string.IsNullOrEmpty(exp.CreatedBy) && exp.Deposited == true && CommonSpacing.RemoveSpacestoTrim(exp.ModeName) == PaymentsMode.OnlinePayment.ToString().ToLower()
                     && CommonSpacing.RemoveSpacestoTrim(exp.RoleName) != DesignationNames.CallCenter.ToString().ToLower())) ||
                    (exp.UpdatedBy == request.UserId) &&
                    request.CityAreasId.Contains(exp.CityAreaId);
                }
                else
                {
                    query =
                   exp =>
                   (isSearchFilterSpecified &&
                   ((exp.CompanyName.Contains(request.SearchString)) || (exp.FullName.Contains(request.SearchString)) ||
                   (exp.PackageName.Contains(request.SearchString)) || (exp.ModeName.Contains(request.SearchString)) ||
                   (exp.StatusName.Contains(request.SearchString)) || (Convert.ToString(exp.Id) == Convert.ToString(request.SearchString)))
                   || !isSearchFilterSpecified) && ((!string.IsNullOrEmpty(exp.CreatedBy) && exp.Deposited == false) ||
                   (!string.IsNullOrEmpty(exp.CreatedBy) && exp.Deposited == true && CommonSpacing.RemoveSpacestoTrim(exp.ModeName) == PaymentsMode.OnlinePayment.ToString().ToLower()
                     && CommonSpacing.RemoveSpacestoTrim(exp.RoleName) != DesignationNames.CallCenter.ToString().ToLower())) ||
                   (exp.UpdatedBy == request.UserId);
                }

                int rowCount = db.VLoadTellerOrdersDeposited.Count(query);
                // Server Side Pager
                IEnumerable<VLoadTellerOrdersDeposited> VLoadTellerListings = request.IsAsc
                    ? db.VLoadTellerOrdersDeposited.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList()
                    : db.VLoadTellerOrdersDeposited.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList();
                return new VLoadTellerPackagesResponse
                {
                    RowCount = rowCount,
                    VloadTellerPackages = VLoadTellerListings
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public VMTellerDeposited GetTellerOrderNotDeposit(decimal InvoiceId)
        {
            return TellerOrderExecutive.SpGetOpenDepositForDeposit(InvoiceId);
        }
        public UpdateTellerOrderResponse UpdateTellerOrder(VMTellerDeposited instance)
        {
            UpdateTellerOrderResponse response = new UpdateTellerOrderResponse();
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var GetSaleOrder = db.ListingPremium.Where(c => c.Id == instance.Id).FirstOrDefault();
                    var Statues = db.PremiumStatus.ToList();
                    var GetPaymentMode = db.PaymentModes.Where(c => c.Id == GetSaleOrder.ModeId).FirstOrDefault();
                    var GetPackage = db.VPackagePrices.Where(c => c.Id == GetSaleOrder.PackageId && c.PriceActive == true).FirstOrDefault();
                    var RoleIdDesignationId = CommonStoreProcedure.GetRoleandDesignationByUserId(instance.UpdatedBy);
                    GetSaleOrder.Deposited = true;
                    db.ListingPremium.Update(GetSaleOrder);
                    PremiumManageStatus pstates = new PremiumManageStatus
                    {
                        PremiumId = GetSaleOrder.Id,
                        RoleId = RoleIdDesignationId.RoleId,
                        DesignationId = RoleIdDesignationId.DesignationId,
                        StatusId = GetSaleOrder.StatusId,
                        Deposited = GetSaleOrder.Deposited,
                        UpdatedBy = instance.UpdatedBy,
                        UpdatedDate = instance.UpdatedDate,
                    };
                    db.PremiumManageStatus.Add(pstates);
                    db.SaveChanges();
                    transaction.Commit();
                    response.InvoiceNo = GetSaleOrder.Id;
                    response.AssignedPackage = GetPackage.Name;
                    response.PackageCost = GetSaleOrder.Price;
                    response.PackageMonths = (int)GetPackage.Months;
                    response.PaymentMode = GetPaymentMode.Name;
                    response.ProcessMessage = "Order Deposited Successfully";
                }
                catch (Exception ex)
                {
                    response.ProcessMessage = ex.Message;
                    transaction.Rollback();
                }
            }
            return response;
        }

        // Call Center Orders
        public VLoadCallCenterPackagesResponse GetLoadCallCenter(VLoadCallCenterPackagesRequest request)
        {
            try
            {
                int fromRow = 0;
                if (request.PageNo == 1)
                {
                    fromRow = (request.PageNo - 1) * request.PageSize;
                }
                else
                {
                    fromRow = request.PageNo;
                }
                int toRow = request.PageSize;
                bool isSearchFilterSpecified = !string.IsNullOrEmpty(request.SearchString);

                Expression<Func<VLoadCallCenterOrdersPackages, bool>> query = 
                exp =>
                    (isSearchFilterSpecified &&
                    ((exp.CompanyName.Contains(request.SearchString)) || (exp.FullName.Contains(request.SearchString)) ||
                    (exp.PackageName.Contains(request.SearchString)) || (exp.ModeName.Contains(request.SearchString)) ||
                    (exp.StatusName.Contains(request.SearchString)) || (Convert.ToString(exp.Id) == Convert.ToString(request.SearchString)))
                    || !isSearchFilterSpecified) && (exp.CreatedBy == request.UserId);

                int rowCount = db.VLoadCallCenterOrdersPackages.Count(query);
                // Server Side Pager
                IEnumerable<VLoadCallCenterOrdersPackages> CallCenterOrdersListings = request.IsAsc
                    ? db.VLoadCallCenterOrdersPackages.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList()
                    : db.VLoadCallCenterOrdersPackages.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList();
                return new VLoadCallCenterPackagesResponse
                {
                    RowCount = rowCount,
                    LoadCallCenterOrdersPackages = CallCenterOrdersListings
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public AddCallCenterOrderResponse AddOrdersCallCenter(VMCallCenterExCreate instance)
        {
            AddCallCenterOrderResponse response = new AddCallCenterOrderResponse();
            if (SaleExecutiveOrders_Execution.GetCountActiveListingByListingId(instance.ListingId) < 1)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var Statues = db.PremiumStatus.ToList();
                        var GetPaymentMode = db.PaymentModes.Where(c => c.Id == instance.ModeId).FirstOrDefault();
                        var GetPackage = db.VPackagePrices.Where(c => c.Id == instance.PackageId && c.PriceActive == true).FirstOrDefault();
                        var RoleIdDesignationId = CommonStoreProcedure.GetRoleandDesignationByUserId(instance.CreatedBy);
                        if (CommonSpacing.RemoveSpacestoTrim(GetPaymentMode.Name) == PaymentsMode.Cash.ToString().Trim().ToLower())
                        {
                            ListingPremium premium = new ListingPremium()
                            {
                                ListingId = instance.ListingId,
                                PackageId = instance.PackageId,
                                ModeId = instance.ModeId,
                                Price = (decimal)GetPackage.Price,
                                IsActive = false,
                                Deposited = false,
                                StatusId = Statues.Where(c => c.Name == PremiumStatuses.Pending.ToString()).FirstOrDefault().Id,
                                Discount = instance.Discount,
                                IsDiscount = instance.IsDiscount
                            };
                            if ((bool)instance.IsDiscount)
                            {
                                if (instance.DiscountType.Trim().ToLower() == "per".ToString().ToLower())
                                {
                                    premium.Discount = instance.Discount + " %";
                                    premium.Price = (decimal)GetPackage.Price - ((decimal)GetPackage.Price * Convert.ToDecimal(instance.Discount) / 100);
                                }
                                else
                                {
                                    premium.Discount = instance.Discount + " Rs";
                                    premium.Price = (decimal)GetPackage.Price - Convert.ToDecimal(instance.Discount);
                                }

                            }
                            else
                            {
                                premium.Price = (decimal)GetPackage.Price;


                            }

                            db.ListingPremium.Add(premium);
                            db.SaveChanges();
                            PremiumManageStatus pstates = new PremiumManageStatus
                            {
                                PremiumId = premium.Id,
                                RoleId = RoleIdDesignationId.RoleId,
                                DesignationId = RoleIdDesignationId.DesignationId,
                                StatusId = premium.StatusId,
                                Deposited = false,
                                CreatedBy = instance.CreatedBy,
                                CreatedDate = instance.CreatedDate,
                            };
                            db.PremiumManageStatus.Add(pstates);
                            var Listing = db.CompanyListings.Where(c => c.Id == premium.ListingId).FirstOrDefault();
                            Listing.ListingTypeId = 2; //Premium
                            db.CompanyListings.Update(Listing);
                            db.SaveChanges();
                            transaction.Commit();
                            response.InvoiceNo = premium.Id;
                            response.AssignedPackage = GetPackage.Name;
                            if ((bool)instance.IsDiscount)
                            {
                                response.PackageCost = (decimal)GetPackage.Price - Convert.ToDecimal(instance.Discount);
                            }
                            else
                            {
                                response.PackageCost = (decimal)GetPackage.Price;
                            }

                            //response.PackageCost = (decimal)GetPackage.Price;
                            response.PackageMonths = (int)GetPackage.Months;
                            response.PaymentMode = GetPaymentMode.Name;
                            response.ProcessMessage = "Package Assigned But in Process for Cash Collection";
                            response.ListingId = instance.ListingId;
                            response.CompanyName = db.CompanyListings.Where(c => c.Id == instance.ListingId).FirstOrDefault().CompanyName;
                            response.CompanyMobileNo = db.ListingMobileNo.Where(c => c.ListingId == instance.ListingId).FirstOrDefault().MobileNo;
                            var employee = db.Employee.Where(c => c.UserId == instance.CreatedBy).FirstOrDefault();
                            response.SalePersonNo = db.EmployeeContact.Where(c => c.EmployeeId == employee.Id).FirstOrDefault().ContactNo;
                        }

                        else if (CommonSpacing.RemoveSpacestoTrim(GetPaymentMode.Name) == PaymentsMode.OnlinePayment.ToString().Trim().ToLower())
                        {
                            ListingPremium premium = new ListingPremium()
                            {
                                AccountNo = instance.AccountNo,
                                BankName = instance.BankName,
                                ListingId = instance.ListingId,
                                PackageId = instance.PackageId,
                                ModeId = instance.ModeId,
                                Price = (decimal)GetPackage.Price,
                                IsActive = false,
                                Deposited = true,
                                StatusId = Statues.Where(c => c.Name == PremiumStatuses.Process.ToString()).FirstOrDefault().Id,
                                Discount = instance.Discount,
                                IsDiscount = instance.IsDiscount
                            };
                            if ((bool)instance.IsDiscount)
                            {
                                if (instance.DiscountType.Trim().ToLower() == "per".ToString().ToLower())
                                {
                                    premium.Discount = instance.Discount + " %";
                                    premium.Price = (decimal)GetPackage.Price - ((decimal)GetPackage.Price * Convert.ToDecimal(instance.Discount) / 100);
                                }
                                else
                                {
                                    premium.Discount = instance.Discount + " Rs";
                                    premium.Price = (decimal)GetPackage.Price - Convert.ToDecimal(instance.Discount);
                                }

                            }
                            else
                            {
                                premium.Price = (decimal)GetPackage.Price;


                            }
                            db.ListingPremium.Add(premium);
                            db.SaveChanges();
                            PremiumManageStatus pstates = new PremiumManageStatus
                            {
                                PremiumId = premium.Id,
                                RoleId = RoleIdDesignationId.RoleId,
                                DesignationId = RoleIdDesignationId.DesignationId,
                                StatusId = premium.StatusId,
                                Deposited = true,
                                CreatedBy = instance.CreatedBy,
                                CreatedDate = instance.CreatedDate,
                            };
                            db.PremiumManageStatus.Add(pstates);
                            db.SaveChanges();
                            transaction.Commit();
                            response.InvoiceNo = premium.Id;
                            response.BankName = instance.BankName;
                            response.AccountNo = instance.AccountNo;
                            response.AssignedPackage = GetPackage.Name;
                            if ((bool)instance.IsDiscount)
                            {
                                response.PackageCost = (decimal)GetPackage.Price - Convert.ToDecimal(instance.Discount);
                            }
                            else
                            {
                                response.PackageCost = (decimal)GetPackage.Price;
                            }

                            //response.PackageCost = (decimal)GetPackage.Price;
                            response.PackageMonths = (int)GetPackage.Months;
                            response.PaymentMode = GetPaymentMode.Name;
                            response.ProcessMessage = "Package Assigned But in Process For Clearence";
                            response.ListingId = instance.ListingId;
                            response.CompanyName = db.CompanyListings.Where(c => c.Id == instance.ListingId).FirstOrDefault().CompanyName;
                            response.CompanyMobileNo = db.ListingMobileNo.Where(c => c.ListingId == instance.ListingId).FirstOrDefault().MobileNo;
                            var employee = db.Employee.Where(c => c.UserId == instance.CreatedBy).FirstOrDefault();
                            response.SalePersonNo = db.EmployeeContact.Where(c => c.EmployeeId == employee.Id).FirstOrDefault().ContactNo;
                        }

                        else if (CommonSpacing.RemoveSpacestoTrim(GetPaymentMode.Name) == PaymentsMode.PayOrder.ToString().Trim().ToLower()
                            || CommonSpacing.RemoveSpacestoTrim(GetPaymentMode.Name) == PaymentsMode.Cheque.ToString().Trim().ToLower())
                        {
                            ListingPremium premium = new ListingPremium()
                            {
                                BankName = instance.BankName,
                                ChequeNo = instance.ChequeNo,
                                ListingId = instance.ListingId,
                                PackageId = instance.PackageId,
                                ModeId = instance.ModeId,
                                Price = (decimal)GetPackage.Price,
                                IsActive = false,
                                Deposited = false,
                                StatusId = Statues.Where(c => c.Name == PremiumStatuses.Pending.ToString()).FirstOrDefault().Id,
                                Discount = instance.Discount,
                                IsDiscount = instance.IsDiscount
                            };
                            if ((bool)instance.IsDiscount)
                            {
                               if (instance.DiscountType.Trim().ToLower() == "per".ToString().ToLower())
                                {
                                    premium.Discount = instance.Discount + " %";
                                    premium.Price = (decimal)GetPackage.Price - ((decimal)GetPackage.Price * Convert.ToDecimal(instance.Discount) / 100);
                                }
                                else
                                {
                                    premium.Discount = instance.Discount + " Rs";
                                    premium.Price = (decimal)GetPackage.Price -  Convert.ToDecimal(instance.Discount);
                                }
                            }
                            else
                            {
                                premium.Price = (decimal)GetPackage.Price;
                            }
                            db.ListingPremium.Add(premium);
                            db.SaveChanges();
                            PremiumManageStatus pstates = new PremiumManageStatus
                            {
                                PremiumId = premium.Id,
                                RoleId = RoleIdDesignationId.RoleId,
                                DesignationId = RoleIdDesignationId.DesignationId,
                                StatusId = premium.StatusId,
                                Deposited = false,
                                CreatedBy = instance.CreatedBy,
                                CreatedDate = instance.CreatedDate,
                            };
                            db.PremiumManageStatus.Add(pstates);
                            db.SaveChanges();
                            transaction.Commit();
                            response.InvoiceNo = premium.Id;
                            response.BankName = instance.BankName;
                            response.ChequeNo = instance.ChequeNo;
                            response.AssignedPackage = GetPackage.Name;
                            if ((bool)instance.IsDiscount)
                            {
                                response.PackageCost = (decimal)GetPackage.Price - Convert.ToDecimal(instance.Discount);
                            }
                            else
                            {
                                response.PackageCost = (decimal)GetPackage.Price;
                            }

                            // response.PackageCost = (decimal)GetPackage.Price;
                            response.PackageMonths = (int)GetPackage.Months;
                            response.PaymentMode = GetPaymentMode.Name;
                            response.ProcessMessage = "Package Assigned But in Process for Collection";
                            response.ListingId = instance.ListingId;
                            response.CompanyName = db.CompanyListings.Where(c => c.Id == instance.ListingId).FirstOrDefault().CompanyName;
                            response.CompanyMobileNo = db.ListingMobileNo.Where(c => c.ListingId == instance.ListingId).FirstOrDefault().MobileNo;
                            var employee = db.Employee.Where(c => c.UserId == instance.CreatedBy).FirstOrDefault();
                            response.SalePersonNo = db.EmployeeContact.Where(c => c.EmployeeId == employee.Id).FirstOrDefault().ContactNo;
                        }
                    }
                    catch (Exception ex)
                    {
                        response.ProcessMessage = ex.Message;
                        transaction.Rollback();
                    }
                }
            }
            else
            {
                response.ProcessMessage = "Package Already Exists";
            }
            return response;
        }
        public CallCenterOrderReportResponse GetCCOrderReport(decimal InvoiceId)
        {
            CallCenterOrderReportResponse response = new CallCenterOrderReportResponse();
            var GetListingPremium = db.VLoadCallCenterOrdersPackages.Where(c => c.Id == InvoiceId && c.CreatedDate != null).FirstOrDefault();
            if (GetListingPremium != null)
            {
                response.InvoiceNo = GetListingPremium.Id;
                response.BankName = GetListingPremium.BankName;
                response.AccountNo = GetListingPremium.Account_No;
                response.ChequeNo = GetListingPremium.ChequeNo;
                response.PaymentMode = CommonSpacing.RemoveSpacestoTrim(GetListingPremium.ModeName);
                response.BusinessPersonName = GetListingPremium.FullName;
                response.ListedAddress = GetListingPremium.ListedAddress;
                response.CreatedDate = CommonDateFormat.GetDateInMonthString((DateTime)GetListingPremium.CreatedDate);
                response.OrderTracking = SaleExecutiveOrders_Execution.SpGetSeOrderTrackings(GetListingPremium.Id);
                if (response.OrderTracking.Count() > 0)
                {
                    foreach (var item in response.OrderTracking)
                    {
                        if (item.CreatedDate != null)
                        {
                            item.CreatedDateVm = CommonDateFormat.GetDateInMonthString((DateTime)item.CreatedDate);
                        }
                        else
                        {
                            item.UpdatedDateVm = CommonDateFormat.GetDateInMonthString((DateTime)item.UpdatedDate);
                        }
                    }
                }
                response.EmailPhone = SaleExecutiveOrders_Execution.SpGetEmailAndPhoneByOrderIds(GetListingPremium.Id);
                response.OrderDetails = SaleExecutiveOrders_Execution.SpGetSeOrderDetailByOrderIds(GetListingPremium.Id);
            }
            return response;
        }

        // Order Transfer To Zone Manager
        public VLoadZoneManagerTransferResponse GetLoadZoneTransferOrders(VLoadZoneManagerTransferRequest request)
        {
            try
            {
                request.CityAreasId = ZoneManagerStoreProcedures.SpGetCityAreasByZoneManagerUserId(request.UserId);
                int fromRow = 0;
                if (request.PageNo == 1)
                {
                    fromRow = (request.PageNo - 1) * request.PageSize;
                }
                else
                {
                    fromRow = request.PageNo;
                }
                int toRow = request.PageSize;
                bool isSearchFilterSpecified = !string.IsNullOrEmpty(request.SearchString);

                Expression<Func<VLoadZoneManagerOrdersTransfer, bool>> query = null;

                query =
                    exp =>
                    (isSearchFilterSpecified &&
                    ((exp.CompanyName.Contains(request.SearchString)) || (exp.FullName.Contains(request.SearchString)) ||
                    (exp.PackageName.Contains(request.SearchString)) || (exp.ModeName.Contains(request.SearchString)) ||
                    (exp.StatusName.Contains(request.SearchString)) || (Convert.ToString(exp.Id) == Convert.ToString(request.SearchString)))
                    || !isSearchFilterSpecified) && exp.StatusName != PremiumStatuses.Rejected.ToString().ToLower() &&
                    ((exp.AssignedFrom == request.UserId ||
                    request.CityAreasId.Contains(exp.CityAreaId)) && exp.Deposited == false);

                int rowCount = db.VLoadZoneManagerOrdersTransfer.Count(query);
                // Server Side Pager
                IEnumerable<VLoadZoneManagerOrdersTransfer> VLoadZonesManagerListings = request.IsAsc
                    ? db.VLoadZoneManagerOrdersTransfer.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList()
                    : db.VLoadZoneManagerOrdersTransfer.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList();
                return new VLoadZoneManagerTransferResponse
                {
                    RowCount = rowCount,
                    vLoadZoneManagers = VLoadZonesManagerListings
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public List<SpGetEmployeeByZoneManager> GetEmployeesByZoneManagerId(string ManagerId)
        {
            var ManagersId = db.Employee.Where(c => c.UserId == ManagerId).FirstOrDefault();
            return ZoneManagerStoreProcedures.SpGetEmployeeByZoneManagerId(ManagersId.Id);
        }
        public VMZoneManagerTransfer GetNotAssignedOrders(decimal InvoiceId)
        {
            return ZoneManagerStoreProcedures.SpGetOpenForManagerTransfer(InvoiceId);
        }
        public UpdateAssigningOrderResponse AssigningOrders(VMZoneManagerTransfer instance)
        {
            UpdateAssigningOrderResponse response = new UpdateAssigningOrderResponse();
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var GetSaleOrder = db.ListingPremium.Where(c => c.Id == instance.Id).FirstOrDefault();
                    if (GetSaleOrder != null)
                    {
                        var Statues = db.PremiumStatus.Where(c => c.Id == GetSaleOrder.StatusId).FirstOrDefault();
                        var Transfer = db.PackageTransfer.Where(c => c.PremiumId == instance.Id).Count();
                        if (Transfer < 1 && Statues.Name.ToLower() == PremiumStatuses.Pending.ToString().ToLower())
                        {
                            PackageTransfer pt = new PackageTransfer
                            {
                                PremiumId = GetSaleOrder.Id,
                                AssignedFrom = instance.AssignedFrom,
                                AssignedTo = instance.AssignedTo,
                                AssignedDate = instance.AssignedDate,
                                Notes = instance.Notes,
                            };
                            db.PackageTransfer.Add(pt);
                            db.SaveChanges();
                            transaction.Commit();
                            response.InvoiceNo = GetSaleOrder.Id;
                            response.ProcessMessage = "Order Assigned Successfully";
                        }
                        else
                        {
                            response.ProcessMessage = "Order Not Assigned Successfully";
                        }
                    }
                    else
                    {
                        response.ProcessMessage = "Your request cannot be processed";
                    }

                }
                catch (Exception ex)
                {
                    response.ProcessMessage = ex.Message;
                    transaction.Rollback();
                }
            }
            return response;
        }

        // Order For Super Admin and Admin
        public VLoadSubAdminOrdersPackagesResponse GetLoadSubAdmin(VLoadAllOrderPackagesRequest request)
        {
            try
            {
                int fromRow = 0;
                if (request.PageNo == 1)
                {
                    fromRow = (request.PageNo - 1) * request.PageSize;
                }
                else
                {
                    fromRow = request.PageNo;
                }
                int toRow = request.PageSize;
                bool isSearchFilterSpecified = !string.IsNullOrEmpty(request.SearchString);

                Expression<Func<VLoadSubAdminOrdersPackages, bool>> query =
                exp =>
                    (isSearchFilterSpecified &&
                    ((exp.CompanyName.Contains(request.SearchString)) || (exp.FullName.Contains(request.SearchString)) ||
                    (exp.PackageName.Contains(request.SearchString)) || (exp.ModeName.Contains(request.SearchString)) ||
                    (exp.StatusName.Contains(request.SearchString)) || (Convert.ToString(exp.Id) == Convert.ToString(request.SearchString)))
                    || !isSearchFilterSpecified);

                int rowCount = db.VLoadSubAdminOrdersPackages.Count(query);
                // Server Side Pager
                IEnumerable<VLoadSubAdminOrdersPackages> OrdersListings = request.IsAsc
                    ? db.VLoadSubAdminOrdersPackages.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList()
                    : db.VLoadSubAdminOrdersPackages.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList();
                return new VLoadSubAdminOrdersPackagesResponse
                {
                    RowCount = rowCount,
                    LoadSubAdminOrdersPackages = OrdersListings
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }



        public AdminSubOrderReportResponse GetSubAdminReport(decimal InvoiceId)
        {
            AdminSubOrderReportResponse response = new AdminSubOrderReportResponse();
            var GetListingPremium = db.VLoadSubAdminOrdersPackages.Where(c => c.Id == InvoiceId).FirstOrDefault();
            if (GetListingPremium != null)
            {
                response.InvoiceNo = GetListingPremium.Id;
                response.BankName = GetListingPremium.BankName;
                response.AccountNo = GetListingPremium.Account_No;
                response.ChequeNo = GetListingPremium.ChequeNo;
                response.PaymentMode = CommonSpacing.RemoveSpacestoTrim(GetListingPremium.ModeName);
                response.BusinessPersonName = GetListingPremium.FullName;
                response.ListedAddress = GetListingPremium.ListedAddress;
                response.CreatedDate = CommonDateFormat.GetDateInMonthString((DateTime)GetListingPremium.CreatedDate);
                response.OrderTracking = SaleExecutiveOrders_Execution.SpGetSeOrderTrackings(GetListingPremium.Id);
                if (response.OrderTracking.Count() > 0)
                {
                    foreach (var item in response.OrderTracking)
                    {
                        if (item.CreatedDate != null)
                        {
                            item.CreatedDateVm = CommonDateFormat.GetDateInMonthString((DateTime)item.CreatedDate);
                        }
                        else
                        {
                            item.UpdatedDateVm = CommonDateFormat.GetDateInMonthString((DateTime)item.UpdatedDate);
                        }
                    }
                }
                response.EmailPhone = SaleExecutiveOrders_Execution.SpGetEmailAndPhoneByOrderIds(GetListingPremium.Id);
                response.OrderDetails = SaleExecutiveOrders_Execution.SpGetSeOrderDetailByOrderIds(GetListingPremium.Id);
            }
            return response;
        }

        public VMSubAdminOrdersUpdate GetSubAdminOrderById(decimal InvoiceId)
        {
            return AdminStoreProcdures.SubAdminOrdersUpdate(InvoiceId);
        }
        public UpdateSubOrderResponse UpdateSubOrders(SubAdminOrders instance)
        {
            UpdateSubOrderResponse response = new UpdateSubOrderResponse();
            var GetSaleOrder = db.ListingPremium.Where(c => c.Id == instance.Id).FirstOrDefault();
            if (GetSaleOrder != null)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var Statues = db.PremiumStatus.ToList();
                        var GetPackage = db.VPackagePrices.Where(c => c.Id == GetSaleOrder.PackageId && c.PriceActive == true).FirstOrDefault();
                        var RoleIdDesignationId = CommonStoreProcedure.GetRoleandDesignationByUserId(instance.UpdatedBy);
                        if (instance.StatusName == "Approved")
                        {
                            GetSaleOrder.IsActive = true;
                            GetSaleOrder.ListingFrom = DateTime.Now;
                            GetSaleOrder.ListingTo = DateTime.Now.AddMonths((int)GetPackage.Months);
                            GetSaleOrder.StatusId = Statues.Where(c => c.Name == PremiumStatuses.Approved.ToString()).FirstOrDefault().Id;
                            db.ListingPremium.Update(GetSaleOrder);
                            PremiumManageStatus pstates = new PremiumManageStatus
                            {
                                PremiumId = GetSaleOrder.Id,
                                RoleId = RoleIdDesignationId.RoleId,
                                DesignationId = RoleIdDesignationId.DesignationId,
                                StatusId = GetSaleOrder.StatusId,
                                Deposited = GetSaleOrder.Deposited,
                                UpdatedBy = instance.UpdatedBy,
                                UpdatedDate = instance.UpdatedDate,
                            };
                            db.PremiumManageStatus.Add(pstates);
                            var Listing = db.CompanyListings.Where(c => c.Id == GetSaleOrder.ListingId).FirstOrDefault();
                            Listing.ListingTypeId = 2; //Premium
                            db.CompanyListings.Update(Listing);
                            db.SaveChanges();
                            transaction.Commit();
                            response.InvoiceNo = GetSaleOrder.Id;
                            response.ProcessMessage = "Package Activated Successfully";
                            response.CompanyName = db.CompanyListings.Where(c => c.Id == GetSaleOrder.ListingId).FirstOrDefault().CompanyName;
                            response.CompanyMobileNo = db.ListingMobileNo.Where(c => c.ListingId == GetSaleOrder.ListingId).FirstOrDefault().MobileNo;
                        }
                        else if (instance.StatusName == "Rejected")
                        {
                            GetSaleOrder.IsActive = false;
                            GetSaleOrder.StatusId = Statues.Where(c => c.Name == PremiumStatuses.Rejected.ToString()).FirstOrDefault().Id;
                            db.ListingPremium.Update(GetSaleOrder);
                            PremiumManageStatus pstates = new PremiumManageStatus
                            {
                                PremiumId = GetSaleOrder.Id,
                                RoleId = RoleIdDesignationId.RoleId,
                                DesignationId = RoleIdDesignationId.DesignationId,
                                StatusId = GetSaleOrder.StatusId,
                                Deposited = GetSaleOrder.Deposited,
                                UpdatedBy = instance.UpdatedBy,
                                UpdatedDate = instance.UpdatedDate,
                            };
                            db.PremiumManageStatus.Add(pstates);
                            db.SaveChanges();
                            transaction.Commit();
                            response.InvoiceNo = GetSaleOrder.Id;
                            response.ProcessMessage = "Package Rejected Successfully";
                            response.CompanyName = db.CompanyListings.Where(c => c.Id == GetSaleOrder.ListingId).FirstOrDefault().CompanyName;
                            response.CompanyMobileNo = db.ListingMobileNo.Where(c => c.ListingId == GetSaleOrder.ListingId).FirstOrDefault().MobileNo;
                        }
                    }
                    catch (Exception ex)
                    {
                        response.ProcessMessage = ex.Message;
                        transaction.Rollback();
                    }
                }
            }
            else
            {
                response.ProcessMessage = "Record Not Exits";
            }
            return response;
        }

        public List<AutoUpdateExpiredOrder> AutoUpdateExpiredOrders()
        {
            return AutoJobsSp.AutoUpdateExpiredOrders();
        }
    }
}
