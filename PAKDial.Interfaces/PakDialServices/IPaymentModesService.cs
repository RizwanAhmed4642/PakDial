using Microsoft.AspNetCore.Http;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices
{
    public interface IPaymentModesService
    {
        int Update(PaymentModes instance);
        int Delete(decimal Id);
        decimal Add(PaymentModes instance);
        PaymentModes FindById(decimal id);
        IEnumerable<PaymentModes> GetAll();
        PaymentModesResponse GetPaymentModes(PaymentModesRequestModel request);
        GetPaymentModesResponse GetPaymentModesList(PaymentModesRequestModel request);
        int UploadIcons(decimal Id, IFormFile file, string AbsolutePath);

    }
}
