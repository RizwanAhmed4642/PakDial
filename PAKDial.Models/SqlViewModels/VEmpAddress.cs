using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.SqlViewModels
{
    public class VEmpAddress
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
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string CityArea { get; set; }
    }
}
