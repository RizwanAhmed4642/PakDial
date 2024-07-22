using System;
using System.Collections.Generic;

namespace PAKDial.Domains.ViewModels
{
    public class VMAddUpdateRegionalManager
    {
        public VMAddUpdateRegionalManager()
        {
            AssignedCityList = new List<decimal>();
        }
        public decimal EmployeeId { get; set; }
        public decimal ManagerId { get; set; }
        public decimal StateId { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<decimal> AssignedCityList { get; set; }
    }
}
