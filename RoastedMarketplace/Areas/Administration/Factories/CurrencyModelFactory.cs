using RoastedMarketplace.Areas.Administration.Models.Cultures;
using RoastedMarketplace.Data.Entity.Cultures;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;

namespace RoastedMarketplace.Areas.Administration.Factories
{
    public class CurrencyModelFactory : ICurrencyModelFactory
    {
        private readonly IModelMapper _modelMapper;
        public CurrencyModelFactory(IModelMapper modelMapper)
        {
            _modelMapper = modelMapper;
        }

        public CurrencyModel Create(Currency entity)
        {
            var model = _modelMapper.Map<CurrencyModel>(entity);
            model.FlagUrl = ApplicationEngine.MapUrl($"~/common/flags/{entity.Flag}");
            return model;
        }
    }
}