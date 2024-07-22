using PAKDial.Interfaces.PakDialServices;
using PAKDial.Interfaces.Repository;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using PAKDial.Domains.ViewModels;
using PAKDial.Domains.StoreProcedureModel;

namespace PAKDial.Implementation.PakDialServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IEmployeeAddressRepository employeeAddressRepository;
        private readonly IEmployeeContactRepository employeeContactRepository;

        public EmployeeService(IEmployeeRepository employeeRepository,
            IEmployeeAddressRepository employeeAddressRepository,
            IEmployeeContactRepository employeeContactRepository)
        {
            this.employeeRepository = employeeRepository;
            this.employeeAddressRepository = employeeAddressRepository;
            this.employeeContactRepository = employeeContactRepository;
        }

        public int Add(CreateUpdateEmployee instance)
        {
            Employee employee = new Employee
            {
                FirstName = instance.FirstName,
                LastName = instance.LastName,
                Cnic = instance.Cnic,
                CreatedBy = instance.CreatedBy,
                CreatedDate = instance.CreatedDate,
                UpdatedBy = instance.UpdatedBy,
                UpdatedDate = instance.UpdatedDate,
                DateOfBirth = instance.DateOfBirth,
                DesignationId = instance.DesignationId,
                PassportNo = instance.PassportNo,
                UserId = instance.UserId,
            };
            employee.EmployeeAddress.Add(new EmployeeAddress
            {
                EmployeeId = instance.EmployeeId,
                EmpAddress = instance.EmpAddress,
                CityAreaId = instance.CityAreaId,
                ProvinceId = instance.ProvinceId,
                CountryId = instance.CountryId,
                CityId = instance.CityId,
                CreatedBy = instance.CreatedBy,
                CreatedDate = instance.CreatedDate,
                UpdatedBy = instance.UpdatedBy,
                UpdatedDate = instance.UpdatedDate
            });

            employee.EmployeeContact.Add(new EmployeeContact
            {
                EmployeeId = instance.EmployeeId,
                ContactNo = instance.ContactNo,
                PhoneNo = instance.PhoneNo,
                CreatedBy = instance.CreatedBy,
                CreatedDate = instance.CreatedDate,
                UpdatedBy = instance.UpdatedBy,
                UpdatedDate = instance.UpdatedDate
            });
            employeeRepository.Add(employee);
            return employeeRepository.SaveChanges();
        }
        public int Update(CreateUpdateEmployee instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            return employeeRepository.UpdateEmp(instance);
        }
        public int ImageUpdate(decimal Id , string ImagePath)
        {
            if (Id < 1)
            {
                throw new ArgumentNullException("instance");
            }
            var emp = FindById(Id);
            emp.ImagePath = ImagePath;
            employeeRepository.Update(emp);
            return employeeRepository.SaveChanges();
        }
        public int Delete(Employee instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            employeeRepository.Delete(instance);
            return employeeRepository.SaveChanges();
        }
        public int AddRange(List<Employee> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            employeeRepository.AddRange(instance);
            return employeeRepository.SaveChanges();
        }
        public int UpdateRange(List<Employee> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            employeeRepository.UpdateRange(instance);
            return employeeRepository.SaveChanges();
        }
        public int DeleteRange(List<Employee> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            employeeRepository.DeleteRange(instance);
            return employeeRepository.SaveChanges();
        }
        public Employee FindById(decimal id)
        {
            return employeeRepository.Find(id);
        }
        public Employee FindByUserId(string UserId)
        {
            return employeeRepository.FindByUserId(UserId);
        }
        public IEnumerable<Employee> GetAll()
        {
            return employeeRepository.GetAll();
        }

        public IEnumerable<EmployeeAddress> GetAddressByEmpId(decimal EmpId)
        {
            return employeeAddressRepository.GetAddressByEmpId(EmpId);
        }
        public IEnumerable<EmployeeContact> GetContactByEmpId(decimal EmpId)
        {
            return employeeContactRepository.GetContactByEmpId(EmpId);
        }

        public GetEmployeeResponse GetEmployeeDetails(decimal EmpId)
        {
            return employeeRepository.GetEmployeeDetails(EmpId);
        }

        public EmployeeResponse GetEmployees(EmployeeRequestModel request)
        {
            return employeeRepository.GetEmployees(request);
        }

        public EmployeeResponse GetSpecificEmployees(EmployeeRequestModel request)
        {
            return employeeRepository.GetSpecificEmployees(request);
        }

        public Employee FindByCnic(string Cnic, decimal? EmpId)
        {
            return employeeRepository.FindByCnic(Cnic, EmpId);
        }

        public VMWrapperRegionalManager GetRegionalManager(decimal Id)
        {
            return employeeRepository.GetRegionalManager(Id);
        }

        public VMWrapperCategoryManager GetCategoryManager(decimal Id)
        {
            return employeeRepository.GetCategoryManager(Id);
        }

        public VMWrapperZoneManager GetZoneManager(decimal Id)
        {
            return employeeRepository.GetZoneManager(Id);
        }

        public VMWrapperOtherManager GetOtherManager(decimal Id)
        {
            return employeeRepository.GetOtherManager(Id);
        }

        public decimal AddUpdateOtherManager(decimal EmployeeId, decimal ManagerId, string UpdatedBy, DateTime UpdatedDate)
        {
            return employeeRepository.AddUpdateOtherManager(EmployeeId, ManagerId, UpdatedBy, UpdatedDate);
        }

        public VMOnSavedRegionalManager AddUpdateRegionalManager(VMAddUpdateRegionalManager response)
        {
            return employeeRepository.AddUpdateRegionalManager(response);
        }

        public VMOnSavedCategoryManager AddUpdateCategoryManager(VMAddUpdateCategoryManager response)
        {
            return employeeRepository.AddUpdateCategoryManager(response);
        }

        public VMOnSavedZoneManager AddUpdateZoneManager(VMAddUpdateZoneManager response)
        {
            return employeeRepository.AddUpdateZoneManager(response);
        }

        public List<EmployeeCountStateReport> EmployeeReportings(string UserId)
        {
            return employeeRepository.EmployeeReportings(UserId);
        }
    }
}
