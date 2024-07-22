using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZongSmsService;

namespace PAKDial.ZongServices.SmsService
{
    public class SendSmsService : IService
    {
        private string LoginId { get; set; }
        private string Password { get; set; }
        private string Masks { get; set; }
        private string UniCode { get; set; }
        private string ShortCodePrefered { get; set; }

        public SendSmsService()
        {
            LoginId = "923114516938";
            Password = "Zong@123";
            Masks = "PAKDIAL";
            UniCode = "0";
            ShortCodePrefered = "n";
        }


        public async Task<string> SendMessageAsync(string PhoneNo , string Message)
        {
            try
            {
                CorporateCBSClient client = new CorporateCBSClient(CorporateCBSClient.EndpointConfiguration.BasicHttpBinding_ICorporateCBS1);
                QuickSMSResquest quick = new QuickSMSResquest
                {
                    loginId = LoginId,
                    loginPassword = Password,
                    Destination = PhoneNo,
                    Mask = Masks,
                    Message = Message,
                    UniCode = UniCode,
                    ShortCodePrefered = ShortCodePrefered,
                };
                string message = await client.QuickSMSAsync(quick);
                await client.CloseAsync();
                return message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
