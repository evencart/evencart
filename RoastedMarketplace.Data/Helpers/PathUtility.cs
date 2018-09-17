using System.IO;

namespace RoastedMarketplace.Data.Helpers
{
    public class PathUtility
    {
        /// <summary>
        /// Gets a file path that's safe to write to file system
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="fileName"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        public static string GetFileSavePath(string directoryPath, string fileName, bool overwrite = false)
        {
            var filePath = Path.Combine(directoryPath, fileName);
            if (!File.Exists(filePath) || overwrite)
                //file with this name doesn't exist, so it's safe to return this path
                return filePath;

            //get raw file name
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            //get it's extension
            var fileExtension = Path.GetExtension(filePath);

            var newFullPath = filePath;
            //the filename will be prefixed with count if a file of same name exists already
            var count = 1;
            while (File.Exists(newFullPath))
            {
                var tempFileName = $"{fileNameWithoutExtension}_{count++}";
                newFullPath = Path.Combine(directoryPath, tempFileName + fileExtension);
            }
            //return this file name
            return newFullPath;

        }

        public static string GetFileExtensionFromContentType(string contentType)
        {
            switch (contentType)
            {
                case "image/bmp":
                    return ".bmp";
                case "image/gif":
                    return ".gif";
                case "image/jpeg":
                    return ".jpg";
                case "image/png":
                    return ".png";
                case "image/tiff":
                    return ".tiff";
                case "image/x-icon":
                    return ".ico";
                case "video/x-flv":
                    return ".flv";
                case "video/mp4":
                    return ".mp4";
                case "video/3gpp":
                    return ".3gp";
                case "video/quicktime":
                    return ".mov";
                case "video/x-msvideo":
                    return ".avi";
                case "video/x-ms-wmv":
                    return ".wmv";
            }

            return "";
        }
    }
}
