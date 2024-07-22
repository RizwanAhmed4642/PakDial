using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.ISearchesBehaviour
{
    public interface ISearchBehaviourService
    {
        SearchBehaviourResultsResponse GetSearchResults(SearchBehaviourRequestModel request);
    }
}
