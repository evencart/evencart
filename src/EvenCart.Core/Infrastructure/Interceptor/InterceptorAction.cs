using System.Collections.Generic;

namespace EvenCart.Core.Infrastructure.Interceptor
{
    public class InterceptorAction
    {
        public string InterceptorName { get; set; }

        public int Priority { get; set; }

        public IList<string> InterceptorLocations { get; set; }

        public InterceptorFunc Action { get; set; }

        public string Error { get; set; }
    }

    public delegate bool InterceptorFunc(params object[] parameters);
}