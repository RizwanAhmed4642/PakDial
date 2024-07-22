using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System.Collections.Generic;

namespace PAKDial.Interfaces.PakDialServices
{
    public interface IDesignationService
    {
        int Update(Designation instance);
        int Delete(decimal Id);
        int Add(Designation instance);
        int UpdateRange(List<Designation> instance);
        int DeleteRange(List<Designation> instance);
        int AddRange(List<Designation> instance);
        Designation FindById(decimal id);
        IEnumerable<Designation> GetAll();
        DesignationResponse GetDesignations(DesignationRequestModel request);


    }
}
