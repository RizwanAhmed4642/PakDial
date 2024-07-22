using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.SqlViewModels
{
    public class VActiveZones
    {
        public decimal Id { get; set; }
        public decimal StateId { get; set; }
        public string StateName { get; set; }
        public decimal CityId { get; set; }
        public string CityName { get; set; }
        public decimal ZoneId { get; set; }
        public string ZoneName { get; set; }
        public decimal CityAreaId { get; set; }
        public string CityAreaName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
