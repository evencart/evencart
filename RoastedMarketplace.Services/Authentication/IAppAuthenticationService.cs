using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Enum;

namespace RoastedMarketplace.Services.Authentication
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