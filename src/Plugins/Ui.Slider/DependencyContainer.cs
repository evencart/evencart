using DryIoc;
using EvenCart.Core.Infrastructure;
using Ui.Slider.Services;

namespace Ui.Slider
{
    public class DependencyContainer : IDependencyContainer
    {
        public void RegisterDependencies(IRegistrator registrar)
        {
            registrar.Register<IUiSliderService, UiSliderService>(Reuse.ScopedOrSingleton);
        }

        public void RegisterDependenciesIfActive(IRegistrator registrar)
        {
            
        }

        public int Priority { get; } = 0;
    }
}