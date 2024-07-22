using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels.Configuration;
using PAKDial.Domains.ResponseModels.Configuration;
using PAKDial.Interfaces.Repository.Configuration;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PAKDial.Repository.Repositories.Configuration
{
    public  class VerificationTypesRepository :BaseRepository<VerificationTypes,decimal>, IVerificationTypesRepository
    {
        #region Prop

        #endregion

        #region Ctor
        public VerificationTypesRepository(PAKDialSolutionsContext context):base(context)
        {

        }


        #endregion

        #region DBSet

        
        protected override DbSet<VerificationTypes> DbSet {
            get
            {
                return db.VerificationTypes;
            }
        }

        #endregion

        #region Repository Interface Implementation

        
        public decimal AddVerificationTypes(VerificationTypes Instance)
        {
            decimal Result = 0;
            using (var Transation = db.Database.BeginTransaction())
            {
                try
                {

                    db.VerificationTypes.Add(Instance);
                    db.SaveChanges();
                    Transation.Commit();
                    Result = Instance.Id;

                }
                catch (Exception)
                {
                    Transation.Rollback();
                    Result = 0;
                }
            }
            return Result;
        }

        public bool CheckExistance(VerificationTypes instance)
        {
            bool Result = false;
            if (instance.Id > 0)
            {
                Result = DbSet.Where(c => c.Id != instance.Id && c.Name == instance.Name).Count() > 0 ? true : false;
            }
            else
            {
                Result = DbSet.Where(c => c.Name == instance.Name).Count() > 0 ? true : false;
            }
            return Result;
        }

        public decimal DeleteVerificationTypes(int Id)
        {
            decimal Result = 0;
            try
            {
                 var lobjVeriLisiting = db.VerifiedListing.Where(c => c.VerificationId == Id).ToList();
                 if (lobjVeriLisiting.Count>0)
                {
                    Result = -3;
                }
                else { 
                VerificationTypes lObjVerificationTypes = db.VerificationTypes.Where(c => c.Id == Id).FirstOrDefault();
                db.Entry(lObjVerificationTypes).State = EntityState.Deleted;
                db.SaveChanges();
               
                Result = 1;
                }
            }
            catch (Exception ex)
            {
                return Result;
            }


            return Result;
        }

        public VerificationTypes GetByName(string Name)
        {
            return db.VerificationTypes.Where(c => c.Name == Name).FirstOrDefault();

        }

        public VerificationTypesResponse GetVerificationTypes(VerificationTypesRequestModel request)
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
                Expression<Func<VerificationTypes, bool>> query =
                        exp =>
                            (isSearchFilterSpecified && ((exp.Name.Contains(request.SearchString))) ||
                             !isSearchFilterSpecified);

                int rowCount = DbSet.Count(query);
                // Server Side Pager
                IEnumerable<VerificationTypes> _VerificationTypes = request.IsAsc
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
                return new VerificationTypesResponse
                {
                    RowCount = rowCount,
                    VerificationTypes = _VerificationTypes
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public decimal UpdateVerificationTypes(VerificationTypes Instance)
        {
            decimal Result = 0;
            using (var Transation = db.Database.BeginTransaction())
            {
                try
                {

                    db.Entry(Instance).State = EntityState.Modified;
                    db.SaveChanges();
                    Transation.Commit();
                    Result = Instance.Id;

                }
                catch (Exception ex)
                {
                    Transation.Rollback();
                    Result = 0;
                }
            }
            return Result;
        }
        #endregion
    }
}
