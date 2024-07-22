using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PAKDial.Domains.DomainModels
{
    public partial class ListingPremium
    {
        [NotMapped]
        public string CreatedBy { get; set; }
        [NotMapped]
        public DateTime? CreatedDate { get; set; }
        [NotMapped]
        public string UpdatedBy { get; set; }
        [NotMapped]
        public DateTime UpdatedDate { get; set; }
    }
}
