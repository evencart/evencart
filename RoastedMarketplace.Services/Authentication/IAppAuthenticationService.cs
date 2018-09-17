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
        /// Signs out the current logged in user
        /// </summary>
        void SignOut();

        /// <summary>
        /// Creates an authentication ticket for the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isPersistent"></param>
        void CreateAuthenticationTicket(User user, bool isPersistent = false);

        /// <summary>
        /// Clears the authentication ticket of the user
        /// </summary>
        /// <param name="user"></param>
        void ClearAuthenticationTicket();

        /// <summary>
        /// Gets the current user
        /// </summary>
        /// <returns></returns>
        User GetCurrentUser();
    }
}