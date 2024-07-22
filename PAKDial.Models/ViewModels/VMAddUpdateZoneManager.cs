using System;
using System.Collections.Generic;

namespace PAKDial.Domains.ViewModels
{
    public class VMAddUpdateZoneManager
    {
        public VMAddUpdateZoneManager()
        {
            AssignedZoneList = new List<decimal>();
        }
        public decimal EmployeeId { get; set; }
        public decimal ManagerId { get; set; }
        public decimal CityId { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<decimal> AssignedZoneList { get; set; }
    }
}
