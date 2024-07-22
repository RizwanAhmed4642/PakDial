using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Interfaces.PakDialServices.IListingQuery;
using PAKDial.Interfaces.Repository.IListingQuery;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.QueryTrack
{
    public class ListingQueryTrackService : IListingQueryTrackService
    {
        private readonly IListingQueryTrackRepository _listingQueryTrackRepository;
        public ListingQueryTrackService(IListingQueryTrackRepository listingQueryTrackRepository)
        {
            _listingQueryTrackRepository = listingQueryTrackRepository;
        }
        public ListingQueryTrackResponse GetListingQueryTrack(ListingQueryTrackRequestModel request)
        {
            return _listingQueryTrackRepository.GetListingQueryTrack(request);
        }
    }
}
