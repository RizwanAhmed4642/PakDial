using Microsoft.EntityFrameworkCore;
using PAKDial.Interfaces.Repository;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PAKDial.Repository.Repositories
{
    public class RouteControlsRepository : BaseRepository<RouteControls, decimal>, IRouteControlsRepository
    {
        public RouteControlsRepository(PAKDialSolutionsContext context)
            : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<RouteControls> DbSet
        {
            get
            {
                return db.RouteControls;
            }
        }
        public bool CheckExistance(RouteControls instance)
        {
            bool Results = false;
            if (instance.Id > 0)
            {
                Results = DbSet.Where(c => c.Controller.ToLower().Trim() == instance.Controller.ToLower().Trim()
                && c.Action.ToLower().Trim() == instance.Action.ToLower().Trim() && c.Id != instance.Id)
                    .Count() > 0 ? true : false;
            }
            else
            {
                Results = DbSet.Where(c => c.Controller.ToLower().Trim() == instance.Controller.ToLower().Trim()
                && c.Action.ToLower().Trim() == instance.Action.ToLower().Trim())
                   .Count() > 0 ? true : false;
            }
            return Results;
        }

        public bool CheckPermissionByRouteId(decimal Id)
        {
            bool Results = false;
            var CheckRoleBased = db.RoleBasedPermission.Where(c => c.RouteControlId == Id).FirstOrDefault();
            var CheckUserBased = db.UserBasedPermission.Where(c => c.RouteControlId == Id).FirstOrDefault();
            if(CheckRoleBased != null || CheckUserBased != null)
            {
                Results = true;
            }
            return Results;
        }

        public List<VRouteControl> GetRouteByModuleId(decimal ModuleId)
        {
            return db.VRouteControl.Where(c => c.ModuleId == ModuleId).ToList();
        }

        public RouteControlsResponse GetRouteControls(RouteControlsRequestModel request)
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

                Expression<Func<VRouteControl, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.Controller.Contains(request.SearchString)) || 
                        (exp.Action.Contains(request.SearchString)) 
                        || (exp.Module.Contains(request.SearchString))) || !isSearchFilterSpecified);

                int rowCount = db.VRouteControl.Count(query);
                // Server Side Pager
                IEnumerable<VRouteControl> RouteControl = request.IsAsc
                    ? db.VRouteControl.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : db.VRouteControl.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();

                return new RouteControlsResponse
                {
                    RowCount = rowCount,
                    RouteControl = RouteControl
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
