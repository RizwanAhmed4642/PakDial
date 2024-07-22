using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.UserEndViewModel;
using System;
using System.Collections.Generic;

namespace PAKDial.Interfaces.Repository
{
    public interface IOutSourceAdvertismentRepository : IBaseRepository<OutSourceAdvertisment, Decimal>
    {
        OutSourceAdvertismentResponse GetAdvertismentList(OutSourceAdvertismentRequestModel request);
        bool CheckExistance(OutSourceAdvertisment instance);
        void UploadImages(decimal Id, string ImagePath);
        List<VMOutSourceAdvertisment> GetLoadHomeSlider();
        void MobUploadImages(decimal Id, string MobImagePath);
    }
}
