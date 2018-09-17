using DryIoc;

namespace RoastedMarketplace.Core.Infrastructure
{
    public interface IDependencyContainer
    {
        void RegisterDependencies(IRegistrator registrar);

        int Priority { get; }
    }
}