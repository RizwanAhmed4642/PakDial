using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository
{
    public interface IEmployeeRepository : IBaseRepository<Employee,decimal>
    {
        EmployeeResponse GetEmployees(EmployeeRequestModel request);
        bool EmployeeDesignationExits(decimal DesignationId);
        int UpdateEmp(CreateUpdateEmployee instance);
        GetEmployeeResponse GetEmployeeDetails(decimal EmpId);
        Employee FindByCnic(string Cnic, decimal? EmpId);
        Employee FindByUserId(string UserId);

        VMWrapperRegionalManager GetRegionalManager(decimal Id);
        VMWrapperCategoryManager GetCategoryManager(decimal Id);
        VMWrapperZoneManager GetZoneManager(decimal Id);
        VMWrapperOtherManager GetOtherManager(decimal Id);
        decimal AddUpdateOtherManager(decimal EmployeeId, decimal ManagerId, string UpdatedBy, DateTime UpdatedDate);
        VMOnSavedRegionalManager AddUpdateRegionalManager(VMAddUpdateRegionalManager response);
        VMOnSavedCategoryManager AddUpdateCategoryManager(VMAddUpdateCategoryManager response);
        VMOnSavedZoneManager AddUpdateZoneManager(VMAddUpdateZoneManager response);
        EmployeeResponse GetSpecificEmployees(EmployeeRequestModel request);
        List<EmployeeCountStateReport> EmployeeReportings(string UserId);
    }
}
