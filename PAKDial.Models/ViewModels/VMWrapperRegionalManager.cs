using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ViewModels
{
    public class VMWrapperRegionalManager
    {
        public VMWrapperRegionalManager()
        {
            AssignedCityList = new List<VMGenericKeyValuePair<decimal>>();
            ManagersList = new List<VMGenericKeyValuePair<decimal>>();
            StateList = new List<VMGenericKeyValuePair<decimal>>();
        }
        public decimal EmployeeId { get; set; }
        public decimal? ReportingTo { get; set; }
        public decimal? StateId { get; set; }
        public List<VMGenericKeyValuePair<decimal>> AssignedCityList { get; set; }
        public List<VMGenericKeyValuePair<decimal>> StateList { get; set; }
        public List<VMGenericKeyValuePair<decimal>> ManagersList { get; set; }
    }
}
