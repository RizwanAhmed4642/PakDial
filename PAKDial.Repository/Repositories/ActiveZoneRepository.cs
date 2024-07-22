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
    public class ActiveZoneRepository : BaseRepository<ActiveZone, Decimal>, IActiveZoneRepository
    {
        private readonly IStateProvinceRepository stateProvinceRepository;
        private readonly ICityRepository cityRepository;
        private readonly ICityAreaRepository cityAreaRepository;
        private readonly IZonesRepository zonesRepository;
        public ActiveZoneRepository(PAKDialSolutionsContext context, IStateProvinceRepository stateProvinceRepository
            ,ICityRepository cityRepository, ICityAreaRepository cityAreaRepository,
            IZonesRepository zonesRepository)
            : base(context)
        {
            this.stateProvinceRepository = stateProvinceRepository;
            this.cityRepository = cityRepository;
            this.cityAreaRepository = cityAreaRepository;
            this.zonesRepository = zonesRepository;
        }
        /// Primary database set
        protected override DbSet<ActiveZone> DbSet
        {
            get
            {
                return db.ActiveZone;
            }
        }

        public bool CheckExistance(ActiveZone instance)
        {
            bool Results = false;
            if (instance.Id > 0)
            {
                Results = DbSet.Where(c => c.CityAreaId == instance.CityAreaId && c.Id != instance.Id)
                    .Count() > 0 ? true : false;
            }
            else
            {
                Results = DbSet.Where(c => c.CityAreaId == instance.CityAreaId)
                    .Count() > 0 ? true : false;
            }
            return Results;
        }

        public bool CheckZoneExist(decimal ZoneId)
        {
            bool Results = false;
            Results = DbSet.Where(c => c.ZoneId == ZoneId)
                    .Count() > 0 ? true : false;
            return Results;
        }

        public VMActiveZonesWrapper FindActiveZoneId(decimal id)
        {
            var ActiveZone = Find(id);
            VMActiveZonesWrapper zonesWrapper = new VMActiveZonesWrapper
            {
                ActiveZones = ActiveZone,
                GetStates = stateProvinceRepository.GetAllStatesKeyValue().ToList(),
                GetZones = zonesRepository.GetAllZonesKey().ToList(),
                GetCities = cityRepository.GetAllCityByStatesKey(ActiveZone.StateId).ToList(),
                GetAreas = cityAreaRepository.GetAllAreaByCityIdKeyValue(ActiveZone.CityId).ToList(),
            };
            return zonesWrapper;
        }

        public ActiveZonesResponse GetActiveZones(ActiveZonesRequestModel request)
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
                Expression<Func<VActiveZones, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.StateName.Contains(request.SearchString)) ||
                        (exp.CityName.Contains(request.SearchString)) || (exp.ZoneName.Contains(request.SearchString)) ||
                        (exp.CityAreaName.Contains(request.SearchString))) || !isSearchFilterSpecified);

                int rowCount = db.VActiveZones.Count(query);
                // Server Side Pager
                IEnumerable<VActiveZones> Activezones = request.IsAsc
                    ? db.VActiveZones.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList()
                    : db.VActiveZones.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList();
                return new ActiveZonesResponse
                {
                    RowCount = rowCount,
                    VActiveZones = Activezones
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
