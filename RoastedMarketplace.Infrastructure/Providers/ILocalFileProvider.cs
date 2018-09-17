using System;
using Microsoft.Extensions.FileProviders;

namespace RoastedMarketplace.Infrastructure.Providers
{
    public interface ILocalFileProvider : IFileProvider
    {
        string ReadAllText(string fileName);

        void WriteAllText(string fileName, string content);

        bool FileExists(string fileName);

        bool DirectoryExists(string directoryName);

        string CombinePaths(params string[] paths);

        DateTime GetLastModifiedDateTime(string fileName);
    }
}