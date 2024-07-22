using PAKDial.Domains.SqlViewModels;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Interfaces.PakDialServices.Home;
using PAKDial.Interfaces.Repository.Home;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.Home
{
    public class VLoadHomePopularServiceService : IVLoadHomePopularServiceService
    {
        #region Prop
        private readonly IVLoadHomePopularServiceRepository _IVLoadHomePopularServiceRepository;
        #endregion

        #region Constructor
        public VLoadHomePopularServiceService(IVLoadHomePopularServiceRepository IVLoadHomePopularServiceRepository)
        {
            _IVLoadHomePopularServiceRepository = IVLoadHomePopularServiceRepository;
        }
        #endregion

        #region Method
        public List<VLoadHomePopularService> GetHomePopularServiceRepository()
        {
            return _IVLoadHomePopularServiceRepository.GetHomePopularServiceRepository();
        }

        public List<MainMenuSubMenu> GetSubMenu(decimal CatId)
        {
            return _IVLoadHomePopularServiceRepository.GetSubMenu(CatId);
        }

        #endregion

    }
}
