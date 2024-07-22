using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class CityArea
    {
        public CityArea()
        {
            EmployeeAddress = new HashSet<EmployeeAddress>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string GoogleLoc { get; set; }
        public string AreaLat { get; set; }
        public string AreaLong { get; set; }
        public int? AreaPopular { get; set; }
        public int? AreaStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public decimal? CityId { get; set; }
        public decimal CountryId { get; set; }
        public decimal? StateId { get; set; }

        public City City { get; set; }
        public ICollection<EmployeeAddress> EmployeeAddress { get; set; }
    }
}
