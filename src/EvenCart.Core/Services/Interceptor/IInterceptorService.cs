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

using EvenCart.Core.Infrastructure.Interceptor;

namespace EvenCart.Core.Services.Interceptor
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