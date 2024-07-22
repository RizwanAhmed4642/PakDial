using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class EmployeeContact
    {
        public decimal Id { get; set; }
        public string ContactNo { get; set; }
        public string PhoneNo { get; set; }
        public decimal EmployeeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Employee Employee { get; set; }
    }
}
