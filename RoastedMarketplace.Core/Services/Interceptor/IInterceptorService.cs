#region Author Information
// IInterceptorService.cs
// 
// Author 2016 Apexol Technologies. All Rights Reserved.
// 
// Created On: 01 07 2016 08:26 PM
// Last Modified: 01 07 2016 08:26 PM
#endregion

using RoastedMarketplace.Core.Infrastructure.Interceptor;

namespace RoastedMarketplace.Core.Services.Interceptor
{
    public interface IInterceptorService
    {
        /// <summary>
        /// Returns the last error that occured while execution of interceptors
        /// </summary>
        string LastError { get; set; }

        /// <summary>
        /// Executes functions assigned at the provided location. Returns true if all the functions pass. Returns false if any one fails
        /// </summary>
        bool Intercept(string interceptorLocationName, params object[] parameters);

        /// <summary>
        /// Sets an interceptor 
        /// </summary>
        void SetInterceptor(InterceptorAction action);
    }
}