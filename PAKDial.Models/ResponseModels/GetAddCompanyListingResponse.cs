using PAKDial.Domains.DomainModels;
using PAKDial.Domains.ViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class GetAddCompanyListingResponse
    {
        public GetAddCompanyListingResponse()
        {
            ListingTypes = new List<VMKeyValuePair>();
            Categories = new List<VMKeyValuePair>();
            States = new List<VMKeyValuePair>();
            SocialMediaModes = new List<SocialMediaModes>();
            CityAreaKeyValue = new List<VMGenericKeyValuePair<decimal>>();
        }
        public decimal? CityId { get; set; }
        public decimal? StateId { get; set; }
        public List<VMKeyValuePair> ListingTypes { get; set; }
        public List<VMKeyValuePair> Categories { get; set; }
        public List<VMKeyValuePair> States { get; set; }
        public List<SocialMediaModes> SocialMediaModes { get; set; }
        public List<VMGenericKeyValuePair<decimal>> CityAreaKeyValue { get; set; }
    }

}
