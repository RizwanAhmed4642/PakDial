using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System;

namespace PAKDial.Interfaces.Repository
{
    public interface IHomeSectionCategoryRepository : IBaseRepository<HomeSectionCategory, Decimal>
    {
        HomeSectionCategoryResponse GetHomeSection(HomeSectionCategoryRequestModel request);
        bool CheckExistance(HomeSectionCategory instance);
    }
}
