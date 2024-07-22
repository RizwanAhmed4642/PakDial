using PAKDial.Interfaces.PakDialServices;
using PAKDial.Interfaces.Repository;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PAKDial.Implementation.PakDialServices
{
    public class DesignationService : IDesignationService
    {
        private readonly IDesignationRepository designationRepository;
        private readonly IEmployeeRepository employeeRepository;

        public DesignationService(IDesignationRepository designationRepository,
            IEmployeeRepository employeeRepository)
        {
            this.designationRepository = designationRepository;
            this.employeeRepository = employeeRepository;
        }
        public int Add(Designation instance)
        {
            int Save = 0; //"Designation Not Added";
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            var CheckExistance = designationRepository.CheckExistance(instance);
            if (!CheckExistance)
            {
                designationRepository.Add(instance);
                Save = designationRepository.SaveChanges(); //1 Designation Added Successfully
            }
            else
            {
                Save = -2; // Designation Already Exist.
            }
            return Save;
        }
        public int Update(Designation instance)
        {
            int Result = 0; //Record Not Updated Successfully
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            bool result = designationRepository.CheckExistance(instance);
            if (!result)
            {
                var Editdesignation = FindById(instance.Id);
                Editdesignation.Name = instance.Name;
                Editdesignation.UpdatedBy = instance.UpdatedBy;
                Editdesignation.UpdatedDate = instance.UpdatedDate;
                Editdesignation.ReportingTo = instance.ReportingTo;
                designationRepository.Update(Editdesignation);
                Result = designationRepository.SaveChanges(); //Record Updated Successfully
            }
            else
            {
                Result = -2; //"Designation Already Exits";
            }
            return Result;
        }
        public int Delete(decimal Id)
        {
            int Results = 0; //"Designation Not Deleted."
            var checkStates = employeeRepository.EmployeeDesignationExits(Id);
            if (!checkStates && GetAll().ToList().Where(c=>c.ReportingTo == Id).Count() < 1)
            {
                designationRepository.Delete(FindById(Id));
                Results = designationRepository.SaveChanges(); // Designation Deleted Successfully.
            }
            else
            {
                Results = -2; // Please Delete its Employees First and Child Designation.
            }
            return Results;
        }
        public int AddRange(List<Designation> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            designationRepository.AddRange(instance);
            return designationRepository.SaveChanges();
        }
        public int UpdateRange(List<Designation> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            designationRepository.UpdateRange(instance);
            return designationRepository.SaveChanges();
        }
        public int DeleteRange(List<Designation> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            designationRepository.DeleteRange(instance);
            return designationRepository.SaveChanges();
        }
        public Designation FindById(decimal id)
        {
            return designationRepository.Find(id);
        }
        public IEnumerable<Designation> GetAll()
        {
            return designationRepository.GetAll();
        }

        public DesignationResponse GetDesignations(DesignationRequestModel request)
        {
            return designationRepository.GetDesignations(request);
        }
    }
}
