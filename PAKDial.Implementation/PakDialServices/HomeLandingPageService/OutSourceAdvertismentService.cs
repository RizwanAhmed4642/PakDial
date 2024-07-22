using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Interfaces.PakDialServices.IHomeLandingPageService;
using PAKDial.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.HomeLandingPageService
{
    public class OutSourceAdvertismentService : IOutSourceAdvertismentService
    {
        private readonly IOutSourceAdvertismentRepository outSourceAdvertismentRepository;

        public OutSourceAdvertismentService(IOutSourceAdvertismentRepository outSourceAdvertismentRepository)
        {
            this.outSourceAdvertismentRepository = outSourceAdvertismentRepository;
        }

        public decimal Add(OutSourceAdvertisment instance)
        {
            decimal Save = 0; //"Advertisement Not Added";
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            var CheckExistance = outSourceAdvertismentRepository.CheckExistance(instance);
            if (!CheckExistance)
            {
                outSourceAdvertismentRepository.Add(instance);
                //1 Advertisement Added Successfully
                if(outSourceAdvertismentRepository.SaveChanges() > 0 )
                {
                    Save = instance.Id;
                }
            }
            else
            {
                Save = -2; // Advertisement Already Exist.
            }
            return Save;
        }

        public int Update(OutSourceAdvertisment instance)
        {
            int Result = 0; //Record Not Updated Successfully
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            bool result = outSourceAdvertismentRepository.CheckExistance(instance);
            if (!result)
            {
                var outSource = FindById(instance.Id);
                outSource.Name = instance.Name;
                outSource.Description = instance.Description;
                outSource.ImageUrl = instance.ImageUrl;
                outSource.ImageBtn = instance.ImageBtn;
                outSource.UpdatedBy = instance.UpdatedBy;
                outSource.UpdatedDate = instance.UpdatedDate;
                outSource.IsActive = instance.IsActive;
                outSourceAdvertismentRepository.Update(outSource);
                Result = outSourceAdvertismentRepository.SaveChanges(); //Record Updated Successfully
            }
            else
            {
                Result = -2; //"Advertisement Already Exits";
            }
            return Result;
        }

        public int Delete(decimal Id)
        {
            int Results = 0; //"Advertisement Not Deleted."
            outSourceAdvertismentRepository.Delete(FindById(Id));
            Results = outSourceAdvertismentRepository.SaveChanges(); // Advertisement Deleted Successfully.
            return Results;
        }

        public OutSourceAdvertisment FindById(decimal id)
        {
            return outSourceAdvertismentRepository.Find(id);
        }

        public OutSourceAdvertismentResponse GetAdvertismentList(OutSourceAdvertismentRequestModel request)
        {
            return outSourceAdvertismentRepository.GetAdvertismentList(request);
        }

        public IEnumerable<OutSourceAdvertisment> GetAll()
        {
            return outSourceAdvertismentRepository.GetAll();
        }

        public int UploadImages(decimal Id, string ImagePath)
        {
            int Save = 0;
            if (!string.IsNullOrEmpty(ImagePath))
            {
                outSourceAdvertismentRepository.UploadImages(Id, ImagePath);
                Save = outSourceAdvertismentRepository.SaveChanges();
            }
            return Save;
        }
        public int MobUploadImages(decimal Id, string MobImagePath)
        {
            int Save = 0;
            if (!string.IsNullOrEmpty(MobImagePath))
            {
                outSourceAdvertismentRepository.MobUploadImages(Id, MobImagePath);
                Save = outSourceAdvertismentRepository.SaveChanges();
            }
            return Save;
        }

        public List<VMOutSourceAdvertisment> GetLoadHomeSlider()
        {
            return outSourceAdvertismentRepository.GetLoadHomeSlider();
        }
    }
}
