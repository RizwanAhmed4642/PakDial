using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.UserEndViewModel
{
    public class ShowPopularCatByArea
    {
        public string CtName { get; set; }
        public decimal SbCId { get; set; }
        public string SbCName { get; set; }
        public string SortFilter { get; set; }
        public string ArName { get; set; }
        public string SbCNameReplace { get; set; }
        public int TotalRecords { get; set; }
    }
}
