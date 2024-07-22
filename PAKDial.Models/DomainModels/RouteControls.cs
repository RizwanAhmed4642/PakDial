using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class RouteControls
    {
        public RouteControls()
        {
            RoleBasedPermission = new HashSet<RoleBasedPermission>();
            UserBasedPermission = new HashSet<UserBasedPermission>();
        }

        public decimal Id { get; set; }
        public decimal ModuleId { get; set; }
        public string Areas { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string MenuName { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public bool? IsShown { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Modules Module { get; set; }
        public ICollection<RoleBasedPermission> RoleBasedPermission { get; set; }
        public ICollection<UserBasedPermission> UserBasedPermission { get; set; }
    }
}
