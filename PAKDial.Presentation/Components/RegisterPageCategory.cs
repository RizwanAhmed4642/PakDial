using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Interfaces.PakDialServices.IHomeLandingPageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class RegisterPageCategory : ViewComponent
    {
        private readonly IMainMenuCategoryService _SideMenuService;
        private IMemoryCache _cache;
        public RegisterPageCategory(IMainMenuCategoryService SideMenuService, IMemoryCache cache)
        {
            _SideMenuService = SideMenuService;
            _cache = cache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var MainM1 = new List<MainSideBarMenu>();
            if (!_cache.TryGetValue("MainM1", out MainM1))
            {
                if (MainM1 == null)
                {
                    MainM1 = _SideMenuService.GetRegListMenu(10);
                }
                var CacheEntryOption = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12));
                _cache.Set("MainM1", MainM1);
            }
            return View(MainM1);
        }
    }
}
