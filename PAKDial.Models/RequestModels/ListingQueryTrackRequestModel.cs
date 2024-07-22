namespace PAKDial.Domains.RequestModels
{
    public class ListingQueryTrackRequestModel : GetPagedListRequest
    {
        public decimal ListingId { get; set; }
    }
}
