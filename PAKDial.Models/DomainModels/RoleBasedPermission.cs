using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class RoleBasedPermission
    {
        public decimal Id { get; set; }
        public decimal RouteControlId { get; set; }
        public string RoleId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public AspNetRoles Role { get; set; }
        public RouteControls RouteControl { get; set; }
    }
}
