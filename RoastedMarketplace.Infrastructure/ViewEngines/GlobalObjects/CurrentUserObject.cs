using RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects.Implementations;
using RoastedMarketplace.Services.Extensions;

namespace RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects
{
    public class CurrentUserObject : GlobalObject
    {
        public override object GetObject()
        {
            var currentUser = ApplicationEngine.CurrentUser;
            var currentUserImpl = new CurrentUserImplementation()
            {
                Name = currentUser?.Name,
                IsVisitor = currentUser?.IsVisitor() ?? true,
                IsAdministrator = currentUser?.IsAdministrator() ?? false,
                Id = currentUser?.Id ?? 0,
                Email = currentUser?.Email ?? null,
                FirstName = currentUser?.FirstName ?? null,
                LastName = currentUser?.LastName ?? null,
            };
            return currentUserImpl;
        }
    }
}