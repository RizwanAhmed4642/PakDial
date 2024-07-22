using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository
{
    public interface IEmployeeAddressRepository : IBaseRepository<EmployeeAddress, decimal>
    {
        IEnumerable<EmployeeAddress> GetAddressByEmpId(decimal EmpId);
    }
}
