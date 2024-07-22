using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels.Configuration;
using PAKDial.Domains.ResponseModels.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.Configuration
{
    public interface ISocialMediaModesRepository : IBaseRepository<SocialMediaModes, decimal>
    {
        decimal UpdateSocialMediaModes(SocialMediaModes Instance);
        decimal AddSocialMediaModes(SocialMediaModes Instance);
        decimal DeleteSocialMediaModes(int Id);
        SocialMediaModesResponse GetSocialMediaModes(SocialMediaModesRequestModel request);
        SocialMediaModes GetByName(string Name);
        bool CheckExistance(SocialMediaModes instance);
    }
}
