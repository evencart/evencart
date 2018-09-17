using RoastedMarketplace.Core.Infrastructure.Media;
using NReco.VideoConverter;

namespace RoastedMarketplace.Services.MediaServices
{
    public class RoastedMarketplaceVideoProcessor : IRoastedMarketplaceVideoProcessor
    {
        public void WriteVideoThumbnailPicture(string videoFilePath, string imageFilePath)
        {
            WriteVideoThumbnailPicture(videoFilePath, imageFilePath, null, null);
        }

        public void WriteVideoThumbnailPicture(string videoFilePath, string imageFilePath, PictureSize size)
        {
            WriteVideoThumbnailPicture(videoFilePath, imageFilePath, size, null);
        }

        public void WriteVideoThumbnailPicture(string videoFilePath, string imageFilePath, float? frameTime)
        {
            WriteVideoThumbnailPicture(videoFilePath, imageFilePath, null, frameTime);
        }

        public void WriteVideoThumbnailPicture(string videoFilePath, string imageFilePath, PictureSize size, float? frameTime)
        {
            var ffmpeg = new FFMpegConverter();
            ffmpeg.GetVideoThumbnail(videoFilePath, imageFilePath, frameTime);
        }
    }
}