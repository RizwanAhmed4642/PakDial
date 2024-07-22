namespace PAKDial.Domains.RequestModels
{
    public class CityRequestModel : GetPagedListRequest
    {
        public decimal? CountryId { get; set; }
        public decimal? StateId { get; set; }
    }
}
