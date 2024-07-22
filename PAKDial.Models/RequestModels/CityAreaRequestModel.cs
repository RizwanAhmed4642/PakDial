namespace PAKDial.Domains.RequestModels
{
    public class CityAreaRequestModel : GetPagedListRequest
    {
        public decimal? CountryId { get; set; }
        public decimal? StateId { get; set; }
        public decimal? CityId { get; set; }
    }
}
