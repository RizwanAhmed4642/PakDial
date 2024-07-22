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
public class SocialMediaModesRepository:BaseRepository<SocialMediaModes,decimal>, ISocialMediaModesRepository
    {
        #region Properties

        #endregion
        public SocialMediaModesRepository(PAKDialSolutionsContext context):base(context)
        {

        }
        #region DbSet

        protected override DbSet<SocialMediaModes>DbSet
        {
            get
            {
                return db.SocialMediaModes;
            }
        }
        #endregion

        #region Repository Interface Implementation
        public decimal AddSocialMediaModes(SocialMediaModes Instance)
        {
            decimal Result = 0;
            using (var Transation = db.Database.BeginTransaction())
            {
                try
                {

                    db.SocialMediaModes.Add(Instance);
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

        public bool CheckExistance(SocialMediaModes instance)
        {
            bool Result = false;
             if (instance.Id>0)
            {
                Result = DbSet.Where(c => c.Id != instance.Id && c.Name == instance.Name).Count() > 0 ? true : false;
            }
             else
            {
                Result = DbSet.Where(c =>c.Name == instance.Name).Count() > 0 ? true : false;
            }
            return Result;
        }

        public decimal DeleteSocialMediaModes(int Id)
        {
            decimal Result = 0;
            try
            {
                var lObjSocialList = db.ListingSocialMedia.Where(c => c.MediaId == Id).ToList();
                if (lObjSocialList.Count > 0)
                {
                    Result = -3;
                }
                else
                {

                    SocialMediaModes lObjSocialMediaModes = db.SocialMediaModes.Where(c => c.Id == Id).FirstOrDefault();
                    db.Entry(lObjSocialMediaModes).State = EntityState.Deleted;
                    db.SaveChanges();
                    Result = 1;

                }
            }
            catch (Exception)
            {
                return Result;
            }


            return Result;
        }

        public SocialMediaModes GetByName(string Name)
        {
        return  db.SocialMediaModes.Where(c => c.Name == Name).FirstOrDefault();
        }

        public SocialMediaModesResponse GetSocialMediaModes(SocialMediaModesRequestModel request)
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
                Expression<Func<SocialMediaModes, bool>> query =
                        exp =>
                            (isSearchFilterSpecified && ((exp.Name.Contains(request.SearchString))) ||
                             !isSearchFilterSpecified);

                int rowCount = DbSet.Count(query);
                // Server Side Pager
                IEnumerable<SocialMediaModes> _SocialMediaModes = request.IsAsc
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
                return new SocialMediaModesResponse
                {
                    RowCount = rowCount,
                    SocialMediaModes = _SocialMediaModes
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public decimal UpdateSocialMediaModes(SocialMediaModes Instance)
        {
            decimal Result = 0;
            using (var Transation = db.Database.BeginTransaction())
            {
                try
                {
                    var getData = DbSet.Where(c => c.Id == Instance.Id).FirstOrDefault();
                    getData.Name = Instance.Name;
                    getData.UpdatedBy = Instance.UpdatedBy;
                    getData.UpdatedDate = Instance.UpdatedDate;
                    db.Entry(getData).State = EntityState.Modified;
                    db.SaveChanges();
                    Transation.Commit();
                    Result = getData.Id;

                }
                catch (Exception)
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
