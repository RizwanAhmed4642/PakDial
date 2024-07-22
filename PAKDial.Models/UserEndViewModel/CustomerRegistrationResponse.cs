using PAKDial.Domains.DomainModels;
using PAKDial.Domains.IdentityManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.UserEndViewModel
{
    public class CustomerRegistrationResponse
    {
        public int Result { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
    }
}
