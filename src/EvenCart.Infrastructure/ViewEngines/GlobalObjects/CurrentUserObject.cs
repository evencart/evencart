#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System.Linq;
using EvenCart.Core.Extensions;
using EvenCart.Data.Constants;
using EvenCart.Data.Extensions;
using EvenCart.Services.Extensions;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.ViewEngines.GlobalObjects.Implementations;

namespace EvenCart.Infrastructure.ViewEngines.GlobalObjects
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
                IsAdministrator = currentUser?.Can(CapabilitySystemNames.AccessAdministration) ?? false,
                Id = currentUser?.Id ?? 0,
                Email = currentUser?.Email ?? null,
                FirstName = currentUser?.FirstName ?? null,
                LastName = currentUser?.LastName ?? null,
                IsImitator = !currentUser?.GetMeta<string>(ApplicationConfig.ImitatorKey).IsNullEmptyOrWhiteSpace() ?? false
            };
            if (currentUserImpl.Name.IsNullEmptyOrWhiteSpace())
                currentUserImpl.Name = currentUserImpl.Email ??
                                       (LocalizationHelper.Localize("Imitated User # ",
                                           ApplicationEngine.CurrentLanguage.CultureCode) + currentUser?.Id);
            if (currentUserImpl.FirstName.IsNullEmptyOrWhiteSpace())
                currentUserImpl.FirstName = currentUserImpl.Name;

            if (currentUserImpl.IsAdministrator)
            {
               currentUserImpl.Capabilities = currentUser?.Capabilities?.Select(x => x.Name).ToList();
            }
            return currentUserImpl;
        }

        public override bool RenderInAdmin => true;

        public override bool RenderInPublic => true;
    }
}