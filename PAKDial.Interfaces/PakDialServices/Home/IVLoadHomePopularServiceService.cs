using PAKDial.Domains.SqlViewModels;
using PAKDial.Domains.UserEndViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.Home
{
   public interface IVLoadHomePopularServiceService
    {
        List<VLoadHomePopularService> GetHomePopularServiceRepository();
        List<MainMenuSubMenu> GetSubMenu(decimal CatId);
    }
}
