using System.ComponentModel;

namespace EvenCart.Data.Enum
{
    public enum RegistrationMode
    {
        [Description("Immediate")]
        Immediate,
        [Description("With activation email to the user")]
        WithActivationEmail,
        [Description("Manually approve the registered user")]
        ManualApproval,
        [Description("Only users with invitation link will be allowed to register")]
        InviteOnly,
        [Description("Disable Registrations")]
        Disabled
    }
}