using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Interfaces.PakDialServices.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class CategorySection : ViewComponent
    {
        private readonly IVLoadHomePopularServiceService _Repo;
        private IMemoryCache _cache;
        public CategorySection(IVLoadHomePopularServiceService Repo, IMemoryCache cache)
        {
            _Repo = Repo;
            _cache = cache;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var CategorySection = new List<VLoadHomePopularService>();
            if (!_cache.TryGetValue("CategorySection", out CategorySection))
            {
                if (CategorySection == null)
                {
                    CategorySection = _Repo.GetHomePopularServiceRepository();
                }
                var CacheEntryOption = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12));
                _cache.Set("CategorySection", CategorySection);
            }
            return View(CategorySection);
        }
          
    }

}
