using EvenCart.Areas.Administration.Models.Gdpr;
using EvenCart.Data.Entity.Gdpr;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Gdpr
{
    public interface IGdprModelFactory : IModelFactory<Consent, ConsentModel>, IModelFactory<ConsentLog, ConsentLogModel>
    {
        
    }
}