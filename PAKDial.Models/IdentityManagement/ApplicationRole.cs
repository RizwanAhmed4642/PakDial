using Microsoft.AspNetCore.Identity;
using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.IdentityManagement
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
            //RoleBasedPermission = new HashSet<RoleBasedPermission>();
        }

        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        //public virtual ICollection<RoleBasedPermission> RoleBasedPermission { get; set; }
    }
}
