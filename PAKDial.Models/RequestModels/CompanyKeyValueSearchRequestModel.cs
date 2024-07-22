namespace PAKDial.Domains.RequestModels
{
    public class CompanyKeyValueSearchRequestModel : GetPagedListRequest
    {
        public decimal CityAreaId { get; set; }
    }
}
