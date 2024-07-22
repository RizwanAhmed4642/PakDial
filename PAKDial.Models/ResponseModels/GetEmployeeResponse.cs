using PAKDial.Domains.DomainModels;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ResponseModels
{
    public class GetEmployeeResponse
    {
        public GetEmployeeResponse()
        {
            Employees = new VEmployees();
            Contacts = new VMEmployeeContact();
            Addresses = new VMEmployeeAddress();
            States = new VMStateProvince();
            Cities = new VMCity();
            CityAreas = new VMCityArea();
        }
        public VEmployees Employees { get; set; }
        public VMEmployeeContact Contacts { get; set; }
        public VMEmployeeAddress Addresses { get; set; }
        public VMStateProvince States { get; set; }
        public VMCity Cities { get; set; }
        public VMCityArea CityAreas { get; set; }
    }
}
