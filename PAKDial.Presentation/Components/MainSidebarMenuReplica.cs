using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Interfaces.PakDialServices.IHomeLandingPageService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class MainSidebarMenuReplica : ViewComponent
    {
        private readonly IMainMenuCategoryService _SideMenuService;
        private IMemoryCache _cache;

        public MainSidebarMenuReplica(IMainMenuCategoryService SideMenuService, IMemoryCache cache)
        {
            _SideMenuService = SideMenuService;
            _cache = cache;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var MainMenu1 = new List<MainSideBarMenu>();
            if (!_cache.TryGetValue("MainMenu1", out MainMenu1))
            {
                if (MainMenu1 == null)
                {
                    MainMenu1 = _SideMenuService.GetSideMainMenu();
                }
                var CacheEntryOption = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12));
                _cache.Set("MainMenu1", MainMenu1);
            }
            return View(MainMenu1);
        }
    }
}
