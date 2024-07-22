using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.StoreProcedureModel
{
    public class SpGetSeOrderTracking
    {
        public decimal Id { get; set; }
        public string RoleName { get; set; }
        public string DesignationName { get; set; }
        public string EmpName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string CreatedDateVm { get; set; }
        public string UpdatedDateVm { get; set; }

        public string AmountDeposited { get; set; }
        public string StatusName { get; set; }

    }
}
