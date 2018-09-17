using System;
using System.Collections.Generic;
using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects
{
    public abstract class GlobalObject : FoundationModel
    {
        public abstract object GetObject();

        public static readonly Dictionary<string, GlobalObject> RegisteredObjects = new Dictionary<string, GlobalObject>();

        public static void RegisterObject<T>(string key) where T : GlobalObject
        {
            if(!RegisteredObjects.ContainsKey(key))
                RegisteredObjects.Add(key, Activator.CreateInstance<T>());
        }
    }
}