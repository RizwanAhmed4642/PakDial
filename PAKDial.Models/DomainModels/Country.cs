﻿using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class Country
    {
        public Country()
        {
            StateProvince = new HashSet<StateProvince>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public ICollection<StateProvince> StateProvince { get; set; }
    }
}