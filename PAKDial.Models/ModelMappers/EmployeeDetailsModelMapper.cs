using PAKDial.Domains.DomainModels;
using PAKDial.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ModelMappers
{
    public static class EmployeeDetailsModelMapper
    {
        public static EmployeeViewModel MapEmployeeViewModel(Employee employee)
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Cnic = employee.Cnic,
                DateOfBirth = employee.DateOfBirth,
                Designation = employee.Designation.Name != null ? employee.Designation.Name : null,
                PassportNo = employee.PassportNo,
                CreatedBy = employee.CreatedBy,
                CreatedDate = employee.CreatedDate,
                UpdatedBy = employee.UpdatedBy,
                UpdatedDate = employee.UpdatedDate,
            };
            return employeeViewModel;
        }

        public static List<EmployeeViewModel> MapEmployeeViewModelList(List<Employee> employee)
        {
            List<EmployeeViewModel> empList = new List<EmployeeViewModel>();
            foreach (var emp in employee)
            {
                EmployeeViewModel employeeViewModel = new EmployeeViewModel
                {
                    Id = emp.Id,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    Cnic = emp.Cnic,
                    DateOfBirth = emp.DateOfBirth,
                    Designation = emp.Designation.Name != null ? emp.Designation.Name : null,
                    PassportNo = emp.PassportNo,
                    CreatedBy = emp.CreatedBy,
                    CreatedDate = emp.CreatedDate,
                    UpdatedBy = emp.UpdatedBy,
                    UpdatedDate = emp.UpdatedDate,
                };

                empList.Add(employeeViewModel);
            }
            
            return empList;
        }
    }
}
