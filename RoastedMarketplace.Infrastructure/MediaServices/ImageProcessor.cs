using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

namespace RoastedMarketplace.Infrastructure.MediaServices
{
    public class ImageProcessor : IImageProcessor
    {
        public byte[] ResizeImage(byte[] imageBytes, int width, int height)
        {
            using (var image = Image.Load(imageBytes, out IImageFormat imageFormat))
            using (var outStream = new MemoryStream())
            {
                
                if (width == 0)
                    width = image.Width;
                if (height == 0)
                    height = image.Height;
                image.Mutate(x => x
                    .Resize(new ResizeOptions()
                    {
                        Size = new Size(width, height),
                        Mode = ResizeMode.Pad
                    })
                    .BackgroundColor(Rgba32.White));
                
                IImageEncoder encoder = null;
                switch (imageFormat.Name) {
                    case "PNG":
                        encoder = new PngEncoder();
                        break;
                    case "GIF":
                        encoder = new GifEncoder();
                        break;
                    case "BMP":
                        encoder = new BmpEncoder();
                        break;
                    default:
                        encoder = new JpegEncoder();
                        break;
                }

                image.Save(outStream, encoder);
                return outStream.ToArray();
            }
        }
    }
}