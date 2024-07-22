using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeAddress = new HashSet<EmployeeAddress>();
            EmployeeContact = new HashSet<EmployeeContact>();
        }

        public decimal Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Cnic { get; set; }
        public string PassportNo { get; set; }
        public decimal DesignationId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UserId { get; set; }
        public bool IsActive { get; set; }
        public string ImagePath { get; set; }
        public decimal? ManagerId { get; set; }
        public decimal? ZoneManagerId { get; set; }

        public Designation Designation { get; set; }
        public ICollection<EmployeeAddress> EmployeeAddress { get; set; }
        public ICollection<EmployeeContact> EmployeeContact { get; set; }
    }
}
