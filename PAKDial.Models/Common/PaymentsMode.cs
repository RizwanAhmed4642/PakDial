using System.ComponentModel;

namespace PAKDial.Domains.Common
{
    public enum PaymentsMode
    {
        [Description("Cash")]
        Cash = 1,
        [Description("Visa Card")]
        VisaCard = 2,
        [Description("Credit Card")]
        CreditCard = 3,
        [Description("Master Card")]
        MasterCard = 4,
        [Description("Debit Card")]
        DebitCard = 5,
        [Description("Cheque")]
        Cheque = 6,
        [Description("Money Order")]
        MoneyOrder = 7,
        [Description("PayOrder")]
        PayOrder = 8,
        [Description("Online Payment")]
        OnlinePayment = 9
    }
}
