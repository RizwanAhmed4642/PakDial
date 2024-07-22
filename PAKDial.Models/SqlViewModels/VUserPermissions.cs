using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.SqlViewModels
{
    public class VUserPermissions
    {
        public decimal Id { get; set; }
        public decimal RouteControlId { get; set; }
        public decimal ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool? Status { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public decimal UserTypeId { get; set; }
    }
}
