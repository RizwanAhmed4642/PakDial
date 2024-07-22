using Microsoft.EntityFrameworkCore;
using PAKDial.Interfaces.Repository;
using PAKDial.Domains.DomainModels;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAKDial.Repository.Repositories
{
  
    public class EmployeeAddressRepository : BaseRepository<EmployeeAddress, decimal>, IEmployeeAddressRepository
    {
        public EmployeeAddressRepository(PAKDialSolutionsContext context)
          : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<EmployeeAddress> DbSet
        {
            get
            {
                return db.EmployeeAddress;
            }
        }


        

        public IEnumerable<EmployeeAddress> GetAddressByEmpId(decimal EmpId)
        {
            List<EmployeeAddress> employeeAddresses = null;
            if (EmpId > 0)
            {
                employeeAddresses = DbSet.Where(c => c.EmployeeId == EmpId).ToList();
            }
            else
            {
                employeeAddresses = new List<EmployeeAddress>();
            }
            return employeeAddresses;
        }
    }
}
