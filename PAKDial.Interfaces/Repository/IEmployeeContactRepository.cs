using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository
{
    public interface IEmployeeContactRepository : IBaseRepository<EmployeeContact,decimal>
    {
        IEnumerable<EmployeeContact> GetContactByEmpId(decimal EmpId);
    }
}
