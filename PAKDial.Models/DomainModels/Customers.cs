using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class Customers
    {
        public decimal Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Cnic { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UserId { get; set; }
        public bool? IsDefault { get; set; }
    }
}
