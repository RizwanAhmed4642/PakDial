using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class AssignedEmpAreas
    {
        public decimal Id { get; set; }
        public decimal? CityId { get; set; }
        public decimal? ZoneId { get; set; }
        public decimal EmployeeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
