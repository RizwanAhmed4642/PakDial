namespace PAKDial.Domains.RequestModels
{
    public class ZonesRequestModel : GetPagedListRequest
    {
        public decimal CityId { get; set; }
    }
}
