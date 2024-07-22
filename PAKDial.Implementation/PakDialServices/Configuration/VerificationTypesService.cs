using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels.Configuration;
using PAKDial.Domains.ResponseModels.Configuration;
using PAKDial.Interfaces.PakDialServices.Configuration;
using PAKDial.Interfaces.Repository.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.Configuration
{
    public class VerificationTypesService : IVerificationTypesService
    {
        #region Prop
        private readonly IVerificationTypesRepository _IVerificationTypesRepository;
        #endregion

        #region Ctor
        public VerificationTypesService(IVerificationTypesRepository IVerificationTypesRepository)
        {
            _IVerificationTypesRepository = IVerificationTypesRepository;
        }
        #endregion

        #region Method


        public decimal Add(VerificationTypes Instance)
        {
            return _IVerificationTypesRepository.AddVerificationTypes(Instance);
        }

        public decimal Delete(int Id)
        {
            return _IVerificationTypesRepository.DeleteVerificationTypes(Id);
        }

        public IEnumerable<VerificationTypes> GetAll()
        {
            return _IVerificationTypesRepository.GetAll();
        }

        public VerificationTypes GetById(int Id)
        {
            return _IVerificationTypesRepository.Find(Id);
        }

        public VerificationTypes GetByName(string Name)
        {
            return _IVerificationTypesRepository.GetByName(Name);
        }

        public VerificationTypesResponse GetVerificationTypes(VerificationTypesRequestModel request)
        {
            return _IVerificationTypesRepository.GetVerificationTypes(request);
        }

        public int ImageUpdate(decimal Id, string ImagePath, string AbsolutePath)
        {
            if (Id < 1)
            {
                throw new ArgumentNullException("instance");
            }
            var  verificationTypes = GetById((int)Id);
            verificationTypes.ImageDir = ImagePath;
            verificationTypes.ImageUrl = AbsolutePath;
            _IVerificationTypesRepository.Update(verificationTypes);
            return _IVerificationTypesRepository.SaveChanges();
        }

        public decimal Update(VerificationTypes Instance)
        {
            return _IVerificationTypesRepository.UpdateVerificationTypes(Instance);
        }

        #endregion
    }
}
