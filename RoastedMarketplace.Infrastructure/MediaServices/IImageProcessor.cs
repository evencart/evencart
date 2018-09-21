namespace RoastedMarketplace.Infrastructure.MediaServices
{
    public interface IImageProcessor
    {
        byte[] ResizeImage(byte[] imageBytes, int width, int height);
    }
}