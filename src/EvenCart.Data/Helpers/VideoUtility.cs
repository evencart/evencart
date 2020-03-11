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

namespace EvenCart.Data.Helpers
{
    public static class VideoUtility
    {
        //Used from here
        //http://help.encoding.com/knowledge-base/article/correct-mime-types-for-serving-video-files/

        public static string GetContentType(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".flv":
                    return "video/x-flv";
                case ".mp4":
                    return "video/mp4";
                case ".3gp":
                    return "video/3gpp";
                case ".mov":
                    return "video/quicktime";
                case ".avi":
                    return "video/x-msvideo";
                case ".wmv":
                    return "video/x-ms-wmv";
                default:
                    return string.Empty;
            }
        }
    }
}
