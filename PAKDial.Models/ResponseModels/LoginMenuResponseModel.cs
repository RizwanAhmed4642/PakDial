using PAKDial.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ResponseModels
{
    public class LoginMenuResponseModel
    {
        public LoginMenuResponseModel()
        {
            UserLoginRUMenu = new List<VUserLoginRUMenu>();
        }
        public List<VUserLoginRUMenu> UserLoginRUMenu { get; set; }
    }
}
