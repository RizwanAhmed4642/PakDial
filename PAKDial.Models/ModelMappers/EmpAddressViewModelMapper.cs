using PAKDial.Domains.SqlViewModels;
using PAKDial.Domains.ViewModels;
using System.Collections.Generic;

namespace PAKDial.Domains.ModelMappers
{
    public static class EmpAddressViewModelMapper
    {
        public static EmployeeAddressViewModel MapEmpAddressViewModel(VEmpAddress address)
        {
            EmployeeAddressViewModel empAddress = new EmployeeAddressViewModel
            {
                Id = address.Id,
                EmpAddress = address.EmpAddress,
                Country =address.Country,
                Province = address.Province,
                City = address.City,
                CityArea = address.CityArea
            };
            return empAddress;
        }

        public static List<EmployeeAddressViewModel> MapEmpAddressViewModelList(List<VEmpAddress> Addresses)
        {
            List<EmployeeAddressViewModel> AddressList = new List<EmployeeAddressViewModel>();
            foreach (var address in Addresses)
            {
                EmployeeAddressViewModel empAddress = new EmployeeAddressViewModel
                {
                    Id = address.Id,
                    EmpAddress = address.EmpAddress,
                    Country = address.Country,
                    Province = address.Province,
                    City = address.City,
                    CityArea = address.CityArea
                };
                AddressList.Add(empAddress);
            }

            return AddressList;
        }
    }
}
