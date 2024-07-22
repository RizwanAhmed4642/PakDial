using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.ISearchesBehaviour
{
    public interface ISearchBehaviourRepository : IBaseRepository<SearchBehaviour, Decimal>
    {
        SearchBehaviourResultsResponse GetSearchResults(SearchBehaviourRequestModel request);
    }
}
