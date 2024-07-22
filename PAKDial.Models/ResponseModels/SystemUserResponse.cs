using PAKDial.Domains.DomainModels;
using PAKDial.Domains.SqlViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ResponseModels
{
    public class SystemUserResponse
    {
        public int RowCount { get; set; }

        public IEnumerable<VSystemUser> SystemUsers { get; set; }
    }
}
