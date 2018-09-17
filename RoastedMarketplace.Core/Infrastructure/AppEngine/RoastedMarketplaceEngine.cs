using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DryIoc;
using Microsoft.Extensions.DependencyInjection;
using RoastedMarketplace.Core.Exception;
using RoastedMarketplace.Core.Infrastructure.Interceptor;
using RoastedMarketplace.Core.Infrastructure.Media;
using RoastedMarketplace.Core.Infrastructure.Utils;
using RoastedMarketplace.Core.Modules;
using RoastedMarketplace.Core.Startup;
using RoastedMarketplace.Core.Tasks;

namespace RoastedMarketplace.Core.Infrastructure.AppEngine
{
    [Obsolete]
    public class RoastedMarketplaceEngine : IAppEngine
    {
        public IContainer IocContainer { get; private set; }

        public IServiceCollection  Services { get; private set; }

        public static IList<PictureSize> PictureSizes { get; private set; }

        public static IList<Type> ScheduledTasks { get; set; }

        public IDictionary<string, IList<InterceptorAction>> Interceptors { get; private set; }

        public T Resolve<T>(bool returnDefaultIfNotResolved = false) where T : class
        {
            return IocContainer.Resolve<T>(returnDefaultIfNotResolved ? IfUnresolved.ReturnDefault : IfUnresolved.Throw);
        }

        public T RegisterAndResolve<T>(object instance = null, bool instantiateIfNull = true, IReuse reuse = null) where T : class
        {
            if (instance == null)
                if (instantiateIfNull)
                    instance = Activator.CreateInstance<T>();
                else
                {
                    throw new RoastedMarketplaceException("Can't register a null instance");
                }
            var typedInstance = Resolve<T>(true);
            if (typedInstance == null)
                IocContainer.RegisterInstance<T>(instance as T, reuse);
            return instance as T;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Start(IServiceCollection services)
        {
            //setup ioc container
            SetupContainer();

            //run startup tasks
            RunStartupTasks();

            //start task manager to run scheduled tasks
            StartTaskManager();

            //setup interceptors
            SetupInterceptors();

        }

        public void Reset()
        {
            //HttpRuntime.UnloadAppDomain();
        }

        public void SetupContainer()
        {
            IocContainer = new Container(rules => rules.WithoutThrowIfDependencyHasShorterReuseLifespan());
        }

        private void RunStartupTasks()
        {
            var startupTasks = TypeFinder.ClassesOfType<IStartupTask>();
            var tasks =
                startupTasks.Select(startupTask => (IStartupTask)Activator.CreateInstance(startupTask)).ToList();

            //reorder according to prioiryt
            tasks = tasks.OrderBy(x => x.Priority).ToList();

            foreach (var task in tasks)
                task.Run();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void StartTaskManager()
        {
            var taskManager = ActiveEngine.Resolve<ITaskManager>();
            var tasks = TypeFinder.ClassesOfType<ITask>();
            ScheduledTasks = tasks;
            taskManager?.Start(tasks.ToArray());
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void SetupPictureSizes(bool testMode = false)
        {
            if (PictureSizes != null && PictureSizes.Count > 0)
                return; //already registered
            PictureSizes = PictureSizes ?? new List<PictureSize>();
            if (testMode)
                return;
            var allPictureSizeRegistrars = TypeFinder.ClassesOfType<IPictureSizeRegistrar>();
            var allPictureSizeInstances =
                allPictureSizeRegistrars.Select(x => (IPictureSizeRegistrar)Activator.CreateInstance(x));

            foreach (var sizeInstance in allPictureSizeInstances)
                sizeInstance.RegisterPictureSize(PictureSizes);
        }

        private void SetupInterceptors()
        {
            
            
        }

        public static RoastedMarketplaceEngine ActiveEngine
        {
            get { return Singleton<RoastedMarketplaceEngine>.Instance; }
        }
    }
}