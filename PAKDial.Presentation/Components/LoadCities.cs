using Microsoft.AspNetCore.Mvc;
using PAKDial.Interfaces.CommonServices;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class LoadCities : ViewComponent
    {
        private readonly ICityService cityService;
        public LoadCities(ICityService cityService)
        {
            this.cityService = cityService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(cityService.GetAllCitiesLoad());
        }
    }
}
