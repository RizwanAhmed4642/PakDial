using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels.Configuration;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ResponseModels.Configuration;
using PAKDial.Interfaces.PakDialServices.Configuration;
using PAKDial.Interfaces.Repository.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.Configuration
{
    public class TypeOfServicesService : ITypeOfServicesService
    {

        #region Prop

        private readonly ITypeOfServicesRepository _ITypeOfServicesRepository;

        #endregion

        #region Ctor

        public TypeOfServicesService(ITypeOfServicesRepository ITypeOfServicesRepository)
        {

            _ITypeOfServicesRepository = ITypeOfServicesRepository;
        }
        #endregion

        #region Method


        public decimal Add(TypeOfServices Instance)
        {
            decimal Result = 0;
            Result = _ITypeOfServicesRepository.AddTypeOfServices(Instance);
            return Result;
        }

        public decimal Delete(int Id)
        {
            return _ITypeOfServicesRepository.DeleteTypeOfServices(Id);

        }

        public IEnumerable<TypeOfServices> GetAll()
        {
            return _ITypeOfServicesRepository.GetAll();
        }

        public TypeOfServices GetById(int Id)
        {
            return  _ITypeOfServicesRepository.Find(Id);
        }

        public TypeOfServices GetByName(string Name)
        {
            return _ITypeOfServicesRepository.GetByName(Name);
        }

        public TypeOfServicesResponse GetTypeOfService(TypeOfServicesRequestModel request)
        {
            return _ITypeOfServicesRepository.GetTypeOfServices(request);
        }

        public GetServicesTypeResponse GetTypeofServicesList(TypeOfServicesRequestModel request)
        {
            return _ITypeOfServicesRepository.GetTypeofServicesList(request);
        }

        public decimal Update(TypeOfServices Instance)
        {
            return _ITypeOfServicesRepository.UpdateTypeOfServices(Instance);
        }
        #endregion
    }
}
