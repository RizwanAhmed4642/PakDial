using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices
{
  public interface ICustomerService
    {
        Customers FindByCnic(string Cnic, decimal? CustId);
        CustomerResponse GetCustomer(CustomerRequestModel request);
        int ImageUpdate(decimal Id, string ImagePath);
        Customers FindById(decimal id);

        int Add(CreateUpdateCustomer Entity);
        int AddRange(List<Customers> lEntity);
        int Update(CreateUpdateCustomer Entity);
        int UpdateRange(List<Customers> lEntity);
        int Delete(Customers Entity);
        int DeleteRange(Customers lEntity);
        GetCustomerResponse GetCustomerById(decimal Id);
        IEnumerable<Customers> GetAll();
        GetCustomerListResponse GetCustomerList(GetCustomerListRequestModel request);
        VClientLogins CustomerClientLogin(ClientLogin login);
        decimal changeNumber(ClientChangeNo login);
        Customers FindByUserId(string UserId);
        int CheckExiting(string MobileNo, decimal id);
        

    }
}
