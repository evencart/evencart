using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace RoastedMarketplace.Infrastructure.Providers
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
    }
}