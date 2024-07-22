using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices
{
    public class BusinessTypesService : IBusinessTypesService
    {
        private readonly IBusinessTypesRepository businessTypesRepository;
        private readonly IListingsBusinessTypesRepository listingsBusinessTypesRepo;

        public BusinessTypesService(IBusinessTypesRepository businessTypesRepository
            , IListingsBusinessTypesRepository listingsBusinessTypesRepo)
        {
            this.businessTypesRepository = businessTypesRepository;
            this.listingsBusinessTypesRepo = listingsBusinessTypesRepo;
        }

        public decimal Add(BusinessTypes instance)
        {
            decimal Save = 0; //"Business Type Not Added";
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            var CheckExistance = businessTypesRepository.CheckExistance(instance);
            if (!CheckExistance)
            {
                businessTypesRepository.Add(instance);
                Save = businessTypesRepository.SaveChanges(); //1 Business Type Added Successfully
            }
            else
            {
                Save = -2; // Business Type Already Exist.
            }
            return Save;
        }

        public decimal Update(BusinessTypes instance)
        {
            decimal Result = 0; //Record Not Updated Successfully
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            bool result = businessTypesRepository.CheckExistance(instance);
            if (!result)
            {
                var EditBusiness = FindById(instance.Id);
                EditBusiness.Name = instance.Name;
                EditBusiness.UpdatedBy = instance.UpdatedBy;
                EditBusiness.UpdatedDate = instance.UpdatedDate;
                businessTypesRepository.Update(EditBusiness);
                Result = businessTypesRepository.SaveChanges(); //Record Updated Successfully
            }
            else
            {
                Result = -2; //"Business Type Already Exits";
            }
            return Result;
        }

        public decimal Delete(decimal Id)
        {
            decimal Results = 0; //"Business Type Not Deleted."
            var checkStates = listingsBusinessTypesRepo.BusinessTypeExistance(Id);
            if (!checkStates)
            {
                businessTypesRepository.Delete(FindById(Id));
                Results = businessTypesRepository.SaveChanges(); // Business Type Deleted Successfully.
            }
            else
            {
                Results = -2; // Please Delete its Business Listing Type First.
            }
            return Results;
        }

        public BusinessTypes FindById(decimal id)
        {
            return businessTypesRepository.Find(id);
        }

        public IEnumerable<BusinessTypes> GetAll()
        {
            return businessTypesRepository.GetAll();
        }

        public IEnumerable<VMGenericKeyValuePair<decimal>> GetAllBusinessTypeKey()
        {
            return businessTypesRepository.GetAllBusinessTypeKey();
        }

        public BusinessTypesResponse GetBusinessTypes(BusinessTypesRequestModel request)
        {
            return businessTypesRepository.GetBusinessTypes(request);
        }
    }
}
