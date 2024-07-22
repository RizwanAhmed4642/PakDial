using Microsoft.EntityFrameworkCore;
using PAKDial.Interfaces.Repository;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PAKDial.Repository.Repositories
{
    public class ModulesRepository : BaseRepository<Modules, decimal>, IModulesRepository
    {
        private readonly IRoleBasedPermissionRepository roleBasedPermissionRepository;
        public ModulesRepository(PAKDialSolutionsContext context, IRoleBasedPermissionRepository roleBasedPermissionRepository)
            : base(context)
        {
            this.roleBasedPermissionRepository = roleBasedPermissionRepository;
        }
        /// Primary database set
        protected override DbSet<Modules> DbSet
        {
            get
            {
                return db.Modules;
            }
        }
        public IEnumerable<Modules> GetAllIncludeRoutes()
        {
            return DbSet.Include("RouteControls").ToList();
        }

        public List<Modules> GetModulesNotInRoles(string RoleId)
        {
            List<Modules> modules = new List<Modules>();
            var GetAssignRoleControl = roleBasedPermissionRepository.GetRolePermissionById(RoleId).Select(c => c.RouteControlId);
            if(GetAssignRoleControl != null)
            {
                modules = DbSet.ToList();
                foreach (var item in modules)
                {
                    item.RouteControls = null;
                    item.RouteControls = db.RouteControls.Where(c => !GetAssignRoleControl.Contains(c.Id) && c.Status == true && c.ModuleId == item.Id).ToList();
                }
            }
            else
            {
                modules = DbSet.ToList();
                foreach (var item in modules)
                {
                    item.RouteControls = null;
                    item.RouteControls = db.RouteControls.Where(c => c.Status == true && c.ModuleId == item.Id).ToList();
                }
            }
            return modules;
        }

        public ModulesResponse GetModules(ModulesRequestModel request)
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

                Expression<Func<Modules, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.Name.Contains(request.SearchString))) || !isSearchFilterSpecified);

                int rowCount = DbSet.Count(query);
                // Server Side Pager
                IEnumerable<Modules> modules = request.IsAsc
                    ? DbSet.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : DbSet.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();
                return new ModulesResponse
                {
                    RowCount = rowCount,
                    Modules = modules
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public bool CheckExistance(Modules instance)
        {
            bool Results = false;
            if (instance.Id > 0)
            {
                Results = DbSet.Where(c => c.Name.ToLower().Trim() == instance.Name.ToLower().Trim() && c.Id != instance.Id)
                    .Count() > 0 ? true : false;
            }
            else
            {
                Results = DbSet.Where(c => c.Name.ToLower().Trim() == instance.Name.ToLower().Trim())
                    .Count() > 0 ? true : false;
            }
            return Results;
        }
    }
}
