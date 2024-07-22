using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class SearchBehaviour
    {
        public decimal Id { get; set; }
        public string SearchResults { get; set; }
        public string LocationSearch { get; set; }
        public string AreaSearch { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
