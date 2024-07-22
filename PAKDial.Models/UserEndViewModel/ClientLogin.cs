using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.UserEndViewModel
{
    public class ClientLogin
    {
        public string UserName { get; set; }
        public string Number { get; set; }
    }
    public class ClientChangeNo
    {
        public string UserName { get; set; }
        public string Number { get; set; }
        public string NewNumber { get; set; }
    }
    public class VClientLogins
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public bool LockoutEnabled { get; set; }
    }
}
