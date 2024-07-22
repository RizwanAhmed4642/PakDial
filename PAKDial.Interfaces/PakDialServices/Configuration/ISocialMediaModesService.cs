using Microsoft.AspNetCore.Http;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels.Configuration;
using PAKDial.Domains.ResponseModels.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.Configuration
{
    public interface ISocialMediaModesService
    {
        decimal Update(SocialMediaModes Instance);
        decimal Add(SocialMediaModes Instance);
        decimal Delete(int Id);
        SocialMediaModes GetById(int Id);
        SocialMediaModes GetByName(string Name);
        IEnumerable<SocialMediaModes> GetAll();
        SocialMediaModesResponse GetSocialMediaModes(SocialMediaModesRequestModel request);
        int UploadIcons(decimal Id, IFormFile file, string AbsolutePath);
    }
}
