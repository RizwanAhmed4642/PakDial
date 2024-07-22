using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.SqlViewModels
{
    public class VSystemUser
    {
        public decimal Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Cnic { get; set; }
        public decimal DesignationId { get; set; }
        public string DesignationName { get; set; }
        public decimal EmpId { get; set; }
        public string UserName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
