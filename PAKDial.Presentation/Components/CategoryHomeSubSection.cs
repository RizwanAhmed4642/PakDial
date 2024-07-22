using Microsoft.AspNetCore.Mvc;
using PAKDial.Interfaces.PakDialServices.Home;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class CategoryHomeSubSection : ViewComponent
    {
        private readonly IVLoadHomePopularServiceService _Repo;
        public CategoryHomeSubSection(IVLoadHomePopularServiceService Repo)
        {
            _Repo = Repo;
        }
        public async Task<IViewComponentResult> InvokeAsync(decimal CatId)
        {
            return View(_Repo.GetSubMenu(CatId));
        }

    }

}
