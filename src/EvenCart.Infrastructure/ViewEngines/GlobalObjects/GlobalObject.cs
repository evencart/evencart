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