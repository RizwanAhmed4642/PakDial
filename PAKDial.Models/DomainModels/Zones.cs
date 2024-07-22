﻿using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class Zones
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
