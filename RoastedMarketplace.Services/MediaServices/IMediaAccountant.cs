using RoastedMarketplace.Data.Entity.MediaEntities;

namespace RoastedMarketplace.Services.MediaServices
{
    public interface IMediaAccountant
    {
        string GetPictureUrl(int pictureId, int width = 0, int height = 0, bool returnDefaultIfNotFound = false);

        string GetPictureUrl(Media picture, int width = 0, int height = 0, bool returnDefaultIfNotFound = false);

        string GetVideoUrl(int mediaId);

        string GetVideoUrl(Media media);

        void WritePictureBytes(Media picture);

        void WriteVideoBytes(Media video);

        void WriteOtherMediaBytes(Media media);
    }
}