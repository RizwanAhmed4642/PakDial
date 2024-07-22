using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class ListingQueryCycle
    {
        public decimal Id { get; set; }
        public decimal ListingId { get; set; }
        public decimal SubCatId { get; set; }
        public decimal AreaId { get; set; }
        public decimal? CityId { get; set; }
    }
}
