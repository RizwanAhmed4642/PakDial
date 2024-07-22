using System;
using System.Collections.Generic;

namespace PAKDial.Domains.ViewModels
{
    public class VMAddUpdateCategoryManager
    {
        public VMAddUpdateCategoryManager()
        {
            AssignedCategoryList = new List<decimal>();
        }
        public decimal EmployeeId { get; set; }
        public decimal ManagerId { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<decimal> AssignedCategoryList { get; set; }
    }
}
