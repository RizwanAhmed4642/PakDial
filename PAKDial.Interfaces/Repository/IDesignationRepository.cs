using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository
{
    public interface IDesignationRepository : IBaseRepository<Designation,decimal>
    {
        DesignationResponse GetDesignations(DesignationRequestModel request);
        bool CheckExistance(Designation designation);
    }
}
