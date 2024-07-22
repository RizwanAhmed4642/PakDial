using Microsoft.EntityFrameworkCore;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.Repository;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PAKDial.Repository.Repositories
{
    public class CustomerRepository : BaseRepository<Customers, decimal>, ICustomerRepository
    {
       
        public CustomerRepository(PAKDialSolutionsContext context) :base(context)
        {
            
        }
        protected override DbSet<Customers> DbSet
        {
            get
            {
                return db.Customers;
            }
        }

        public int CheckExiting(string MobileNo ,decimal id)
        {
            if(id>0)
            {
                return DbSet.Where(c => c.PhoneNumber == MobileNo && c.Id!= id).Count();
            }
            else
            {
                return DbSet.Where(c => c.PhoneNumber == MobileNo).Count();
            }
            
        }

        public Customers FindByUserId(string UserId)
        {
            return db.Customers.Where(c => c.UserId == UserId).FirstOrDefault();
        }
        public Customers FindByCnic(string Cnic, decimal? CustId)
        {
            if (CustId<1)
            {
                return DbSet.Where(c => c.Cnic.ToLower().Trim() == Cnic.ToLower().Trim()).FirstOrDefault();
            }
            else
            {
                return DbSet.Where(c => c.Cnic.ToLower().Trim() == Cnic.ToLower().Trim() && c.Id != CustId).FirstOrDefault();
            }
            
        }

        public CustomerResponse GetCustomer(CustomerRequestModel request)
        {
            try
            {
                int fromRow = 0;
                if (request.PageNo == 1)
                {
                    fromRow = (request.PageNo - 1) * request.PageSize;
                }
                else
                {
                    fromRow = request.PageNo;
                }
                int toRow = request.PageSize;
                bool isSearchFilterSpecified = !string.IsNullOrEmpty(request.SearchString);

                Expression<Func<VCustomers, bool>> query =
                   exp =>
                       (isSearchFilterSpecified && ((exp.FullName.Contains(request.SearchString)) || (exp.PhoneNumber.Contains(request.SearchString))
                       || (exp.CNIC.Contains(request.SearchString)))
                       || !isSearchFilterSpecified);

                int rowCount = db.VCustomers.Count(query);
                // Server Side Pager
                IEnumerable<VCustomers> Customer = request.IsAsc
                    ? db.VCustomers.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : db.VCustomers.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();

                return new CustomerResponse
                {
                    RowCount = rowCount,
                    Customers = Customer
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public GetCustomerResponse GetCustomerDetails(decimal CustId)
        {
            GetCustomerResponse CustResponse = new GetCustomerResponse();
            if (CustId > 0)
            {
                var Customer = db.VCustomers.Where(c => c.Id == CustId).FirstOrDefault();
                if (Customer != null)
                {
                    CustResponse.Customers = Customer;
                }
            }
            return CustResponse;
        }

        public GetCustomerListResponse GetCustomerList(GetCustomerListRequestModel request)
        {
            try
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;

                Expression<Func<Customers, bool>> query = null;
                if (request.IsDefault == false)
                {
                    query = exp =>
                          (string.IsNullOrEmpty(request.SearchString) || exp.FirstName.Contains(request.SearchString.Trim().ToLower())
                          || exp.LastName.Contains(request.SearchString.Trim().ToLower()) || exp.Cnic.Contains(request.SearchString.Trim()))
                          && (exp.IsDefault == false || exp.IsDefault == null) && (exp.IsActive == true);
                }
                else
                {
                    query = exp =>
                       (exp.IsDefault == true && exp.IsActive == true);
                }

                int rowCount = DbSet.Count(query);
                // Server Side Pager
                IEnumerable<VMKeyValuePair> GetCustomers =
                      DbSet.Where(query)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList().Select(c => new VMKeyValuePair { id = c.Id, text = c.FirstName +" "+c.LastName +"||"+ c.PhoneNumber });

                return new GetCustomerListResponse
                {
                    PageNo = fromRow,
                    RowCount = rowCount,
                    CustomerList = GetCustomers
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VClientLogins CustomerClientLogin(ClientLogin login)
        {
            VClientLogins vClient = new VClientLogins();
            var results = db.VClientLogins.Where(c => c.UserName.ToLower() == login.UserName.ToLower()
            && c.PhoneNumber == login.Number && c.IsActive == true && c.LockoutEnabled == false).FirstOrDefault();
            if(results != null)
            {
                vClient = results;
            }
            return vClient;
        }
        public int ImageUpdate(decimal Id, string ImagePath)
        {
            throw new NotImplementedException();
        }

        public decimal changeNumber(ClientChangeNo login)
        {
            decimal result = 0;
            try
            {
                VClientLogins vClient = new VClientLogins();

                var entity = db.Customers.Where(c => c.PhoneNumber.ToLower() == login.Number.ToLower()).FirstOrDefault();
               var count = DbSet.Where(c => c.PhoneNumber == login.NewNumber && c.Id != entity.Id).Count();
                if(count>0)
                {
                    result = 2;
                }else
                {
                    
                    entity.PhoneNumber = login.NewNumber;
                    db.Customers.Update(entity);
                    db.SaveChanges();
                    result = 1;
                }
                
               return result ;
            }
            catch (Exception ex)
            {

                return result;
            }
                
           
            
           
        }
    }
}
