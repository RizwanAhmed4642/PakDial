using System.Collections.Generic;

namespace PAKDial.Domains.ViewModels
{
    public class VMWrapperCategoryManager
    {
        public VMWrapperCategoryManager()
        {
            ManagersList = new List<VMGenericKeyValuePair<decimal>>();
            AssignedCategoryList = new List<VMGenericKeyValuePair<decimal>>();
        }
        public decimal EmployeeId { get; set; }
        public decimal? ReportingTo { get; set; }
        public List<VMGenericKeyValuePair<decimal>> AssignedCategoryList { get; set; }
        public List<VMGenericKeyValuePair<decimal>> ManagersList { get; set; }
    }
}
