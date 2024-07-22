using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Domains.ViewModels;
using System;
using System.Collections.Generic;

namespace PAKDial.Interfaces.PakDialServices
{
    public interface IEmployeeService
    {
        int Update(CreateUpdateEmployee instance);
        int ImageUpdate(decimal Id, string ImagePath);
        int Delete(Employee instance);
        int Add(CreateUpdateEmployee instance);
        int UpdateRange(List<Employee> instance);
        int DeleteRange(List<Employee> instance);
        int AddRange(List<Employee> instance);
        Employee FindById(decimal id);
        Employee FindByUserId(string UserId);
        Employee FindByCnic(string Cnic, decimal? EmpId);
        IEnumerable<Employee> GetAll();
        VMWrapperRegionalManager GetRegionalManager(decimal Id);
        VMWrapperCategoryManager GetCategoryManager(decimal Id);
        VMWrapperZoneManager GetZoneManager(decimal Id);
        VMWrapperOtherManager GetOtherManager(decimal Id);
        decimal AddUpdateOtherManager(decimal EmployeeId, decimal ManagerId,string UpdatedBy,DateTime UpdatedDate);
        VMOnSavedRegionalManager AddUpdateRegionalManager(VMAddUpdateRegionalManager response);
        VMOnSavedCategoryManager AddUpdateCategoryManager(VMAddUpdateCategoryManager response);
        VMOnSavedZoneManager AddUpdateZoneManager(VMAddUpdateZoneManager response);
        GetEmployeeResponse GetEmployeeDetails(decimal EmpId);
        EmployeeResponse GetEmployees(EmployeeRequestModel request);
        IEnumerable<EmployeeContact> GetContactByEmpId(decimal EmpId);
        IEnumerable<EmployeeAddress> GetAddressByEmpId(decimal EmpId);
        EmployeeResponse GetSpecificEmployees(EmployeeRequestModel request);
        List<EmployeeCountStateReport> EmployeeReportings(string UserId);
        

    }
}
