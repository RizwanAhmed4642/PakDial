using System.Collections.Generic;

namespace PAKDial.Domains.UserEndViewModel
{
    public class ListingRatingWrapperModel
    {
        public ListingRatingWrapperModel()
        {
            ListingRating = new List<ListingRatingModel>();
            IncrementalCount = 0;
            RowCount = 0;
        }
        public List<ListingRatingModel> ListingRating { get; set; }
        public int IncrementalCount { get; set; }
        public int RowCount { get; set; }
    }
}
