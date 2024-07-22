using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels.Configuration;
using PAKDial.Domains.ResponseModels.Configuration;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Interfaces.Repository.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.Configuration
{
    public class ListingTypesService : IListingTypesService
    {

        #region Prop

        private readonly IListingTypesRepository _listingTypesRepository;

        #endregion

        #region Ctor

        public ListingTypesService(IListingTypesRepository listingTypesRepository)
        {
            _listingTypesRepository = listingTypesRepository;
        }

        #endregion

        #region Interface implementation 

        /// <summary>
        /// Add Listing Types in DataBase and return Decimal Value.
        /// </summary>
        /// <param name="Instance"></param>
        /// <returns></returns>
        public decimal Add(ListingTypes Instance)
        {
            decimal Result = 0;

            bool result = _listingTypesRepository.CheckExistance(Instance);
            if (!result)
            {
                Result = _listingTypesRepository.AddListingTypes(Instance);

            }
            else
            {
                Result = -2;
            }

            return Result;
        }

        /// <summary>
        /// Delete Record On the base of Record Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public decimal Delete(decimal Id)
        {
            decimal Result = 0;
            Result = _listingTypesRepository.DeleteListingTypes(Id);

            return Result;
        }

        /// <summary>
        /// Get All Listing Types from DataBase.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ListingTypes> GetAll()
        {
              return _listingTypesRepository.GetAll();
          
        }

        /// <summary>
        /// Get single record by listing Types Id 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ListingTypes GetById(decimal Id)
        {
            return _listingTypesRepository.Find(Id);
        }

        /// <summary>
        /// Get signle record by listing type name
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public ListingTypes GetByName(string Name)
        {
            return _listingTypesRepository.GetByName(Name);

        }

        /// <summary>
        /// get list of record by searching string.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ListingTypesResponse GetListingTypes(ListingTypesRequestModel request)
        {
            return _listingTypesRepository.GetListingTypes(request);
        }

        /// <summary>
        /// Update signle record on demand of User.
        /// </summary>
        /// <param name="Instance"></param>
        /// <returns></returns>
        public decimal Update(ListingTypes Instance)
        {
            return _listingTypesRepository.UpdateListingTypes(Instance);
            
        }

        #endregion

    }
}
