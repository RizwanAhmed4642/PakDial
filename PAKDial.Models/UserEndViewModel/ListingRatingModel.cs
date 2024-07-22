using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.UserEndViewModel
{
    public class ListingRatingModel
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string RatingDesc { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal ListingId { get; set; }
    }
}
