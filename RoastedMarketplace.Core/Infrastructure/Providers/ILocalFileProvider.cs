using System;
using Microsoft.Extensions.FileProviders;

namespace RoastedMarketplace.Core.Infrastructure.Providers
{
    public interface ILocalFileProvider : IFileProvider
    {
        string ReadAllText(string fileName);

        void WriteAllText(string fileName, string content);

        bool FileExists(string fileName);

        bool DirectoryExists(string directoryName);

        string CombinePaths(params string[] paths);

        DateTime GetLastModifiedDateTime(string fileName);

        byte[] ReadBytes(string fileName);

        void WriteBytes(string fileName, byte[] bytes);

        void GetSafeFileName(string fileName, string directoryName, out string saveFileName, out string saveFileNameWithPath);

        string GetExtension(string fileName);

        string GetFileNameWithoutExtension(string fileName);

        string GetFileName(string filePath);

        string[] GetFiles(string directoryName, string pattern = "*.*");
    }
}