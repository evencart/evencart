using EvenCart.Areas.Administration.Models.System;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.System
{
    public interface IAboutModelFactory : IModelFactory
    {
        AboutModel Create();
    }
}