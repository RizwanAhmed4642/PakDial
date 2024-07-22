using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class EmployeeAddress
    {
        public decimal Id { get; set; }
        public string EmpAddress { get; set; }
        public decimal? CountryId { get; set; }
        public decimal? ProvinceId { get; set; }
        public decimal? CityId { get; set; }
        public decimal CityAreaId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public decimal EmployeeId { get; set; }

        public CityArea CityArea { get; set; }
        public Employee Employee { get; set; }
    }
}
