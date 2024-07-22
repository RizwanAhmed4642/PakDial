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
using PAKDial.Domains.SqlViewModels;

namespace PAKDial.Repository.Repositories
{
    public class DesignationRepository : BaseRepository<Designation, decimal>, IDesignationRepository
    {
        public DesignationRepository(PAKDialSolutionsContext context)
           : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<Designation> DbSet
        {
            get
            {
                return db.Designation;
            }
        }

        public bool CheckExistance(Designation designation)
        {
            bool Results = false;
            if(designation.Id > 0)
            {
                Results = DbSet.Where(c => c.Name.ToLower().Trim() == designation.Name.ToLower().Trim() && c.Id != designation.Id)
                    .Count() > 0 ? true : false;
            }
            else
            {
                Results = DbSet.Where(c => c.Name.ToLower().Trim() == designation.Name.ToLower().Trim())
                    .Count() > 0 ? true : false;
            }
            return Results;
        }

        public DesignationResponse GetDesignations(DesignationRequestModel request)
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
                Expression<Func<VDesignation, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.Name.Contains(request.SearchString))) ||
                         !isSearchFilterSpecified);

                int rowCount = db.VDesignation.Count(query);
                // Server Side Pager
                IEnumerable<VDesignation> designations = request.IsAsc
                    ? db.VDesignation.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : db.VDesignation.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();
                return new DesignationResponse
                {
                    RowCount = rowCount,
                    designations = designations
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
