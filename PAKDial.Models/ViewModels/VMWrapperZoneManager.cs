using System.Collections.Generic;

namespace PAKDial.Domains.ViewModels
{
    public class VMWrapperZoneManager
    {
        public VMWrapperZoneManager()
        {
            AssignedZoneList = new List<VMGenericKeyValuePair<decimal>>();
            ManagersList = new List<VMGenericKeyValuePair<decimal>>();
            CityList = new List<VMGenericKeyValuePair<decimal>>();
        }
        public decimal EmployeeId { get; set; }
        public decimal? ReportingTo { get; set; }
        public decimal? CityId { get; set; }
        public List<VMGenericKeyValuePair<decimal>> AssignedZoneList { get; set; }
        public List<VMGenericKeyValuePair<decimal>> ManagersList { get; set; }
        public List<VMGenericKeyValuePair<decimal>> CityList { get; set; }

    }
}
