using System.Collections.Generic;

namespace PAKDial.Domains.RequestModels
{
    public class VLoadTellerPackagesRequest : GetPagedListRequest
    {
        public VLoadTellerPackagesRequest()
        {
            CityAreasId = new List<decimal>();
        }
        public string UserId { get; set; }
        public List<decimal> CityAreasId { get; set; }
    }
}
