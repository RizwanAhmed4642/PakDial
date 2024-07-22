using System.Collections.Generic;

namespace PAKDial.Domains.ViewModels
{
    public class VMWrapperOtherManager
    {
        public decimal EmployeeId { get; set; }
        public decimal? ReportingTo { get; set; }
        public List<VMGenericKeyValuePair<decimal>> ManagersList { get; set; }
    }
}
