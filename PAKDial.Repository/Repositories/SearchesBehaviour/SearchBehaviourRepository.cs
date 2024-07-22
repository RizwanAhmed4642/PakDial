using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Interfaces.Repository.ISearchesBehaviour;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PAKDial.Repository.Repositories.SearchesBehaviour
{
    public class SearchBehaviourRepository : BaseRepository<SearchBehaviour, Decimal>, ISearchBehaviourRepository
    {
        public SearchBehaviourRepository(PAKDialSolutionsContext context)
            : base(context)
        {
        }
        /// Primary database set
        protected override DbSet<SearchBehaviour> DbSet
        {
            get
            {
                return db.SearchBehaviour;
            }
        }

        public SearchBehaviourResultsResponse GetSearchResults(SearchBehaviourRequestModel request)
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

                Expression<Func<VSearchBehaviourResults, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.SearchResults.Contains(request.SearchString)) ||
                        (exp.LocationSearch.Contains(request.SearchString)) ||
                        (exp.AreaSearch.Contains(request.SearchString))) || !isSearchFilterSpecified);

                int rowCount = db.VSearchBehaviourResults.Count(query);
                // Server Side Pager
                IEnumerable<VSearchBehaviourResults> behaviourResults = request.IsAsc
                    ? db.VSearchBehaviourResults.Where(query)
                        .OrderBy(x => x.TotalSearches)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList()
                    : db.VSearchBehaviourResults.Where(query)
                        .OrderByDescending(x => x.TotalSearches)
                        .Skip(fromRow)
                        .Take(toRow)
                        .AsNoTracking()
                        .ToList();
              
                return new SearchBehaviourResultsResponse
                {
                    RowCount = rowCount,
                    SearchResults = behaviourResults
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
