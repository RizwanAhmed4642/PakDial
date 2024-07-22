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
    public class EmployeeContactRepository : BaseRepository<EmployeeContact, decimal>, IEmployeeContactRepository
    {
        public EmployeeContactRepository(PAKDialSolutionsContext context)
          : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<EmployeeContact> DbSet
        {
            get
            {
                return db.EmployeeContact;
            }
        }

        public IEnumerable<EmployeeContact> GetContactByEmpId(decimal EmpId)
        {
            List<EmployeeContact> employeeContact = null;
            if(EmpId > 0)
            {
                employeeContact = DbSet.Where(c => c.EmployeeId == EmpId).ToList();
            }
            else
            {
                employeeContact = new List<EmployeeContact>();
            }
            return employeeContact;
        }
    }
}
