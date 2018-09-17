using System;

namespace RoastedMarketplace.Core.Infrastructure.Utils
{
    public class Singleton<T> where T: class
    {
        private static T instance;
        private static readonly object padlock = new object();

        public static T Instance
        {
            get
            {
                lock (padlock)
                {
                    return instance ?? (instance = Activator.CreateInstance<T>());
                }
            }
        } 
    }
}