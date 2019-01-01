using System.ComponentModel;

namespace RoastedMarketplace.Data.Enum
{
    public enum RegistrationMode
    {
        [Description("Immediate")]
        Immediate,
        [Description("With activation email to the user")]
        WithActivationEmail,
        [Description("Manual")]
        ManualApproval
    }
}