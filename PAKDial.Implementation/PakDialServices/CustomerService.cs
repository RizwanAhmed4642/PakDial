using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository CustomRepository;

        public CustomerService(ICustomerRepository CustomRepository)
        {
            this.CustomRepository = CustomRepository;
        }

        public int Add(CreateUpdateCustomer Entity)
        {
            Customers lObjcustomer = new Customers
            {
                Cnic = Entity.Cnic,
                FirstName = Entity.FirstName,
                LastName = Entity.LastName,
                ImagePath = Entity.ImagePath,
                DateOfBirth = Entity.DateOfBirth,
                IsActive = Entity.IsActive,
                PhoneNumber = Entity.PhoneNumber,
                Id = Entity.Id,
                UserId = Entity.UserId,
                CreatedBy = Entity.CreatedBy,
                CreatedDate = Entity.CreatedDate,
                UpdatedBy = Entity.UpdatedBy,
                UpdatedDate = Entity.UpdatedDate
            };

            CustomRepository.Add(lObjcustomer);
          return  CustomRepository.SaveChanges();

        }

        public int AddRange(List<Customers> lEntity)
        {
            throw new NotImplementedException();
        }

        public int Delete(Customers Entity)
        {
            throw new NotImplementedException();
        }

        public int DeleteRange(Customers lEntity)
        {
            throw new NotImplementedException();
        }

        public Customers FindByCnic(string Cnic, decimal? CustId)
        {
            return CustomRepository.FindByCnic(Cnic, CustId);
        }

        public Customers FindById(decimal id)
        {
            return CustomRepository.Find(id);
        }
        public Customers FindByUserId(string UserId)
        {
            return CustomRepository.FindByUserId(UserId);
        }

        public GetCustomerResponse GetCustomerById(decimal Id)
        {
            return CustomRepository.GetCustomerDetails(Id);
        }

        public IEnumerable<Customers> GetAll()
        {
            throw new NotImplementedException();
        }

        public CustomerResponse GetCustomer(CustomerRequestModel request)
        {
            return CustomRepository.GetCustomer(request);
        }

        public int ImageUpdate(decimal Id, string ImagePath)
        {
            if (Id < 1)
            {
                throw new ArgumentNullException("instance");
            }
            var Cust = FindById(Id);
            Cust.ImagePath = ImagePath;
            CustomRepository.Update(Cust);
            return CustomRepository.SaveChanges();
        }

        public int Update(CreateUpdateCustomer Entity)
        {
            int Result = 0;
            if (Entity == null)
            {
                throw new ArgumentNullException("instance");
            }
            var Cnic = FindByCnic(Entity.Cnic, Entity.Id);
            var mobile = CheckExiting(Entity.PhoneNumber, Entity.Id);
            if (Cnic != null)
            {
                Result = 2; // CNIC Already Exists;
            }
            else if(mobile>0)
            {

                Result = 3; //Mobile Numeber Exists;
            }
            else
            {
                var customers = FindById(Entity.Id);
                customers.FirstName = Entity.FirstName;
                customers.LastName = Entity.LastName;
                customers.DateOfBirth = Entity.DateOfBirth;
                customers.Cnic = Entity.Cnic;
                customers.PhoneNumber = Entity.PhoneNumber;
                customers.IsActive = Entity.IsActive;
                customers.UpdatedBy = Entity.UpdatedBy;
                customers.UpdatedDate = Entity.UpdatedDate;
                CustomRepository.Update(customers);
                Result = CustomRepository.SaveChanges();
            }
            return Result;
        }

        public int UpdateRange(List<Customers> lEntity)
        {
            if (lEntity ==null)
            {
                throw new NotImplementedException();
            }
            else
            {
                CustomRepository.AddRange(lEntity);
              return  CustomRepository.SaveChanges();
            }
            
        }

        public GetCustomerListResponse GetCustomerList(GetCustomerListRequestModel request)
        {
            return CustomRepository.GetCustomerList(request);
        }

        public VClientLogins CustomerClientLogin(ClientLogin login)
        {
            return CustomRepository.CustomerClientLogin(login);
        }

        public int CheckExiting(string MobileNo, decimal id)
        {
            return CustomRepository.CheckExiting(MobileNo, id);
        }

        public decimal changeNumber(ClientChangeNo login)
        {
         return   CustomRepository.changeNumber(login);
        }
    }
}
