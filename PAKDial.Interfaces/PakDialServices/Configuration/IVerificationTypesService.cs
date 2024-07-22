using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels.Configuration;
using PAKDial.Domains.ResponseModels.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.Configuration
{
    public interface IVerificationTypesService
    {
        decimal Update(VerificationTypes Instance);
        decimal Add(VerificationTypes Instance);
        decimal Delete(int Id);
        VerificationTypes GetById(int Id);
        VerificationTypes GetByName(string Name);
        IEnumerable<VerificationTypes> GetAll();
        VerificationTypesResponse GetVerificationTypes(VerificationTypesRequestModel request);
        int ImageUpdate(decimal Id, string ImagePath, string AbsolutePath);
    }
}
