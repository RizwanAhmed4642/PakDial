using PAKDial.Domains.DomainModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ResponseModels
{
    public class RoleResponse
    {
        public int RowCount { get; set; }

        public IEnumerable<AspNetRoles> AspRoles { get; set; }
    }


}
