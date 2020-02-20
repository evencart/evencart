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