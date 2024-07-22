using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.Repository;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PAKDial.Repository.Repositories
{
    public class ListingPackagesRepository : BaseRepository<ListingPackages, decimal>, IListingPackagesRepository
    {
        public readonly IPackagePricesRepository packagePricesRepository;
        public ListingPackagesRepository(PAKDialSolutionsContext context, IPackagePricesRepository packagePricesRepository)
          : base(context)
        {
            this.packagePricesRepository = packagePricesRepository;
        }
        /// Primary database set
        protected override DbSet<ListingPackages> DbSet
        {
            get
            {
                return db.ListingPackages;
            }
        }

        public bool CheckExistance(decimal? Id,string Name)
        {
            bool Result = false;
            if(Id > 0)
            {
                Result= db.ListingPackages.Where(c => c.Name.Trim().ToLower() == Name.Trim().ToLower() && c.Id != Id).Count() > 0 ? true : false;
            }
            else
            {
                Result = db.ListingPackages.Where(c => c.Name.Trim().ToLower() == Name.Trim().ToLower()).Count() > 0 ? true : false;
            }
            return Result;
        }

        public decimal UpdatePackages(ListingPackageViewModel instance)
        {
            decimal Result = 0;
            using (var transction = db.Database.BeginTransaction())
            {
                try
                {
                    if (instance.NewPrice > 0)
                    {
                        var UpdateListingPackage = DbSet.Where(c => c.Id == instance.Id).FirstOrDefault();
                        UpdateListingPackage.Name = instance.Name;
                        UpdateListingPackage.Description = instance.Description;
                        UpdateListingPackage.Months = instance.Months;
                        UpdateListingPackage.IsActive = instance.IsActive;
                        UpdateListingPackage.UpdatedBy = instance.UpdatedBy;
                        UpdateListingPackage.UpdatedDate = instance.UpdatedDate;
                        db.ListingPackages.Update(UpdateListingPackage);
                        db.SaveChanges();
                        var DisablePackage = packagePricesRepository.Find(instance.PriceId);
                        DisablePackage.IsActive = false;
                        DisablePackage.UpdatedBy = instance.UpdatedBy;
                        DisablePackage.UpdatedDate = instance.UpdatedDate;
                        db.PackagePrices.Update(DisablePackage);
                        db.SaveChanges();
                        db.PackagePrices.Add(new PackagePrices
                        {
                            Price = instance.NewPrice,
                            IsActive = true,
                            CreatedBy = instance.UpdatedBy,
                            CreatedDate = instance.UpdatedDate,
                            UpdatedBy = instance.UpdatedBy,
                            UpdatedDate = instance.UpdatedDate,
                            ListingPackageId = instance.Id
                        });
                        db.SaveChanges();
                        transction.Commit();
                        Result = 1;
                    }
                    else
                    {
                        var UpdateListingPackage = DbSet.Where(c => c.Id == instance.Id).FirstOrDefault();
                        UpdateListingPackage.Name = instance.Name;
                        UpdateListingPackage.Description = instance.Description;
                        UpdateListingPackage.Months = instance.Months;
                        UpdateListingPackage.IsActive = instance.IsActive;
                        UpdateListingPackage.UpdatedBy = instance.UpdatedBy;
                        UpdateListingPackage.UpdatedDate = instance.UpdatedDate;
                        db.ListingPackages.Update(UpdateListingPackage);
                        db.SaveChanges();
                        transction.Commit();
                        Result = 1;
                    }
                }
                catch (Exception ex)
                {
                    Result = 0;
                    transction.Rollback();
                }
            }
            return Result;
        }

        public decimal DeletePackage(decimal Id)
        {
            decimal Result = 0;
            using (var transction = db.Database.BeginTransaction())
            {
                try
                {
                    var GetAllPackagePrice = db.PackagePrices.Where(c => c.ListingPackageId == Id).ToList();
                    db.PackagePrices.RemoveRange(GetAllPackagePrice);
                    db.SaveChanges();
                    db.ListingPackages.Remove(Find(Id));
                    db.SaveChanges();
                    transction.Commit();
                    Result = 1;
                }
                catch (Exception ex)
                {
                    Result = 0;
                    transction.Rollback();
                }
            }
            return Result;
        }

        public ListingPackagesResponse GetListingPackages(ListingPackagesRequestModel request)
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
                Expression<Func<VListingPackages, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.Name.Contains(request.SearchString))) ||
                         !isSearchFilterSpecified);

                int rowCount = db.VListingPackages.Count(query);
                // Server Side Pager
                IEnumerable<VListingPackages> listingPackages = request.IsAsc
                    ? db.VListingPackages.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList()
                    : db.VListingPackages.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList();
                return new ListingPackagesResponse
                {
                    RowCount = rowCount,
                    ListingPackages = listingPackages
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public VListingPackages GetById(decimal Id)
        {
            return db.VListingPackages.Where(c => c.Id == Id).FirstOrDefault();
        }
    }
}
