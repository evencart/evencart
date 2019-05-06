using EvenCart.Areas.Administration.Models.Gdpr;
using EvenCart.Data.Entity.Gdpr;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Gdpr
{
    public class GdprModelFactory : IGdprModelFactory
    {
        private readonly IModelMapper _modelMapper;

        public GdprModelFactory(IModelMapper modelMapper)
        {
            _modelMapper = modelMapper;
        }

        public ConsentModel Create(Consent entity)
        {
            var model = _modelMapper.Map<ConsentModel>(entity);
            if (entity.ConsentGroup != null)
            {
                model.ConsentGroup = _modelMapper.Map<ConsentGroupModel>(entity.ConsentGroup);
            }
            return model;
        }

        public ConsentLogModel Create(ConsentLog entity)
        {
            var model = new ConsentLogModel()
            {
                ActivityType = entity.ActivityType,
                ConsentTitle = entity.Consent.Title,
                CreatedOn = entity.CreatedOn,
                UserInfo = entity.EncryptedUserInfo
            };
            return model;
        }
    }
}