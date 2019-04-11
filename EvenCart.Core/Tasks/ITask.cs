using System;

namespace EvenCart.Core.Tasks
{
    public interface ITask : IDisposable
    {
        void Run();

        string SystemName { get; }

        string Name { get; }

        int DefaultCycleDurationInSeconds { get; }
    }
}