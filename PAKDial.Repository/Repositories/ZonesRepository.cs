using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.Repository;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PAKDial.Repository.Repositories
{
    public class ZonesRepository : BaseRepository<Zones, Decimal>, IZonesRepository
    {
        public ZonesRepository(PAKDialSolutionsContext context)
            : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<Zones> DbSet
        {
            get
            {
                return db.Zones;
            }
        }

        public bool CheckExistance(Zones instance)
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

        public IEnumerable<VMGenericKeyValuePair<decimal>> GetAllZonesKey()
        {
            return DbSet.Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.Name }).ToList();
        }

        public GetZonesResponse GetSearchZones(ZonesRequestModel request)
        {
            try
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;

                bool isSearchFilterSpecified = !string.IsNullOrEmpty(request.SearchString);
                Expression<Func<VActiveZones, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.ZoneName.Contains(request.SearchString))) ||
                         !isSearchFilterSpecified) && exp.CityId == request.CityId;

                int rowCount = db.VActiveZones.Count(query);
                // Server Side Pager
                IEnumerable<VMGenericKeyValuePair<decimal>> ZoneList =
                      db.VActiveZones.Where(query)
                        .Skip(fromRow)
                        .Take(toRow)
                        .Select(c => new VMGenericKeyValuePair<decimal> { Id = c.ZoneId, Text = c.ZoneName }).Distinct().ToList();

                return new GetZonesResponse
                {
                    PageNo = fromRow,
                    RowCount = rowCount,
                    ZonesList = ZoneList
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public ZonesResponse GetZones(ZonesRequestModel request)
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
                //int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;
                bool isSearchFilterSpecified = !string.IsNullOrEmpty(request.SearchString);
                Expression<Func<Zones, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.Name.Contains(request.SearchString))) ||
                         !isSearchFilterSpecified);

                int rowCount = DbSet.Count(query);
                // Server Side Pager
                IEnumerable<Zones> zones = request.IsAsc
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
                return new ZonesResponse
                {
                    RowCount = rowCount,
                    Zones = zones
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
