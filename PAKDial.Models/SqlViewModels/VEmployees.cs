using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.SqlViewModels
{
    public class VEmployees
    {
        public decimal Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Cnic { get; set; }
        public string PassportNo { get; set; }
        public decimal DesignationId { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string DesignationName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string RoleId { get; set; }
        public string ImagePath { get; set; }
    }
}
