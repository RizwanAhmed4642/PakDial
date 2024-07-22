using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class ListingQueryTrack
    {
        public decimal Id { get; set; }
        public string AuditName { get; set; }
        public decimal ListingId { get; set; }
        public  DateTime CreatedDate { get; set; }
    }
}
