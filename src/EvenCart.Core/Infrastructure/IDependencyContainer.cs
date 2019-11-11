using DryIoc;

namespace EvenCart.Core.Infrastructure
{
    public interface IDependencyContainer
    {
        void RegisterDependencies(IRegistrator registrar);

        void RegisterDependenciesIfActive(IRegistrator registrar);

        int Priority { get; }
    }
}