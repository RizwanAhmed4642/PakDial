using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class BusinessTypes
    {
        public BusinessTypes()
        {
            ListingsBusinessTypes = new HashSet<ListingsBusinessTypes>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ICollection<ListingsBusinessTypes> ListingsBusinessTypes { get; set; }
    }
}
