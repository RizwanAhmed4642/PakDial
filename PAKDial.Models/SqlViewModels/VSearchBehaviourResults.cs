using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.SqlViewModels
{
    public class VSearchBehaviourResults
    {
        public string SearchResults { get; set; }
        public int TotalSearches { get; set; }
        public string LocationSearch { get; set; }
        public string AreaSearch { get; set; }
    }
}
