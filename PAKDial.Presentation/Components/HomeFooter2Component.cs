using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Interfaces.PakDialServices.Home;
using PAKDial.Interfaces.PakDialServices.IHomeLandingPageService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class HomeFooter2Component : ViewComponent
    {
        private readonly IMainMenuCategoryService _SideMenuService;
        private IMemoryCache _cache;

        public HomeFooter2Component(IMainMenuCategoryService SideMenuService,IMemoryCache cache)
        {
            _SideMenuService = SideMenuService;
            _cache = cache;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Footer2Component = new List<MainSideBarMenu>();
            if (!_cache.TryGetValue("Footer2Component", out Footer2Component))
            {
                if (Footer2Component == null)
                {
                    Footer2Component = _SideMenuService.GetSideMainMenu(12);
                }
                var CacheEntryOption = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12));
                _cache.Set("Footer2Component", Footer2Component);
            }
            return View(Footer2Component);
        }

    }

}
