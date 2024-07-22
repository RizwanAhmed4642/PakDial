using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.UserEndViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository
{
    public interface ICustomerRepository : IBaseRepository<Customers, decimal>
    {
        Customers FindByCnic(string Cnic, decimal? CustId);
        CustomerResponse GetCustomer(CustomerRequestModel request);
        int ImageUpdate(decimal Id, string ImagePath);
        GetCustomerResponse GetCustomerDetails(decimal CustId);
        GetCustomerListResponse GetCustomerList(GetCustomerListRequestModel request);
        VClientLogins CustomerClientLogin(ClientLogin login);
        decimal changeNumber(ClientChangeNo login);
        Customers FindByUserId(string UserId);
        int CheckExiting(string MobileNo, decimal id);
    }
}
