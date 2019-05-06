using System;
using System.Threading.Tasks;

namespace EvenCart.Core.Tasks
{
    public interface ITask : IDisposable
    {
        Task Run();

        string SystemName { get; }

        string Name { get; }

        int DefaultCycleDurationInSeconds { get; }
    }
}