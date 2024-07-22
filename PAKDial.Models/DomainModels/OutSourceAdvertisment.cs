using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class OutSourceAdvertisment
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string ImageBtn { get; set; }
        public string ImageUrl { get; set; }
        public decimal? ClickCounts { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public string MobImagePath { get; set; }
    }
}
