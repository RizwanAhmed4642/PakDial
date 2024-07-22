namespace PAKDial.Domains.RequestModels
{
    public class SubCategoryRequestModel : GetPagedListRequest
    {
        public decimal? SubCatId { get; set; }
        public decimal? MainCatId { get; set; }

    }
}
