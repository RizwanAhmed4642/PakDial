using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAKDial.ZongServices.SmsService
{
    public interface IService
    {
        Task<string> SendMessageAsync(string PhoneNo, string Message);
    }
}
