namespace RoastedMarketplace.Core.Startup
{
    public interface IStartupTask
    {
        /// <summary>
        /// Runs a startup task
        /// </summary>
        void Run();

        /// <summary>
        /// The priority of task. Lower means earlier in pipeline
        /// </summary>
        int Priority { get; }
    }
}