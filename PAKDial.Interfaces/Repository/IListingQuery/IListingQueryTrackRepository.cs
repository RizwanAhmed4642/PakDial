using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.IListingQuery
{
    public interface IListingQueryTrackRepository : IBaseRepository<ListingQueryTrack, Decimal>
    {
        ListingQueryTrackResponse GetListingQueryTrack(ListingQueryTrackRequestModel request);
    }
}
