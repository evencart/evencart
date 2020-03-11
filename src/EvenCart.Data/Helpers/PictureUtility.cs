#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;
using System.Drawing;
using System.Linq;

namespace EvenCart.Data.Helpers
{
    public static class PictureUtility
    {

        //contentType is not always available 
        //that's why we manually update it here
        //http://www.sfsu.edu/training/mimetype.htm
        public static string GetContentType(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".bmp":
                    return "image/bmp";
                case ".gif":
                    return "image/gif";
                case ".jpeg":
                case ".jpg":
                case ".jpe":
                case ".jfif":
                case ".pjpeg":
                case ".pjp":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".tiff":
                case ".tif":
                    return "image/tiff";
                default:
                    return string.Empty;
            }
        }

        
        public static Size GetSize(string sizeString)
        {
            var sizeParts = sizeString.Split('x', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => Convert.ToInt32(x)).ToArray();
            if (sizeParts.Length != 2)
                throw new Exception($"Invalid size value {sizeString} in theme configuration");

            return new Size(sizeParts[0], sizeParts[1]);
        }
    }
}