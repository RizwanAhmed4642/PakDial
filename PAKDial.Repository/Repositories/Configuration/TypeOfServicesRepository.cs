using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels.Configuration;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ResponseModels.Configuration;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.Repository.Configuration;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PAKDial.Repository.Repositories.Configuration
{
  public class TypeOfServicesRepository :BaseRepository<TypeOfServices,decimal>, ITypeOfServicesRepository
    {
        #region Prop

        #endregion

        #region Ctor

        public TypeOfServicesRepository(PAKDialSolutionsContext context) : base(context)
        {

        }

        #endregion

        #region DbSet

        protected override DbSet<TypeOfServices> DbSet
        {
            get
            {
                return db.TypeOfServices;
            }
        }

        #endregion

        #region Repository Interface Implementation

        public decimal AddTypeOfServices(TypeOfServices Instance)
        {
            decimal Result = 0;
            using (var Transaction = db.Database.BeginTransaction())
            {
                try
                {

                    db.TypeOfServices.Add(Instance);
                    db.SaveChanges();
                    Transaction.Commit();
                    Result = Instance.Id;

                }
                catch (Exception)
                {

                    Transaction.Rollback();
                    Result = 0;
                }
                return Result;
            }
        }

        public bool CheckExistance(TypeOfServices instance)
        {
            bool Result = false;
            if (instance.Id>0)
            {
                Result = DbSet.Where(c => c.Id != instance.Id && c.Name == instance.Name).Count() > 0 ? true : false;
            }
            else
            {
                Result = DbSet.Where(c => c.Name == instance.Name).Count() > 0 ? true : false;
            }
            return Result;
        }

        public decimal DeleteTypeOfServices(int Id)
        {
            decimal Result = 0;
            try
            {

               var lObjLstService = db.ListingServices.Where(c => c.ServiceTypeId == Id).ToList();
                if (lObjLstService.Count>0)
                {

                    Result = -3;
                }
                else
                {
                TypeOfServices lObjTypeOfServices = DbSet.Where(c => c.Id == Id).FirstOrDefault();
                db.Entry(lObjTypeOfServices).State = EntityState.Deleted;
                db.SaveChanges();
                Result = 1;
                }
            }
            catch (Exception ex)
            {
                Result = 0;
                
            }

            return Result;
        }

        public TypeOfServices GetByName(string Name)
        {
          return   DbSet.Where(c => c.Name == Name).FirstOrDefault();
        }

        public TypeOfServicesResponse GetTypeOfServices(TypeOfServicesRequestModel request)
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
                Expression<Func<TypeOfServices, bool>> query =
                        exp =>
                            (isSearchFilterSpecified && ((exp.Name.Contains(request.SearchString))) ||
                             !isSearchFilterSpecified);

                int rowCount = DbSet.Count(query);
                // Server Side Pager
                IEnumerable<TypeOfServices> _TypeOfServices = request.IsAsc
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
                return new TypeOfServicesResponse
                {
                    RowCount = rowCount,
                    TypeOfServices = _TypeOfServices
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public GetServicesTypeResponse GetTypeofServicesList(TypeOfServicesRequestModel request)
        {
            try
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;

                Expression<Func<TypeOfServices, bool>> query = exp =>
                       (string.IsNullOrEmpty(request.SearchString) || (exp.Name.Contains(request.SearchString.Trim().ToLower()))
                          && exp.IsActive == true);

                int rowCount = DbSet.Count(query);
                // Server Side Pager
                IEnumerable<VMGenericKeyValuePair<decimal>> TypeofServices =
                      DbSet.Where(query)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList().Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.Name });

                return new GetServicesTypeResponse
                {
                    PageNo = fromRow,
                    RowCount = rowCount,
                    ServicesType = TypeofServices
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public decimal UpdateTypeOfServices(TypeOfServices Instance)
        {
            decimal Result = 0;
            using (var Transaction = db.Database.BeginTransaction())
            {
                try
                {

                    db.Entry(Instance).State = EntityState.Modified;
                    db.SaveChanges();
                    Transaction.Commit();
                    Result =Instance.Id;

                }
                catch (Exception)
                {

                    Transaction.Rollback();
                    Result = 0;
                }
                return Result;
            }
        }
        #endregion
    }
}
