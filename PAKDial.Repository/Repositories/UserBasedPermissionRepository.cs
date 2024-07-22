using Microsoft.EntityFrameworkCore;
using PAKDial.Interfaces.Repository;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAKDial.Repository.Repositories
{
    public class UserBasedPermissionRepository : BaseRepository<UserBasedPermission,decimal>, IUserBasedPermissionRepository
    {
        public UserBasedPermissionRepository(PAKDialSolutionsContext context)
          : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<UserBasedPermission> DbSet
        {
            get
            {
                return db.UserBasedPermission;
            }
        }

        public void DeleteDuplicatePermission(string UserId, string RoleId)
        {
            db.Database.ExecuteSqlCommand("dbo.SpDeleteDuplicatePermission @p0,@p1",parameters:new[] {RoleId,UserId});
        }

        public List<VUserPermissions> GetUserPermissionById(string UserId)
        {
           return db.VUserPermissions.Where(c => c.UserId == UserId).ToList();
        }

        public List<UserBasedPermission> GetAssignedPermissions(string UserId)
        {
            return DbSet.Where(c => c.UserId == UserId).ToList();
        }

        public int AddUpdatePermissions(List<UserBasedPermission> instance)
        {
            var results = 0;
            string UserId = instance.Select(c => c.UserId).FirstOrDefault();
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var GetPermissions = DbSet.Where(c => c.UserId == UserId).ToList();
                    if (GetPermissions != null && GetPermissions.Count() > 0)
                    {
                        DbSet.RemoveRange(GetPermissions);
                        db.SaveChanges();
                    }
                    if (instance.Where(c => c.RouteControlId > 0).FirstOrDefault() != null)
                    {
                        DbSet.AddRange(instance);
                        db.SaveChanges();
                    }
                    transaction.Commit();
                    results = 1;
                }
                catch
                {
                    transaction.Rollback();
                    results = 0;
                }
            }
            return results;
        }
    }
}
