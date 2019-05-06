using EvenCart.Areas.Administration.Models.Cultures;
using EvenCart.Data.Entity.Cultures;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Cultures
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