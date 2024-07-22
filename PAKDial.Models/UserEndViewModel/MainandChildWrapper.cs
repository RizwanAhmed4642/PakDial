using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.UserEndViewModel
{
    public class MainandChildWrapper
    {
        public MainandChildWrapper()
        {
            MainMenuSubMenu = new List<MainMenuSubMenu>();
            MainSideBarMenu = new List<MainSideBarMenu>();
        }
        public List<MainMenuSubMenu> MainMenuSubMenu { get; set; }
        public List<MainSideBarMenu> MainSideBarMenu { get; set; }
    }
}
