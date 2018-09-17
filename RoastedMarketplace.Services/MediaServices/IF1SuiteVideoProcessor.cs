using RoastedMarketplace.Core.Infrastructure.Media;

namespace RoastedMarketplace.Services.MediaServices
{
    public interface IRoastedMarketplaceVideoProcessor
    {
        void WriteVideoThumbnailPicture(string videoFilePath, string imageFilePath);

        void WriteVideoThumbnailPicture(string videoFilePath, string imageFilePath, PictureSize size);

        void WriteVideoThumbnailPicture(string videoFilePath, string imageFilePath, float? frameTime);

        void WriteVideoThumbnailPicture(string videoFilePath, string imageFilePath, PictureSize size, float? frameTime);
    }
}