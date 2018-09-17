using System;

namespace RoastedMarketplace.Services.Helpers
{
    public class FileHelpers
    {
        public static string GetPictureFileNameWithSize(string pictureFileName, int width, int height)
        {
            //filename.jpg
            var fileName = pictureFileName;
            if (string.IsNullOrEmpty(fileName))
                return string.Empty;

            var newFileName = "";
            const string renamePattern = "{0}_{1}_{2}.{3}";

            var extensionIndex = fileName.LastIndexOf(".", StringComparison.InvariantCulture);
            if (extensionIndex == -1)
            {
                newFileName = fileName;
            }
            else
            {
                var extension = fileName.Substring(extensionIndex);
                //find the file name without extesion
                var fileNameWithoutExtension = fileName.Substring(0, fileName.Length - extension.Length);
                newFileName = string.Format(renamePattern, fileNameWithoutExtension, width, height, extension);
            }
            return newFileName;
        }
    }
}