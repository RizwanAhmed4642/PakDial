﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Interfaces.PakDialServices.Home;
using PAKDial.Interfaces.PakDialServices.IHomeLandingPageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class HomeFooterComponent : ViewComponent
    {
        private readonly IHomeListingService _homeListing;
        private IMemoryCache _cache;

        public HomeFooterComponent(IHomeListingService homeListing,IMemoryCache cache)
        {
            _homeListing = homeListing;
            _cache = cache;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var FooterComponent = new MainandChildWrapper();
            if (!_cache.TryGetValue("FooterComponent", out FooterComponent))
            {
                if (FooterComponent == null || FooterComponent.MainMenuSubMenu == null || FooterComponent.MainSideBarMenu == null)
                {
                    FooterComponent = _homeListing.GetMainandChildCategory();
                }
                var CacheEntryOption = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12));
                _cache.Set("FooterComponent", FooterComponent);
            }
            return View(FooterComponent);
        }
          
    }
}