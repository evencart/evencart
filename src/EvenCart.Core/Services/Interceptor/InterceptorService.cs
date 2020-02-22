using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Infrastructure.Interceptor;
using EvenCart.Core.Infrastructure.Utils;

namespace EvenCart.Core.Services.Interceptor
{
    public class InterceptorService : IInterceptorService
    {
        public static IDictionary<string, IList<InterceptorAction>> Interceptors { get; private set; }

        static InterceptorService()
        {
            //init interceptors
            Interceptors = new ConcurrentDictionary<string, IList<InterceptorAction>>();

            var interceptorTasks = TypeFinder.ClassesOfType<IInterceptor>();
            var tasks =
                interceptorTasks.Select(iTask => (IInterceptor) DependencyResolver.Resolve(iTask)).ToList();
            var service = DependencyResolver.Resolve<IInterceptorService>();
            foreach (var task in tasks)
                task.SetupInterceptors(service);
        }

        public string LastError { get; set; }

        public bool Intercept(string interceptorLocationName, params object[] parameters)
        {
            var interceptors = Interceptors;
            if (!interceptors.ContainsKey(interceptorLocationName)) //no interceptor for this location exists. So return true to proceed
                return true;

            //get the methods at this location in ascending order
            var methods = interceptors[interceptorLocationName].OrderBy(x => x.Priority);
            //execute them one by one

            foreach (var method in methods)
            {
                try
                {
                    if (!method.Action(parameters))
                    {
                        this.LastError = $"An error occured while executing '{method.InterceptorName}'";
                        return false; //the method has returned false, so the interception has failed, return false
                    }
                }
                catch (System.Exception ex)
                {
                    this.LastError = ex.Message;
                    return false;
                }
               
            }
            //if we are here, everything work perfectly.
            return true;
        }

        public void SetInterceptor(InterceptorAction action)
        {
            var interceptors = Interceptors;
            foreach (var interceptorLocation in action.InterceptorLocations)
            {
                if (!interceptors.ContainsKey(interceptorLocation))
                    interceptors.Add(interceptorLocation, new List<InterceptorAction>());

                var existingInterceptor =
                    interceptors[interceptorLocation].FirstOrDefault(x => x.InterceptorName == action.InterceptorName);

                //if an interceptor with the same name exists, replace it
                if (existingInterceptor != null)
                {
                    interceptors[interceptorLocation].Remove(existingInterceptor);
                }

                //add this interceptor to the list
                interceptors[interceptorLocation].Add(action);
            }
          
        }
    }
}