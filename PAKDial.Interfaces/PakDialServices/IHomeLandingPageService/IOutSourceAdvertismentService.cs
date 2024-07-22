using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.UserEndViewModel;
using System.Collections.Generic;

namespace PAKDial.Interfaces.PakDialServices.IHomeLandingPageService
{
    public interface IOutSourceAdvertismentService
    {
        int Update(OutSourceAdvertisment instance);
        int Delete(decimal Id);
        decimal Add(OutSourceAdvertisment instance);
        OutSourceAdvertisment FindById(decimal id);
        IEnumerable<OutSourceAdvertisment> GetAll();
        int UploadImages(decimal Id, string ImagePath);
        OutSourceAdvertismentResponse GetAdvertismentList(OutSourceAdvertismentRequestModel request);
        List<VMOutSourceAdvertisment> GetLoadHomeSlider();
        int MobUploadImages(decimal Id, string MobImagePath);
    }
}
