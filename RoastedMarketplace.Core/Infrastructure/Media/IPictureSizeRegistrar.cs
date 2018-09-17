using System.Collections.Generic;

namespace RoastedMarketplace.Core.Infrastructure.Media
{
    public interface IPictureSizeRegistrar
    {
        void RegisterPictureSize(IList<PictureSize> pictureSizes);
    }
}