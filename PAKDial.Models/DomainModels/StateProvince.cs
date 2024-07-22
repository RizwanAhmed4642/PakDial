using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class StateProvince
    {
        public StateProvince()
        {
            City = new HashSet<City>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public decimal CountryId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public Country Country { get; set; }
        public ICollection<City> City { get; set; }
    }
}
