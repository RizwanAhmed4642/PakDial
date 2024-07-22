using Microsoft.EntityFrameworkCore;
using PAKDial.Interfaces.Repository;
using PAKDial.Domains.DomainModels;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Text;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.RequestModels;
using System.Linq.Expressions;
using System.Linq;

namespace PAKDial.Repository.Repositories
{
    public class SystemRoleRepository : BaseRepository<AspNetRoles, string>, ISystemRoleRepository
    {
        public SystemRoleRepository(PAKDialSolutionsContext context)
         : base(context)
        {

        }

        public string GetRoleByUserId(string UserId)
        {
            return db.AspNetUserRoles.Where(c => c.UserId == UserId).FirstOrDefault().RoleId;
        }
        public RoleResponse GetRoles(RoleRequestModel request)
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
                Expression<Func<AspNetRoles, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.Name.Contains(request.SearchString))) ||
                         !isSearchFilterSpecified);

                int rowCount = DbSet.Count(query);
                // Server Side Pager
                IEnumerable<AspNetRoles> roles = request.IsAsc
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
                return new RoleResponse
                {
                    RowCount = rowCount,
                    AspRoles = roles
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        /// Primary database set
        protected override DbSet<AspNetRoles> DbSet
        {
            get
            {
                return db.AspNetRoles;
            }
        }
    }
}
