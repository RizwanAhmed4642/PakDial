using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ViewModels
{
    public class VMEmployeeAddress
    {
        public string EmpAddress { get; set; }
        public decimal? CountryId { get; set; }
        public decimal? ProvinceId { get; set; }
        public decimal? CityId { get; set; }
        public decimal CityAreaId { get; set; }
    }
}
