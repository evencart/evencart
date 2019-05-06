using DryIoc;

namespace EvenCart.Core.Infrastructure
{
    public interface IDependencyContainer
    {
        void RegisterDependencies(IRegistrator registrar);

        int Priority { get; }
    }
}