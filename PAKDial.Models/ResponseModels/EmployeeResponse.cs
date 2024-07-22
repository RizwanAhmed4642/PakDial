using PAKDial.Domains.DomainModels;
using PAKDial.Domains.SqlViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class EmployeeResponse
    {
        public int RowCount { get; set; }

        public IEnumerable<VEmployees> Employees { get; set; }
    }
}
