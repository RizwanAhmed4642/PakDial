using Microsoft.AspNetCore.Identity;
using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.IdentityManagement
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            //UserBasedPermission = new HashSet<UserBasedPermission>();
        }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public decimal? UserTypeId { get; set; }
        //public virtual UserType UserType { get; set; }
        //public virtual ICollection<UserBasedPermission> UserBasedPermission { get; set; }

    }
}
