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
    public class HomeSectionCategoryService : IHomeSectionCategoryService
    {
        private readonly IHomeSectionCategoryRepository homeSectionCategoryRepository;
        private readonly IHomeSecMainMenuCatRepository homeSecMainMenuCatRepository;


        public HomeSectionCategoryService(IHomeSectionCategoryRepository homeSectionCategoryRepository
            , IHomeSecMainMenuCatRepository homeSecMainMenuCatRepository)
        {
            this.homeSectionCategoryRepository = homeSectionCategoryRepository;
            this.homeSecMainMenuCatRepository = homeSecMainMenuCatRepository;
        }

        public int Add(HomeSectionCategory instance)
        {
            int Save = 0; //"HomeSectionCategory Not Added";
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            var CheckExistance = homeSectionCategoryRepository.CheckExistance(instance);
            if (!CheckExistance)
            {
                homeSectionCategoryRepository.Add(instance);
                Save = homeSectionCategoryRepository.SaveChanges(); //1 HomeSectionCategory Added Successfully
            }
            else
            {
                Save = -2; // HomeSectionCategory Already Exist.
            }
            return Save;
        }

        public int Update(HomeSectionCategory instance)
        {
            int Result = 0; //Record Not Updated Successfully
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            bool result = homeSectionCategoryRepository.CheckExistance(instance);
            if (!result)
            {
                var MenuCat = FindById(instance.Id);
                MenuCat.Name = instance.Name;
                MenuCat.Description = instance.Description;
                MenuCat.OrderByNo = instance.OrderByNo;
                MenuCat.UpdatedBy = instance.UpdatedBy;
                MenuCat.UpdatedDate = instance.UpdatedDate;
                MenuCat.IsActive = instance.IsActive;
                homeSectionCategoryRepository.Update(MenuCat);
                Result = homeSectionCategoryRepository.SaveChanges(); //Record Updated Successfully
            }
            else
            {
                Result = -2; //"HomeSectionCategory Already Exits";
            }
            return Result;
        }

        public int Delete(decimal Id)
        {
            int Results = 0; //"HomeSectionCategory Not Deleted."
            var checkStates = homeSecMainMenuCatRepository.GetHomeSecBySecId(Id).Count() > 0 ? true : false;
            if (!checkStates)
            {
                homeSectionCategoryRepository.Delete(FindById(Id));
                Results = homeSectionCategoryRepository.SaveChanges(); // HomeSectionCategory Deleted Successfully.
            }
            else
            {
                Results = -2; // Please Delete From MergeSection and Menu.
            }
            return Results;
        }

        public HomeSectionCategory FindById(decimal id)
        {
            return homeSectionCategoryRepository.Find(id);
        }

        public IEnumerable<HomeSectionCategory> GetAll()
        {
            return homeSectionCategoryRepository.GetAll();
        }

        public HomeSectionCategoryResponse GetHomeSection(HomeSectionCategoryRequestModel request)
        {
            return homeSectionCategoryRepository.GetHomeSection(request);
        }

    }
}
