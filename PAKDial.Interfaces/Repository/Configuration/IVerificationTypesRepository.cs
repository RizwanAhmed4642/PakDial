using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels.Configuration;
using PAKDial.Domains.ResponseModels.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.Configuration
{
    public interface IVerificationTypesRepository : IBaseRepository<VerificationTypes, decimal>
    {
        decimal UpdateVerificationTypes(VerificationTypes Instance);
        decimal AddVerificationTypes(VerificationTypes Instance);
        decimal DeleteVerificationTypes(int Id);
        VerificationTypesResponse GetVerificationTypes(VerificationTypesRequestModel request);
        VerificationTypes GetByName(string Name);
        bool CheckExistance(VerificationTypes instance);
        
    }
}
