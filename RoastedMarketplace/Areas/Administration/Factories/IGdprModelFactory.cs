using RoastedMarketplace.Areas.Administration.Models.Gdpr;
using RoastedMarketplace.Data.Entity.Gdpr;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;

namespace RoastedMarketplace.Areas.Administration.Factories
{
    public interface IGdprModelFactory : IModelFactory<Consent, ConsentModel>, IModelFactory<ConsentLog, ConsentLogModel>
    {
        
    }
}