using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Interfaces.Repository.IListingQuery;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PAKDial.Repository.Repositories.QueryTrack
{
    public class ListingQueryTrackRepository : BaseRepository<ListingQueryTrack, Decimal>, IListingQueryTrackRepository
    {
        public ListingQueryTrackRepository(PAKDialSolutionsContext context)
            : base(context)
        {
        }
        /// Primary database set
        protected override DbSet<ListingQueryTrack> DbSet
        {
            get
            {
                return db.ListingQueryTrack;
            }
        }

        public ListingQueryTrackResponse GetListingQueryTrack(ListingQueryTrackRequestModel request)
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
                Expression<Func<ListingQueryTrack, bool>> query =
                    exp =>
                        exp.ListingId == request.ListingId;

                int rowCount = db.ListingQueryTrack.Count(query);
                // Server Side Pager
                IEnumerable<ListingQueryTrack> listingQueryTracks = request.IsAsc
                    ? db.ListingQueryTrack.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList()
                    : db.ListingQueryTrack.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList();
                return new ListingQueryTrackResponse
                {
                    RowCount = rowCount,
                    QueryTrack = listingQueryTracks
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
