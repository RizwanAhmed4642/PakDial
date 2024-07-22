namespace PAKDial.Domains.RequestModels
{
    public class AssignListingPackageRequestModel : GetPagedListRequest
    {
        public decimal? ListingId { get; set; }
        public decimal? CustomerId { get; set; }
    }
}
