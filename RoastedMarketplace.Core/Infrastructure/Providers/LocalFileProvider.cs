using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace RoastedMarketplace.Core.Infrastructure.Providers
{
    public class LocalFileProvider : PhysicalFileProvider, ILocalFileProvider
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public LocalFileProvider(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment.ContentRootPath)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public string ReadAllText(string fileName)
        {
            return File.ReadAllText(fileName);
        }

        public void WriteAllText(string fileName, string content)
        {
            File.WriteAllText(fileName, content);
        }

        public bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }

        public bool DirectoryExists(string directoryName)
        {
            return Directory.Exists(directoryName);
        }

        public string CombinePaths(params string[] paths)
        {
            return Path.Combine(paths);
        }

        public DateTime GetLastModifiedDateTime(string fileName)
        {
            return File.GetLastWriteTime(fileName);
        }

        public byte[] ReadBytes(string fileName)
        {
            return File.ReadAllBytes(fileName);
        }

        public void WriteBytes(string fileName, byte[] bytes)
        {
            File.WriteAllBytes(fileName, bytes);
        }

        public void GetSafeFileName(string fileName, string directoryName, out string saveFileName, out string saveFileNameWithPath)
        {
            var checkFilePath = CombinePaths(directoryName, fileName);
            var fileNameWithoutExtension = GetFileNameWithoutExtension(fileName);
            var extension = GetExtension(fileName);
            while (FileExists(checkFilePath))
            {
                fileName = $"{fileNameWithoutExtension}_{Randomizer.NewString(10)}{extension}";
                checkFilePath = CombinePaths(directoryName, fileName);
            }
            saveFileName = fileName;
            saveFileNameWithPath = checkFilePath;
        }

        public string GetExtension(string fileName)
        {
            return Path.GetExtension(fileName);
        }

        public string GetFileNameWithoutExtension(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName);
        }

        public string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }
    }
}