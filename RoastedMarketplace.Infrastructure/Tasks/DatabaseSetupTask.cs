using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Core.Startup;
using RoastedMarketplace.Data.Database;

namespace RoastedMarketplace.Infrastructure.Tasks
{
    public class DatabaseSetupTask : IStartupTask
    {

        public void Run()
        {

            if (!DatabaseManager.IsDatabaseInstalled(DependencyResolver.Resolve<IDatabaseSettings>()))
                return;
        }

        public int Priority
        {
            get { return -int.MaxValue; } //should be the first task ever
        }
    }
}