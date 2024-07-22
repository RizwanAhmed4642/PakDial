using Microsoft.EntityFrameworkCore;
using PAKDial.Interfaces.Repository;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PAKDial.Domains.ViewModels;
using PAKDial.Domains.UserEndViewModel;

namespace PAKDial.Repository.Repositories
{
    public class SystemUserRepository : BaseRepository<AspNetUsers, string>, ISystemUserRepository
    {
        public SystemUserRepository(PAKDialSolutionsContext context)
          : base(context)
        {

        }
        /// Primary database set
        protected override DbSet<AspNetUsers> DbSet
        {
            get
            {
                return db.AspNetUsers;
            }
        }
        public CustomerRegistrationResponse RegisterCustomer(RegisterViewModels register)
        {
            DateTime date = DateTime.Now;
            CustomerRegistrationResponse response = new CustomerRegistrationResponse();
            var CheckEmail = db.AspNetUsers.Where(c => c.Email.ToLower() == register.Email.ToLower());
            var CheckPhoneNo = db.Customers.Where(c => c.PhoneNumber == register.MobileNo);
            if(CheckEmail.Count() < 1 && CheckPhoneNo.Count() < 1)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        AspNetUsers aspNet = new AspNetUsers
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserName = register.Email.ToLower(),
                            NormalizedUserName = register.Email.ToUpper(),
                            Email = register.Email.ToLower(),
                            NormalizedEmail = register.Email.ToUpper(),
                            EmailConfirmed = true,
                            PasswordHash = register.PasswordHash,
                            SecurityStamp = Guid.NewGuid().ToString(),
                            PhoneNumberConfirmed = false,
                            LockoutEnabled = false,
                            AccessFailedCount = 0,
                            CreatedDate = date,
                            UpdatedDate = date,
                            UserTypeId = 2,
                        };
                        db.AspNetUsers.Add(aspNet);
                        db.SaveChanges();
                        AspNetUserRoles adduserrole = new AspNetUserRoles
                        {
                            RoleId = db.AspNetRoles.Where(c => c.Name.ToLower() == "Guest".ToLower()).FirstOrDefault().Id,
                            UserId = aspNet.Id,
                        };
                        Customers customers = new Customers
                        {
                            FirstName = register.FirstName,
                            LastName = register.LastName,
                            Cnic = !string.IsNullOrEmpty(register.Cnic) ? register.Cnic : null,
                            IsDefault = false,
                            IsActive = true,
                            PhoneNumber = register.MobileNo,
                            CreatedDate = date,
                            UpdatedDate = date,
                            UserId = aspNet.Id
                        };
                        db.AspNetUserRoles.Add(adduserrole);
                        db.Customers.Add(customers);
                        db.SaveChanges();
                        transaction.Commit();
                        response.Result = 1;
                        response.Message = "";
                        response.UserId = aspNet.Id;
                    }
                    catch (Exception ex)
                    {
                        response.Result = 0;
                        response.Message = "Failed To Register User.";
                    }
                }
            }
            else
            {
                response.Result = 0;
                response.Message = "Email or Phone Number Already Exits.";
            }
            return response;
        }

        public LoginMenuResponseModel GetRoleUserMenu(string UserId,string RoleId)
        {
            LoginMenuResponseModel loginMenu = new LoginMenuResponseModel();
            var LoginMneu = db.VUserLoginRUMenu.Where(c => c.UserId == UserId || c.RoleId == RoleId).ToList();
            if(LoginMneu.Count() > 0 && LoginMneu != null)
            {
                loginMenu.UserLoginRUMenu.AddRange(LoginMneu);
            }
            return loginMenu;
        }

        public bool IsExitUserByRoleId(string RoleId)
        {
            return db.AspNetUserRoles.Where(c => c.RoleId == RoleId).FirstOrDefault() != null ? true : false;
        }

        public SystemUserResponse GetAllSystemUsers(SystemUserRequestModel request)
        {
            try
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;
                bool isSearchFilterSpecified = !string.IsNullOrEmpty(request.SearchString);

                Expression<Func<VSystemUser, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.FirstName.Contains(request.SearchString))
                        || (exp.LastName.Contains(request.SearchString)) 
                        || (exp.DesignationName.Contains(request.SearchString))
                        || (exp.RoleName.Contains(request.SearchString))
                        || (exp.UserName.Contains(request.SearchString))) || !isSearchFilterSpecified);

                int rowCount = db.VSystemUser.Count(query);
                // Server Side Pager
                IEnumerable<VSystemUser> SystemUsers = request.IsAsc
                    ? db.VSystemUser.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : db.VSystemUser.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();
                return new SystemUserResponse
                {
                    RowCount = rowCount,
                    SystemUsers = SystemUsers
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public AspNetUsers FindByEmail(string Email)
        {
            return DbSet.Where(c => c.Email.ToLower().Trim() == Email.ToLower().Trim()).FirstOrDefault();
        }
    }
}
