using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class UserBasedPermission
    {
        public decimal Id { get; set; }
        public decimal RouteControlId { get; set; }
        public string UserId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public RouteControls RouteControl { get; set; }
        public AspNetUsers User { get; set; }
    }
}
