using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;

namespace EvenCart.Services.Authentication
{
    public interface IAppAuthenticationService
    {
        /// <summary>
        /// Signs in the user
        /// </summary>
        LoginStatus SignIn(string email, string name = "", bool isPersistent = false,  bool forceCreateNewAccount = false);

        /// <summary>
        /// Signs in the user
        /// </summary>
        LoginStatus SignIn(string authenticationScheme, string email, string name = "", bool isPersistent = false, bool forceCreateNewAccount = false);

        /// <summary>
        /// Signs in the user in imitation mode
        /// </summary>
        LoginStatus ImitationModeSignIn(string targetUserEmail, string imitatorEmail);

        /// <summary>
        /// Signs out the current logged in user
        /// </summary>
        void SignOut();

        /// <summary>
        /// Signs in the visitor as guest
        /// </summary>
        /// <returns></returns>
        LoginStatus GuestSignIn();
      

        /// <summary>
        /// Gets the current user
        /// </summary>
        /// <returns></returns>
        User GetCurrentUser();
    }
}