using PAKDial.Domains.DomainModels;
using PAKDial.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ModelMappers
{
    public static class EmpContactViewModelMapper
    {
        public static EmployeeContactViewModel MapEmpContactViewModel(EmployeeContact contact)
        {
            EmployeeContactViewModel empContact = new EmployeeContactViewModel
            {
                Id = contact.Id,
                ContactNo = contact.ContactNo
            };
            return empContact;
        }

        public static List<EmployeeContactViewModel> MapEmpContactViewModelList(List<EmployeeContact> contacts)
        {
            List<EmployeeContactViewModel> ContactList = new List<EmployeeContactViewModel>();
            foreach (var contact in contacts)
            {
                EmployeeContactViewModel empContact = new EmployeeContactViewModel
                {
                    Id = contact.Id,
                    ContactNo = contact.ContactNo
                };
                ContactList.Add(empContact);
            }
            
            return ContactList;
        }
    }
}
