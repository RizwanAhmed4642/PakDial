using PAKDial.Domains.DomainModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class ListingQueryTrackResponse
    {
        public int RowCount { get; set; }

        public IEnumerable<ListingQueryTrack> QueryTrack { get; set; }
    }
}
