using Microsoft.EntityFrameworkCore;
using PAKDial.Interfaces.Repository;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.ModelMappers;
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
using PAKDial.Domains.Common;
using PAKDial.Domains.StoreProcedureModel;
using PAKDial.StoreProcdures.ReportingProcdures;

namespace PAKDial.Repository.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee, decimal>, IEmployeeRepository
    {
        private readonly IEmployeeAddressRepository employeeAddressRepository;
        private readonly IEmployeeContactRepository employeeContactRepository;
        private readonly IStateProvinceRepository stateProvinceRepository;
        private readonly ICityRepository cityRepository;
        private readonly ICityAreaRepository cityAreaRepository;
        public EmployeeRepository(PAKDialSolutionsContext context, IEmployeeAddressRepository employeeAddressRepository,
            IEmployeeContactRepository employeeContactRepository,
            IStateProvinceRepository stateProvinceRepository,
            ICityRepository cityRepository,
            ICityAreaRepository cityAreaRepository)
          : base(context)
        {
            this.employeeAddressRepository = employeeAddressRepository;
            this.employeeContactRepository = employeeContactRepository;
            this.stateProvinceRepository = stateProvinceRepository;
            this.cityRepository = cityRepository;
            this.cityAreaRepository = cityAreaRepository;
        }
        /// Primary database set
        protected override DbSet<Employee> DbSet
        {
            get
            {
                return db.Employee;
            }
        }

        public bool EmployeeDesignationExits(decimal DesignationId)
        {
            return db.Employee.Where(c => c.DesignationId == DesignationId).Count() > 0 ? true : false;
        }

        public Employee FindByCnic(string Cnic , decimal? EmpId)
        {   if(EmpId < 1)
               return DbSet.Where(c => c.Cnic.ToLower().Trim() == Cnic.ToLower().Trim()).FirstOrDefault();
            else
                return DbSet.Where(c => c.Cnic.ToLower().Trim() == Cnic.ToLower().Trim() && c.Id != EmpId).FirstOrDefault();
        }
        public Employee FindByUserId(string UserId)
        {
            return DbSet.Where(c => c.UserId == UserId).FirstOrDefault();
        }

        //Rapper Class Use For showing Employee Details
        public GetEmployeeResponse GetEmployeeDetails(decimal EmpId)
        {
            GetEmployeeResponse employeeResponse = new GetEmployeeResponse();
            if (EmpId > 0)
            {
                var Employee = db.VEmployees.Where(c => c.Id == EmpId).FirstOrDefault();
                if (Employee != null)
                {
                    var Address = employeeAddressRepository.GetAddressByEmpId(EmpId).FirstOrDefault();
                    var Contact = employeeContactRepository.GetContactByEmpId(EmpId).FirstOrDefault();
                    var State = stateProvinceRepository.Find((decimal)Address.ProvinceId);
                    var Citie = cityRepository.Find((decimal)Address.CityId);
                    var CityArea = cityAreaRepository.Find((decimal)Address.CityAreaId);

                    employeeResponse.Employees = Employee;
                    employeeResponse.Addresses = new VMEmployeeAddress { CountryId= Address.CountryId,ProvinceId= Address.ProvinceId,
                    CityId = Address.CityId,CityAreaId= Address.CityAreaId,EmpAddress=Address.EmpAddress};
                    employeeResponse.Contacts = new VMEmployeeContact {ContactNo=Contact.ContactNo,PhoneNo=Contact.PhoneNo };
                    employeeResponse.States = new VMStateProvince {Id=State.Id,Name=State.Name };
                    employeeResponse.Cities = new VMCity { Id = Citie.Id, Name = Citie.Name };
                    employeeResponse.CityAreas = new VMCityArea { Id = CityArea.Id, Name = CityArea.Name };
                }
            }
            return employeeResponse;
        }

        public EmployeeResponse GetEmployees(EmployeeRequestModel request)
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

                Expression<Func<VEmployees, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.FirstName.Contains(request.SearchString))
                        || (exp.LastName.Contains(request.SearchString)) || (exp.Cnic.Contains(request.SearchString))
                        || (exp.Email.Contains(request.SearchString)) || (exp.DesignationName.Contains(request.SearchString)))
                        || !isSearchFilterSpecified);


                int rowCount = db.VEmployees.Count(query);
                // Server Side Pager
                IEnumerable<VEmployees> employees = request.IsAsc
                    ? db.VEmployees.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : db.VEmployees.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();

                return new EmployeeResponse
                {
                    RowCount = rowCount,
                    Employees = employees
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public EmployeeResponse GetSpecificEmployees(EmployeeRequestModel request)
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
                List<string> vs = new List<string>
                {
                    AspUserRoles.SalesManager.ToString(),
                    "Call Center",
                    AspUserRoles.SalesExecutive.ToString()
                };

                var Roles = db.AspNetRoles.Where(c => vs.Contains(c.Name)).ToList().Select(c => c.Id);
                Expression<Func<VEmployees, bool>> query =
                    exp =>
                        (isSearchFilterSpecified && ((exp.FirstName.Contains(request.SearchString))
                        || (exp.LastName.Contains(request.SearchString)) || (exp.Cnic.Contains(request.SearchString))
                        || (exp.Email.Contains(request.SearchString)) || (exp.DesignationName.Contains(request.SearchString)))
                        || !isSearchFilterSpecified) && Roles.Contains(exp.RoleId);

                int rowCount = db.VEmployees.Count(query);
                // Server Side Pager
                IEnumerable<VEmployees> employees = request.IsAsc
                    ? db.VEmployees.Where(query)
                        .OrderBy(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : db.VEmployees.Where(query)
                        .OrderByDescending(x => x.Id)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();

                return new EmployeeResponse
                {
                    RowCount = rowCount,
                    Employees = employees
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public VMWrapperRegionalManager GetRegionalManager(decimal Id)
        {
            var employee = db.Employee.Where(c => c.Id == Id).FirstOrDefault();
            var ManagersId = db.Designation.Where(c => c.Id == employee.DesignationId).Select(c => c.ReportingTo).FirstOrDefault();
            VMWrapperRegionalManager response = new VMWrapperRegionalManager
            {
                EmployeeId = employee.Id,
                ReportingTo = employee.ManagerId,
                StateList = db.StateProvince.Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.Name }).ToList(),
                ManagersList = DbSet.Where(c => c.DesignationId == (decimal)ManagersId).Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.FirstName + c.LastName }).ToList(),
            };
            var GetAssignedCities = db.AssignedEmpAreas.Where(c => c.EmployeeId == employee.Id).Select(c => c.CityId).ToList();
            if(GetAssignedCities != null && GetAssignedCities.Count() > 0)
            {
                response.AssignedCityList = db.City.Where(c => GetAssignedCities.Contains(c.Id)).Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.Name }).ToList();
                response.StateId = db.City.Where(c => c.Id == response.AssignedCityList.FirstOrDefault().Id).Select(c => c.StateId).FirstOrDefault();
            }
            return response;
        }

        public VMWrapperCategoryManager GetCategoryManager(decimal Id)
        {
            var employee = db.Employee.Where(c => c.Id == Id).FirstOrDefault();
            var ManagersId = db.Designation.Where(c => c.Id == employee.DesignationId).Select(c => c.ReportingTo).FirstOrDefault();
            VMWrapperCategoryManager response = new VMWrapperCategoryManager
            {
                EmployeeId = employee.Id,
                ReportingTo = employee.ManagerId,
                ManagersList = DbSet.Where(c => c.DesignationId == (decimal)ManagersId).Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.FirstName + c.LastName }).ToList(),
            };
            var GetAssignedCategories = db.AssignedEmpCategory.Where(c => c.EmployeeId == employee.Id).Select(c => c.CategoryId).ToList();
            if (GetAssignedCategories != null && GetAssignedCategories.Count() > 0)
            {
                response.AssignedCategoryList = db.MainMenuCategory.Where(c => GetAssignedCategories.Contains(c.Id)).Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.Name }).ToList();
            }
            return response;
        }

        public VMWrapperZoneManager GetZoneManager(decimal Id)
        {
            var employee = db.Employee.Where(c => c.Id == Id).FirstOrDefault();
            var ManagersId = db.Designation.Where(c => c.Id == employee.DesignationId).Select(c => c.ReportingTo).FirstOrDefault();
            VMWrapperZoneManager response = new VMWrapperZoneManager
            {
                EmployeeId = employee.Id,
                ReportingTo = employee.ManagerId,
                ManagersList = DbSet.Where(c => c.DesignationId == (decimal)ManagersId).Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.FirstName + c.LastName }).ToList(),
            };
            var GetAreas = db.AssignedEmpAreas.Where(c => c.EmployeeId == employee.Id).ToList();
            if (GetAreas != null && GetAreas.Count() > 0)
            {
                response.AssignedZoneList = db.Zones.Where(c => GetAreas.Select(e=>e.ZoneId).Contains(c.Id)).Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.Name }).ToList();
                response.CityId = GetAreas.Select(c => c.CityId).FirstOrDefault();
            }
            if (employee.ManagerId > 0)
            {
                var RegionalManagerArea = db.AssignedEmpAreas.Where(c => c.EmployeeId == employee.ManagerId).Select(c => c.CityId).ToList();
                if (RegionalManagerArea != null && RegionalManagerArea.Count() > 0)
                {
                    response.CityList = db.City.Where(c => RegionalManagerArea.Contains(c.Id)).Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.Name }).ToList();
                }
            }
            else
            {
                var GetSameLevelManagers = db.Employee.Where(c => c.DesignationId == ManagersId).Select(c => c.Id).ToList();
                if(GetSameLevelManagers.Count() > 0)
                {
                    var GetCities = db.AssignedEmpAreas.Where(c => GetSameLevelManagers.Contains(c.EmployeeId)).Select(c=>c.CityId).ToList();
                    response.CityList = db.City.Where(c => GetCities.Contains(c.Id)).Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.Name }).ToList();
                }
            }
            return response;
        }

        public VMWrapperOtherManager GetOtherManager(decimal Id)
        {
            var employee = db.Employee.Where(c => c.Id == Id).FirstOrDefault();
            var ManagersId = db.Designation.Where(c => c.Id == employee.DesignationId).Select(c => c.ReportingTo).FirstOrDefault();
            VMWrapperOtherManager response = new VMWrapperOtherManager
            {
                EmployeeId = Id,
                ReportingTo = employee.ManagerId,
                ManagersList = DbSet.Where(c => c.DesignationId == (decimal)ManagersId).Select(c => new VMGenericKeyValuePair<decimal> { Id = c.Id, Text = c.FirstName + c.LastName }).ToList(),
            };
            return response;
        }

        public decimal AddUpdateOtherManager(decimal EmployeeId, decimal ManagerId, string UpdatedBy, DateTime UpdatedDate)
        {
            decimal Results = 0; // Other Info Not Updated Successfully
            var employee = db.Employee.Where(c => c.Id == EmployeeId).FirstOrDefault();
            var ReportingDesgnId = db.Designation.Where(c => c.Id == employee.DesignationId).FirstOrDefault();
            var ManagerDesignId = db.Employee.Where(c => c.Id == ManagerId).FirstOrDefault();
            if (ReportingDesgnId.ReportingTo == ManagerDesignId.DesignationId)
            {
                if (ReportingDesgnId.Name == DesignationNames.SalesManager.ToString())
                {
                    employee.ZoneManagerId = ManagerId;
                }
                else if (ReportingDesgnId.Name == DesignationNames.SalesExecutive.ToString())
                {
                    employee.ZoneManagerId = ManagerDesignId.ZoneManagerId;
                }
                employee.UpdatedBy = UpdatedBy;
                employee.UpdatedDate = UpdatedDate;
                employee.ManagerId = ManagerId;
                DbSet.Update(employee);
                db.SaveChanges();
                Results = 1; // Other Info Saved Successfully
            }
            else
            {
                Results = -2; // Must be Report to Upper Authority
            }
            return Results;
        }
        public VMOnSavedRegionalManager AddUpdateRegionalManager(VMAddUpdateRegionalManager response)
        {
            VMOnSavedRegionalManager resp = new VMOnSavedRegionalManager();
            try
            {
                resp.Results = 0;
                if (response.AssignedCityList.Count() > 0)
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        var lists = response.AssignedCityList.Distinct().ToList();
                        response.AssignedCityList = null;
                        response.AssignedCityList = lists;
                        var employee = db.Employee.Where(c => c.Id == response.EmployeeId).FirstOrDefault();
                        var checkassigned = db.AssignedEmpAreas.Where(c => c.EmployeeId == employee.Id).ToList();
                        string CreatedBy = "";
                        DateTime CreatedDate = DateTime.Now;
                        if (checkassigned.Count() > 0)
                        {
                            CreatedBy = checkassigned.FirstOrDefault().CreatedBy;
                            CreatedDate = (DateTime)checkassigned.FirstOrDefault().CreatedDate;
                            db.AssignedEmpAreas.RemoveRange(checkassigned);
                            db.SaveChanges();
                        }
                        var CheckExistanceCity = db.AssignedEmpAreas.Where(c => response.AssignedCityList.Contains((decimal)c.CityId) && (c.ZoneId == null || c.ZoneId == 0)).Select(c => c.CityId).ToList();
                        if (CheckExistanceCity.Count() > 0)
                        {
                            resp.Results = -2; // Regional Manager Info Saved Successfully
                            var CheckExitance = db.City.Where(c => CheckExistanceCity.Contains(c.Id)).ToList();
                            StringBuilder citybuilder = new StringBuilder();
                            foreach (var item in CheckExitance)
                            {
                                citybuilder.Append(item.Name);
                                citybuilder.Append(",");
                            }
                            resp.CityExits = citybuilder.ToString();
                            transaction.Rollback();
                        }
                        else
                        {
                            employee.UpdatedBy = response.UpdatedBy;
                            employee.UpdatedDate = response.UpdatedDate;
                            employee.ManagerId = response.ManagerId;
                            DbSet.Update(employee);
                            List<AssignedEmpAreas> empAreas = new List<AssignedEmpAreas>();
                            foreach (var item in response.AssignedCityList)
                            {
                                AssignedEmpAreas assigned = new AssignedEmpAreas
                                {
                                    CityId = item,
                                    EmployeeId = response.EmployeeId,
                                    ZoneId = null,
                                    CreatedBy = !string.IsNullOrEmpty(CreatedBy) ? CreatedBy : response.UpdatedBy,
                                    CreatedDate = !string.IsNullOrEmpty(CreatedBy) ? CreatedDate : response.UpdatedDate,
                                    UpdatedBy = response.UpdatedBy,
                                    UpdatedDate = response.UpdatedDate,
                                };
                                empAreas.Add(assigned);
                                assigned = null;
                            }
                            db.AssignedEmpAreas.AddRange(empAreas);
                            db.SaveChanges();
                            resp.Results = 1; // Regional Manager Info Saved Successfully
                            transaction.Commit();
                        }

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return resp;
        }

        public int UpdateEmp(CreateUpdateEmployee instance)
        {
            int Results = 0;
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.EmployeeContact.Remove(db.EmployeeContact.Where(c => c.EmployeeId == instance.EmployeeId).FirstOrDefault());
                    db.EmployeeAddress.Remove(db.EmployeeAddress.Where(c => c.EmployeeId == instance.EmployeeId).FirstOrDefault());
                    db.SaveChanges();
                    var Employee = DbSet.Where(c => c.Id == instance.EmployeeId).FirstOrDefault();
                    var checkcategory = db.AssignedEmpCategory.Where(c => c.EmployeeId == Employee.Id).Count() > 0 ? true : false;
                    var checkarea = db.AssignedEmpAreas.Where(c => c.EmployeeId == Employee.Id).Count() > 0 ? true : false;

                    var DesigId = Employee.DesignationId;

                    Employee.FirstName = instance.FirstName;
                    Employee.LastName = instance.LastName;
                    Employee.Cnic = instance.Cnic;
                    Employee.UpdatedBy = instance.UpdatedBy;
                    Employee.UpdatedDate = instance.UpdatedDate;
                    Employee.DateOfBirth = instance.DateOfBirth;
                    if(Employee.DesignationId == instance.DesignationId)
                    {
                        Employee.DesignationId = instance.DesignationId;
                    }
                    else if(!checkcategory && !checkarea)
                    {
                        Employee.DesignationId = instance.DesignationId;
                    }
                    Employee.PassportNo = instance.PassportNo;
                    Employee.IsActive = instance.IsActive;
                    db.Entry(Employee).State = EntityState.Modified;
                    db.SaveChanges();
                    db.EmployeeAddress.Add(new EmployeeAddress
                    {
                        EmployeeId = Employee.Id,
                        EmpAddress = instance.EmpAddress,
                        CityAreaId = instance.CityAreaId,
                        ProvinceId = instance.ProvinceId,
                        CountryId = instance.CountryId,
                        CityId = instance.CityId,
                        CreatedBy = Employee.CreatedBy,
                        CreatedDate = Employee.CreatedDate,
                        UpdatedBy = instance.UpdatedBy,
                        UpdatedDate = instance.UpdatedDate
                    });
                    db.EmployeeContact.Add(new EmployeeContact
                    {
                        EmployeeId = Employee.Id,
                        ContactNo = instance.ContactNo,
                        PhoneNo = instance.PhoneNo,
                        CreatedBy = Employee.CreatedBy,
                        CreatedDate = Employee.CreatedDate,
                        UpdatedBy = instance.UpdatedBy,
                        UpdatedDate = instance.UpdatedDate
                    });
                    db.SaveChanges();


                    if (DesigId == instance.DesignationId)
                    {
                        var CheckRole = db.AspNetUserRoles.Where(c => c.UserId == instance.UserId).FirstOrDefault();
                        if (CheckRole != null)
                        {
                            db.AspNetUserRoles.Remove(CheckRole);
                            db.SaveChanges();

                            db.AspNetUserRoles.Add(new AspNetUserRoles { RoleId = instance.RoleId, UserId = instance.UserId });
                            db.SaveChanges();
                        }
                    }
                    else if (!checkcategory && !checkarea)
                    {
                        var CheckRole = db.AspNetUserRoles.Where(c => c.UserId == instance.UserId).FirstOrDefault();
                        if (CheckRole != null)
                        {
                            db.AspNetUserRoles.Remove(CheckRole);
                            db.SaveChanges();

                            db.AspNetUserRoles.Add(new AspNetUserRoles { RoleId = instance.RoleId, UserId = instance.UserId });
                            db.SaveChanges();
                        }
                    }
                    
                    transaction.Commit();
                    Results = 1;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return Results;
        }

        public VMOnSavedCategoryManager AddUpdateCategoryManager(VMAddUpdateCategoryManager response)
        {
            VMOnSavedCategoryManager resp = new VMOnSavedCategoryManager();
            try
            {
                resp.Results = 0;
                if (response.AssignedCategoryList.Count() > 0)
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        var lists = response.AssignedCategoryList.Distinct().ToList();
                        response.AssignedCategoryList = null;
                        response.AssignedCategoryList = lists;
                        var employee = db.Employee.Where(c => c.Id == response.EmployeeId).FirstOrDefault();
                        var checkassigned = db.AssignedEmpCategory.Where(c => c.EmployeeId == employee.Id).ToList();
                        string CreatedBy = "";
                        DateTime CreatedDate = DateTime.Now;
                        if (checkassigned.Count() > 0)
                        {
                            CreatedBy = checkassigned.FirstOrDefault().CreatedBy;
                            CreatedDate = (DateTime)checkassigned.FirstOrDefault().CreatedDate;
                            db.AssignedEmpCategory.RemoveRange(checkassigned);
                            db.SaveChanges();
                        }
                        var CheckExistanceCategory = db.AssignedEmpCategory.Where(c => response.AssignedCategoryList.Contains((decimal)c.CategoryId)).Select(c => c.CategoryId).ToList();
                        if (CheckExistanceCategory.Count() > 0)
                        {
                            resp.Results = -2; // Category Manager Info Saved Successfully
                            var CheckExitance = db.MainMenuCategory.Where(c => CheckExistanceCategory.Contains(c.Id)).ToList();
                            StringBuilder categorybuilder = new StringBuilder();
                            foreach (var item in CheckExitance)
                            {
                                categorybuilder.Append(item.Name);
                                categorybuilder.Append(",");
                            }
                            resp.CategoryExits = categorybuilder.ToString();
                            transaction.Rollback();
                        }
                        else
                        {
                            employee.UpdatedBy = response.UpdatedBy;
                            employee.UpdatedDate = response.UpdatedDate;
                            employee.ManagerId = response.ManagerId;
                            DbSet.Update(employee);
                            List<AssignedEmpCategory> empCategory = new List<AssignedEmpCategory>();
                            foreach (var item in response.AssignedCategoryList)
                            {
                                AssignedEmpCategory assigned = new AssignedEmpCategory
                                {
                                    CategoryId = item,
                                    EmployeeId = response.EmployeeId,
                                    CreatedBy = !string.IsNullOrEmpty(CreatedBy) ? CreatedBy : response.UpdatedBy,
                                    CreatedDate = !string.IsNullOrEmpty(CreatedBy) ? CreatedDate : response.UpdatedDate,
                                    UpdatedBy = response.UpdatedBy,
                                    UpdatedDate = response.UpdatedDate,
                                };
                                empCategory.Add(assigned);
                                assigned = null;
                            }
                            db.AssignedEmpCategory.AddRange(empCategory);
                            db.SaveChanges();
                            resp.Results = 1; // Category Manager Info Saved Successfully
                            transaction.Commit();
                        }

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return resp;
        }

        public VMOnSavedZoneManager AddUpdateZoneManager(VMAddUpdateZoneManager response)
        {
            VMOnSavedZoneManager resp = new VMOnSavedZoneManager();
            try
            {
                resp.Results = 0;
                if (response.AssignedZoneList.Count() > 0)
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        var lists = response.AssignedZoneList.Distinct().ToList();
                        response.AssignedZoneList = null;
                        response.AssignedZoneList = lists;
                        var employee = db.Employee.Where(c => c.Id == response.EmployeeId).FirstOrDefault();
                        var checkassigned = db.AssignedEmpAreas.Where(c => c.EmployeeId == employee.Id).ToList();
                        string CreatedBy = "";
                        DateTime CreatedDate = DateTime.Now;
                        if (checkassigned.Count() > 0)
                        {
                            CreatedBy = checkassigned.FirstOrDefault().CreatedBy;
                            CreatedDate = (DateTime)checkassigned.FirstOrDefault().CreatedDate;
                            db.AssignedEmpAreas.RemoveRange(checkassigned);
                            db.SaveChanges();
                        }
                        var CheckExistance = db.AssignedEmpAreas.Where(c => response.AssignedZoneList.Contains((decimal)c.ZoneId) && c.CityId == response.CityId).Select(c => c.ZoneId).ToList();
                        if (CheckExistance.Count() > 0)
                        {
                            resp.Results = -2; // Zone Manager Info Not Saved Successfully
                            var CheckExitance = db.Zones.Where(c => CheckExistance.Contains(c.Id)).ToList();
                            StringBuilder zonesbuilder = new StringBuilder();
                            foreach (var item in CheckExitance)
                            {
                                zonesbuilder.Append(item.Name);
                                zonesbuilder.Append(",");
                            }
                            resp.ZonesExits = zonesbuilder.ToString();
                            transaction.Rollback();
                        }
                        else
                        {
                            employee.UpdatedBy = response.UpdatedBy;
                            employee.UpdatedDate = response.UpdatedDate;
                            employee.ManagerId = response.ManagerId;
                            DbSet.Update(employee);
                            List<AssignedEmpAreas> empArea = new List<AssignedEmpAreas>();
                            foreach (var item in response.AssignedZoneList)
                            {
                                AssignedEmpAreas assigned = new AssignedEmpAreas
                                {
                                    ZoneId = item,
                                    EmployeeId = response.EmployeeId,
                                    CityId = response.CityId,
                                    CreatedBy = !string.IsNullOrEmpty(CreatedBy) ? CreatedBy : response.UpdatedBy,
                                    CreatedDate = !string.IsNullOrEmpty(CreatedBy) ? CreatedDate : response.UpdatedDate,
                                    UpdatedBy = response.UpdatedBy,
                                    UpdatedDate = response.UpdatedDate,
                                };
                                empArea.Add(assigned);
                                assigned = null;
                            }
                            db.AssignedEmpAreas.AddRange(empArea);
                            db.SaveChanges();
                            resp.Results = 1; // Zone Manager Info Saved Successfully
                            transaction.Commit();
                        }

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return resp;
        }


        public List<EmployeeCountStateReport> EmployeeReportings(string UserId)
        {
            return EmployeeReporting.EmployeeReportings(UserId);
        }
    }

}
