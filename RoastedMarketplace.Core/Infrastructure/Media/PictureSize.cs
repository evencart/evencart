using System;
using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Core.Exception;

namespace RoastedMarketplace.Core.Infrastructure.Media
{
    /// <summary>
    /// Used for storing a single image size used throughout the application
    /// </summary>
    public class PictureSize : IDisposable
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public string Name { get; set; }

        public void Dispose()
        {
                
        }
    }

    /// <summary>
    /// Extensions for picture size.
    /// </summary>
    /// TODO: Move this class to a separate file
    public static class PictureSizeExtensions
    {
        /// <summary>
        /// Maps a new size to picture sizes collection
        /// </summary>
        /// <param name="pictureSizes"></param>
        /// <param name="size"></param>
        public static void MapSize(this IList<PictureSize> pictureSizes, PictureSize size)
        {
            if(pictureSizes == null)
                throw new RoastedMarketplaceException("Can't map size to null collection");

            pictureSizes.Add(size);
        }

        /// <summary>
        /// Overrides existing size with name as provided size with new values. Adds if the size doesn't exist
        /// </summary>
        public static void UpdateOrInsertSize(this IList<PictureSize> pictureSizes, PictureSize size)
        {
            if (pictureSizes == null)
                throw new RoastedMarketplaceException("Can't map size to null collection");
            var pictureSize = pictureSizes.FirstOrDefault(x => x.Name == size.Name);
            if (pictureSize == null)
                MapSize(pictureSizes, size);
            else
            {
                //assign new values
                pictureSize.Width = size.Width;
                pictureSize.Height = size.Height;
                //clear resource
                size.Dispose();
            }
        }
        /// <summary>
        /// Overrides existing size with name as provided size with new values. Adds if the size doesn't exist
        /// </summary>
        public static void UpdateOrInsertSize(this IList<PictureSize> pictureSizes, string sizeName, int newWidth, int newHeight)
        {
            if (pictureSizes == null)
                throw new RoastedMarketplaceException("Can't map size to null collection");
            var pictureSize = pictureSizes.FirstOrDefault(x => x.Name == sizeName);
            if (pictureSize == null)
                MapSize(pictureSizes, new PictureSize()
                {
                    Name = sizeName,
                    Width = newWidth,
                    Height = newHeight
                });
            else
            {
                //assign new values
                pictureSize.Width = newWidth;
                pictureSize.Height = newHeight;
            }
        }
    }
}
