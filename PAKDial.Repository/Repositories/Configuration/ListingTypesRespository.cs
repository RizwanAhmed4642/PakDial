using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels.Configuration;
using PAKDial.Domains.ResponseModels.Configuration;
using PAKDial.Interfaces.Repository;
using PAKDial.Interfaces.Repository.Configuration;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PAKDial.Repository.Repositories.Configuration
{
    public class ListingTypesRespository : BaseRepository<ListingTypes, decimal> ,IListingTypesRepository
    {

        #region Properties

        #endregion

        #region Ctor
        //fghfg
        public ListingTypesRespository(PAKDialSolutionsContext context):base(context)
        {

        }

        #endregion

        #region DbSet

        protected override DbSet<ListingTypes> DbSet
        {
            get
            {
                return db.ListingTypes;
            }
        }



        #endregion

        #region Repository Interface Implementation

        public decimal AddListingTypes(ListingTypes Instance)
        {
            decimal Result = 0;
            using (var Transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.ListingTypes.Add(Instance);
                    db.SaveChanges();
                    Transaction.Commit();
                    Result = Instance.Id;

                }
                catch (Exception ex)
                {
                    Transaction.Rollback();
                    return Result;
                }
                return Result;
            }

        }

        public bool CheckExistance(ListingTypes instance)
        {
            bool Result = false;
            if (instance.Id>0)
            {
               Result = DbSet.Where(c => c.Name.Trim().ToLower() == instance.Name.Trim().ToLower() && c.Id != instance.Id).Count() > 0 ? true : false;
            }
            else
            {
                Result = DbSet.Where(c => c.Name.Trim().ToLower() == instance.Name.Trim().ToLower()).Count() > 0 ? true : false;
            }
            return Result;
           
        }

        public decimal DeleteListingTypes(decimal Id)
        {
            decimal result = 0;
            try
            {

                var lObjTypeLst = db.CompanyListings.Where(c => c.ListingTypeId == Id).ToList();
                if (lObjTypeLst.Count>0)
                {
                    result = -3;
                }
                else
                {

                    ListingTypes lObjListingTypes = db.ListingTypes.Where(x => x.Id == Id).FirstOrDefault();
                    db.Entry(lObjListingTypes).State = EntityState.Deleted;
                    db.SaveChanges();
                    result = 1;

                }
            }
            catch (Exception ex)
            {
                return result;
               
            }

            return result;
        }

        public ListingTypes GetByName(string Name)
        {
            ListingTypes lObjListingTypes = new ListingTypes();
            lObjListingTypes = DbSet.Where(x => x.Name.Trim().ToLower() == Name.Trim().ToLower()).FirstOrDefault();

            return lObjListingTypes;
        }

        public ListingTypesResponse GetListingTypes(ListingTypesRequestModel request)
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
            Expression<Func<ListingTypes, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.Name.Contains(request.SearchString))) ||
                         !isSearchFilterSpecified);

                int rowCount = DbSet.Count(query);
                // Server Side Pager
                IEnumerable<ListingTypes> _ListingTypes = request.IsAsc
                    ? DbSet.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList()
                    : DbSet.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList();
                return new ListingTypesResponse
                {
                    RowCount = rowCount,
                    ListingTypes = _ListingTypes
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public decimal UpdateListingTypes(ListingTypes Instance)
        {
            decimal Result = 0;
            using (var Transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Entry(Instance).State = EntityState.Modified;
                    db.SaveChanges();
                    Transaction.Commit();
                    Result = Instance.Id;

                }
                catch (Exception ex)
                {
                    Transaction.Rollback();
                    return Result;
                }
                return Result;
            }

        }

        #endregion

    }

}
