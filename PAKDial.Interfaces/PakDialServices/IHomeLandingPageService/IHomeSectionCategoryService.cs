using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System.Collections.Generic;

namespace PAKDial.Interfaces.PakDialServices.IHomeLandingPageService
{
    public interface IHomeSectionCategoryService
    {
        int Update(HomeSectionCategory instance);
        int Delete(decimal Id);
        int Add(HomeSectionCategory instance);
        HomeSectionCategory FindById(decimal id);
        IEnumerable<HomeSectionCategory> GetAll();
        HomeSectionCategoryResponse GetHomeSection(HomeSectionCategoryRequestModel request);
    }
}
