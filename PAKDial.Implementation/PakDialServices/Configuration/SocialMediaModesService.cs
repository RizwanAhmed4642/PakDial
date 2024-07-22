using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels.Configuration;
using PAKDial.Domains.ResponseModels.Configuration;
using PAKDial.Interfaces.PakDialServices.Configuration;
using PAKDial.Interfaces.Repository.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.Configuration
{
    public class SocialMediaModesService : ISocialMediaModesService
    {

        #region prop

        private readonly ISocialMediaModesRepository _ISocialMediaModesRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        #endregion

        #region Ctor

        public SocialMediaModesService(ISocialMediaModesRepository ISocialMediaModesRepository,
            IHostingEnvironment hostingEnvironment)
        {
            _ISocialMediaModesRepository = ISocialMediaModesRepository;
            this.hostingEnvironment = hostingEnvironment;

        }

        #endregion

        #region interface implementation

        /// <summary>
        /// Add Social media modes record.
        /// </summary>
        /// <param name="Instance"></param>
        /// <returns></returns>
        public decimal Add(SocialMediaModes Instance)
        {
            decimal Result = 0;
            Result = _ISocialMediaModesRepository.AddSocialMediaModes(Instance);
            return Result;
        }

        /// <summary>
        /// Delete single record by social media modes
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public decimal Delete(int Id)
        {
            decimal Results = 0;
            var DeleteImage = Path.Combine(new string[]
                  {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","SocialMediaModes",Id.ToString()
                  });
            if (Id > 0)
            {
                Results = _ISocialMediaModesRepository.DeleteSocialMediaModes(Id);
            }
            else
            {
                Results = -2;
            }

            if (Results == 1)
            {
                if (Directory.Exists(DeleteImage))
                {
                    Directory.Delete(DeleteImage, true);
                }
            }
            return Results;
        }

        /// <summary>
        /// Get All record
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SocialMediaModes> GetAll()
        {
            return _ISocialMediaModesRepository.GetAll();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public SocialMediaModes GetById(int Id)
        {
            return _ISocialMediaModesRepository.Find(Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public SocialMediaModes GetByName(string Name)
        {
            return _ISocialMediaModesRepository.GetByName(Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SocialMediaModesResponse GetSocialMediaModes(SocialMediaModesRequestModel request)
        {
            return _ISocialMediaModesRepository.GetSocialMediaModes(request);
        }

        /// <summary>
        ///  Upload Icon
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="file"></param>
        /// <param name="AbsolutePath"></param>
        /// <returns></returns>
        public int UploadIcons(decimal Id, IFormFile file, string AbsolutePath)
        {
            int Save = 0;
            var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
            var ExistingIcons = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","SocialMediaModes",Id.ToString()
                    });
            if (Directory.Exists(ExistingIcons))
            {
                Directory.Delete(ExistingIcons, true);
            }
            var path = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","SocialMediaModes",Id.ToString(),
                           Guid.NewGuid() + filename
                    });

            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (FileStream fs = System.IO.File.Create(path))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            path = path.Replace(hostingEnvironment.WebRootPath, "").Replace("\\", "/");
            if (!string.IsNullOrEmpty(path))
            {
                var SocialMediaNodesTypes = GetById((int)Id);
                SocialMediaNodesTypes.ImageDir = path;
                SocialMediaNodesTypes.ImageUrl = AbsolutePath + path;
                _ISocialMediaModesRepository.Update(SocialMediaNodesTypes);
                Save = _ISocialMediaModesRepository.SaveChanges();
            }
            return Save;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Instance"></param>
        /// <returns></returns>
        public decimal Update(SocialMediaModes Instance)
        {
            return _ISocialMediaModesRepository.UpdateSocialMediaModes(Instance);
        }

        #endregion

    }
}
