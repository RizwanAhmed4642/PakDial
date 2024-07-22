using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Repository.BaseRepository;
using PAKDial.StoreProcdures.Home;

namespace PAKDial.Interfaces.Repository.Home
{
    public class VLoadHomePopularServiceRepository : IVLoadHomePopularServiceRepository
    {
        #region prop
        private readonly PAKDialSolutionsContext _context;
         
        #endregion

        #region Constructor

        public VLoadHomePopularServiceRepository(PAKDialSolutionsContext context)
        {
            _context = context;
        }

        
        #endregion

        #region Method

        public List<VLoadHomePopularService> GetHomePopularServiceRepository()
        {
            return _context.VLoadHomePopularService.ToList();
        }

        public List<MainMenuSubMenu> GetSubMenu(decimal CatId)
        {
            return _context.SubMenuCategory.Where(c=>c.MainCategoryId == CatId && c.IsActive == true).Select(c=>new MainMenuSubMenu {Id = c.Id,Name=c.Name,CatId= CatId,IsLastNode=c.IsLastNode }).Take(4).ToList();
        }

        public List<GetBulkQueryFormSubmittion> GetBulkQueryFormSubmittion(ListingQueryRequest request)
        {
           return UserFrontStoreProcedure.GetBulkQueryFormSubmittion(request);
        }

        public List<ListingQueryTrack> GetLeadQueryTrack(DateTime FromDate , DateTime ToDate, decimal ListingId, ref string CName)
        {
            CName =_context.CompanyListings.Where(c => c.Id == ListingId).FirstOrDefault().CompanyName;

            List<ListingQueryTrack> Get = _context.ListingQueryTrack.Where(c => c.ListingId == ListingId && c.CreatedDate >= FromDate && c.CreatedDate <= ToDate).ToList();
            return Get;


        }

        #endregion


    }
}
