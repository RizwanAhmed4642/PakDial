using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Interfaces.PakDialServices.ISearchesBehaviour;
using PAKDial.Interfaces.Repository.ISearchesBehaviour;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.SearchesBehaviour
{
    public class SearchBehaviourService : ISearchBehaviourService
    {
        private readonly ISearchBehaviourRepository _searchBehaviourRepository;
        public SearchBehaviourService(ISearchBehaviourRepository searchBehaviourRepository)
        {
            _searchBehaviourRepository = searchBehaviourRepository;
        }
        public SearchBehaviourResultsResponse GetSearchResults(SearchBehaviourRequestModel request)
        {
            return _searchBehaviourRepository.GetSearchResults(request);
        }
    }
}
