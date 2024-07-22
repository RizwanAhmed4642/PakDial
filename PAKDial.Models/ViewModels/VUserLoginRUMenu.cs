using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ViewModels
{
    public class VUserLoginRUMenu
    {
        public decimal RouteControlId { get; set; }
        public decimal ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string RoleId { get; set; }
        public string UserId { get; set; }
        public bool IsShown { get; set; }
        public string Description { get; set; }
        public string MenuName { get; set; }
        public string ClassName { get; set; }
    }
}
