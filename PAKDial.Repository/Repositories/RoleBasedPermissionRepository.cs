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
    public class RoleBasedPermissionRepository : BaseRepository<RoleBasedPermission, decimal>, IRoleBasedPermissionRepository
    {
        public RoleBasedPermissionRepository(PAKDialSolutionsContext context)
          : base(context)
        {

        }

        public int AddUpdatePermissions(List<RoleBasedPermission> permissions)
        {
            var results = 0;
            string RoleId = permissions.Select(c => c.RoleId).FirstOrDefault();
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var GetPermission = DbSet.Where(c => c.RoleId == RoleId).ToList();
                    if(GetPermission != null && GetPermission.Count() > 0)
                    {
                        db.RemoveRange(GetPermission);
                        db.SaveChanges();
                    }
                    if(permissions.Where(c=>c.RouteControlId > 0).FirstOrDefault() != null)
                    {
                        DbSet.AddRange(permissions);
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
        /// Primary database set
        protected override DbSet<RoleBasedPermission> DbSet
        {
            get
            {
                return db.RoleBasedPermission;
            }
        }

        public List<VRolePermissions> GetRolePermissionById(string RoleId)
        {
            return db.VRolePermissions.Where(c => c.RoleId == RoleId).ToList();
        }
    }
}
