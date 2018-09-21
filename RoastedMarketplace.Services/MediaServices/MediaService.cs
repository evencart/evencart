using DotEntity;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.MediaEntities;
using RoastedMarketplace.Data.Entity.Shop;

namespace RoastedMarketplace.Services.MediaServices
{
    public class MediaService : FoundationEntityService<Media>, IMediaService
    {
        public override void Delete(Media entity)
        {
            //delete the product media
            EntitySet<ProductMedia>.Delete(x => x.MediaId == entity.Id);
            //proceed with usual deletion
            base.Delete(entity);
        }
    }
}