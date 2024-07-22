namespace PAKDial.Domains.RequestModels
{
    public class GetCustomerListRequestModel : GetPagedListRequest
    {
        public bool IsDefault { get; set; }
    }
}
