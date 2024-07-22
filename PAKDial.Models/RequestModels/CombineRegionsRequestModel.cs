using System.Collections.Generic;

namespace PAKDial.Domains.RequestModels
{
    public class CombineRegionsRequestModel : GetPagedListRequest
    {
        public CombineRegionsRequestModel()
        {
            CityAreaIds = new List<decimal>();
        }
        public string UserId { get; set; }
        public List<decimal> CityAreaIds { get; set; }
    }
}
