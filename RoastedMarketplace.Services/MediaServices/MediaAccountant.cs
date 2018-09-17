using RoastedMarketplace.Data.Entity.MediaEntities;

namespace RoastedMarketplace.Services.MediaServices
{
    public class MediaAccountant : IMediaAccountant
    {
        public string GetPictureUrl(int pictureId, int width = 0, int height = 0, bool returnDefaultIfNotFound = false)
        {
            throw new System.NotImplementedException();
        }

        public string GetPictureUrl(Media picture, int width = 0, int height = 0, bool returnDefaultIfNotFound = false)
        {
            throw new System.NotImplementedException();
        }

        public string GetVideoUrl(int mediaId)
        {
            throw new System.NotImplementedException();
        }

        public string GetVideoUrl(Media media)
        {
            throw new System.NotImplementedException();
        }

        public void WritePictureBytes(Media picture)
        {
            throw new System.NotImplementedException();
        }

        public void WriteVideoBytes(Media video)
        {
            throw new System.NotImplementedException();
        }

        public void WriteOtherMediaBytes(Media media)
        {
            throw new System.NotImplementedException();
        }
    }
}