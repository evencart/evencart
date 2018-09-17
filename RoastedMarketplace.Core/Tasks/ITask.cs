using System;

namespace RoastedMarketplace.Core.Tasks
{
    public interface ITask : IDisposable
    {
        void Run();

        string SystemName { get; }

        string Name { get; }

        int DefaultCycleDurationInSeconds { get; }
    }
}