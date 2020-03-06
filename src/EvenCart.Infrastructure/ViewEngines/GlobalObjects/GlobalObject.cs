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

using System;
using System.Collections.Concurrent;
using EvenCart.Data.Database;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Infrastructure.ViewEngines.GlobalObjects
{
    public abstract class GlobalObject : FoundationModel
    {
        public abstract object GetObject();

        public abstract bool RenderInAdmin { get; }

        public abstract bool RenderInPublic { get; }

        public static readonly ConcurrentDictionary<string, GlobalObject> RegisteredObjects = new ConcurrentDictionary<string, GlobalObject>();

        public static void RegisterObject<T>(string key) where T : GlobalObject
        {
            if (!DatabaseManager.IsDatabaseInstalled())
                return;
            if(!RegisteredObjects.ContainsKey(key))
                RegisteredObjects.TryAdd(key, Activator.CreateInstance<T>());
        }

        public static object ExecuteObject(string key)
        {
            if (!RegisteredObjects.ContainsKey(key))
                return null;
            var globalObj = RegisteredObjects[key];
            var isAdmin = ApplicationEngine.IsAdmin();
            if (isAdmin && !globalObj.RenderInAdmin)
                return null;
            if (!isAdmin && !globalObj.RenderInPublic)
                return null;
            return globalObj.GetObject();
        }
    }
}