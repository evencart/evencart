using EvenCart.Core.Services.Interceptor;

namespace EvenCart.Core.Infrastructure.Interceptor
{
    public interface IInterceptor
    {
        void SetupInterceptors(IInterceptorService interceptorService);
    }
}