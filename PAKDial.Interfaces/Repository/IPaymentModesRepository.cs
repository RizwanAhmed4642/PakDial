using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository
{
    public interface IPaymentModesRepository : IBaseRepository<PaymentModes, decimal>
    {
        PaymentModesResponse GetPaymentModes(PaymentModesRequestModel request);
        bool CheckExistance(PaymentModes instance);
        GetPaymentModesResponse GetPaymentModesList(PaymentModesRequestModel request);
        void UploadImages(decimal Id, string ImagePath, string AbsolutePath);
    }
}
