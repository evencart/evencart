using RoastedMarketplace.Areas.Administration.Models.Cultures;
using RoastedMarketplace.Data.Entity.Cultures;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;

namespace RoastedMarketplace.Areas.Administration.Factories
{
    public interface ICurrencyModelFactory : IModelFactory<Currency, CurrencyModel>
    {
        
    }
}