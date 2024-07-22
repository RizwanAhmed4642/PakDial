using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.IListingQuery
{
    public interface IListingQueryTrackService
    {
        ListingQueryTrackResponse GetListingQueryTrack(ListingQueryTrackRequestModel request);
    }
}
