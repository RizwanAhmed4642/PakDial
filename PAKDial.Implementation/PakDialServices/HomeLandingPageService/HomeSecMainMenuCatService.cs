using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Interfaces.PakDialServices.IHomeLandingPageService;
using PAKDial.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.HomeLandingPageService
{
    public class HomeSecMainMenuCatService : IHomeSecMainMenuCatService
    {
        private readonly IHomeSecMainMenuCatRepository homeSecMainMenuCatRepository;


        public HomeSecMainMenuCatService(IHomeSecMainMenuCatRepository homeSecMainMenuCatRepository)
        {
            this.homeSecMainMenuCatRepository = homeSecMainMenuCatRepository;
        }

        public int Add(HomeSecMainMenuCat instance)
        {
            int Save = 0; //"Record Not Added";
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            var CheckExistance = homeSecMainMenuCatRepository.GetHomeSecByBothId(instance.MainMenuCatId,instance.HomeSecCatId).Count() > 0 ? true : false;
            if (!CheckExistance)
            {
                homeSecMainMenuCatRepository.Add(instance);
                Save = homeSecMainMenuCatRepository.SaveChanges(); // 1 Record Added Successfully
            }
            else
            {
                Save = -2; // Record Already Exist.
            }
            return Save;
        }

        public int Update(HomeSecMainMenuCat instance)
        {
            int Save = 0; //"Record Not Updated";
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            var CheckExistance = homeSecMainMenuCatRepository.GetHomeSecByBothId(instance.MainMenuCatId, instance.HomeSecCatId).Count() > 0 ? true : false;
            if (!CheckExistance)
            {
                homeSecMainMenuCatRepository.Update(instance);
                Save = homeSecMainMenuCatRepository.SaveChanges(); //1 Record Updated Successfully
            }
            else
            {
                Save = 2; // Record Already Exist.
            }
            return Save;
        }

        public int Delete(HomeSecMainMenuCat instance)
        {
            int Results = 0; //"Record Not Deleted."
            homeSecMainMenuCatRepository.Delete(GetHomeSecByBothId(instance.MainMenuCatId,instance.HomeSecCatId).FirstOrDefault());
            Results = homeSecMainMenuCatRepository.SaveChanges();
            return Results;
        }

        public HomeSecMainMenuCat FindById(decimal id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HomeSecMainMenuCat> GetAll()
        {
            return homeSecMainMenuCatRepository.GetAll();
        }

        public List<HomeSecMainMenuCat> GetHomeSecByBothId(decimal MainMenuCatId, decimal HomeSecCatId)
        {
            return homeSecMainMenuCatRepository.GetHomeSecByBothId(MainMenuCatId, HomeSecCatId);
        }

        public List<HomeSecMainMenuCat> GetHomeSecByMenuId(decimal MainMenuCatId)
        {
            return homeSecMainMenuCatRepository.GetHomeSecByMenuId(MainMenuCatId);
        }

        public List<HomeSecMainMenuCat> GetHomeSecBySecId(decimal HomeSecCatId)
        {
            return homeSecMainMenuCatRepository.GetHomeSecBySecId(HomeSecCatId);
        }

        public HomeSecMainMenuCatResponse GetHomeSecMainMenu(HomeSecMainMenuCatRequestModel request)
        {
            return homeSecMainMenuCatRepository.GetHomeSecMainMenu(request);
        }
    }
}
