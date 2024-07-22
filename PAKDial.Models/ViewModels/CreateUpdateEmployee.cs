using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ViewModels
{
    public class CreateUpdateEmployee
    {
        public decimal EmployeeId { get; set; }
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
        public bool IsActive { get; set; }

        public string EmpAddress { get; set; }
        public decimal? CountryId { get; set; }
        public decimal? ProvinceId { get; set; }
        public decimal? CityId { get; set; }
        public decimal CityAreaId { get; set; }

        public string ContactNo { get; set; }
        public string PhoneNo { get; set; }

        public string UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleId { get; set; }

    }
}
