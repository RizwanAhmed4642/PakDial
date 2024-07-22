using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ViewModels
{
    public class VMActiveZonesWrapper
    {
        public VMActiveZonesWrapper()
        {
            ActiveZones = new ActiveZone();
            GetStates = new List<VMGenericKeyValuePair<decimal>>();
            GetZones = new List<VMGenericKeyValuePair<decimal>>();
            GetCities = new List<VMGenericKeyValuePair<decimal>>();
            GetAreas = new List<VMGenericKeyValuePair<decimal>>();
        }

        public ActiveZone ActiveZones { get; set; }
        public List<VMGenericKeyValuePair<decimal>> GetStates { get; set; }
        public List<VMGenericKeyValuePair<decimal>> GetZones { get; set; }
        public List<VMGenericKeyValuePair<decimal>> GetCities { get; set; }
        public List<VMGenericKeyValuePair<decimal>> GetAreas { get; set; }

    }
}
