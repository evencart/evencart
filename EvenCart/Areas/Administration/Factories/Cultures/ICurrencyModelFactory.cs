using EvenCart.Areas.Administration.Models.Cultures;
using EvenCart.Data.Entity.Cultures;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Cultures
{
    public interface ICurrencyModelFactory : IModelFactory<Currency, CurrencyModel>
    {
        
    }
}