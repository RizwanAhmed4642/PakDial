using PAKDial.Domains.SqlViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class SearchBehaviourResultsResponse
    {
        public int RowCount { get; set; }

        public IEnumerable<VSearchBehaviourResults> SearchResults { get; set; }
    }
}
