using System.Collections.Generic;

namespace PAKDial.Domains.ViewModels
{
    public class VMStateZonesList
    {
        public VMStateZonesList()
        {
            GetStates = new List<VMGenericKeyValuePair<decimal>>();
            GetZones = new List<VMGenericKeyValuePair<decimal>>();
        }
        public List<VMGenericKeyValuePair<decimal>> GetStates { get; set; }
        public List<VMGenericKeyValuePair<decimal>> GetZones { get; set; }

    }
}
