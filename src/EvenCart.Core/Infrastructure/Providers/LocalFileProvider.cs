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
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace EvenCart.Core.Infrastructure.Providers
{
    public class LocalFileProvider : PhysicalFileProvider, ILocalFileProvider
    {
        public LocalFileProvider(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment.ContentRootPath)
        {
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

        public string[] GetFiles(string directoryName, string pattern = "*.*")
        {
            return Directory.GetFiles(directoryName, pattern);
        }

        public string[] GetDirectories(string directoryName)
        {
            return Directory.GetDirectories(directoryName);
        }

        public void DeleteFiles(string directoryName, string pattern)
        {
            var files = GetFiles(directoryName, pattern);
            foreach (var file in files)
                File.Delete(file);
        }

        public void DeleteFile(string fileName)
        {
            File.Delete(fileName);
        }

        public void ExtractArchive(string zipFileName, string directoryName)
        {
            ZipFile.ExtractToDirectory(zipFileName, directoryName);
        }

        public string GetTemporaryDirectory()
        {
            var cuTempDirectory = Path.GetTempPath();
            var tempDirectoryName = Path.GetRandomFileName();
            var directory = Path.Combine(cuTempDirectory, tempDirectoryName);
            Directory.CreateDirectory(directory);
            return directory;
        }

        public void DeleteDirectory(string directoryName, bool recursive)
        {
            Directory.Delete(directoryName, recursive);
        }

        public void CopyFile(string source, string destination, bool overwrite = false)
        {
            File.Copy(source, destination, overwrite);
        }

        public void CopyDirectory(string source, string destination, bool overwriteExisting = false)
        {
            var sourceInfo = new DirectoryInfo(source);
            var dirs = sourceInfo.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            // Get the files in the directory and copy them to the new location.
            var files = sourceInfo.GetFiles();
            foreach (var file in files)
            {
                var tempPath = CombinePaths(destination, file.Name);
                file.CopyTo(tempPath, overwriteExisting);
            }

            foreach (var subDir in dirs)
            {
                var tempPath = Path.Combine(destination, subDir.Name);
                CopyDirectory(subDir.FullName, tempPath, overwriteExisting);
            }
        }

        public string GetTemporaryFile(byte[] content = null)
        {
            var tempFilePath = Path.GetTempFileName();
            if (content != null)
                File.WriteAllBytes(tempFilePath, content);
            return tempFilePath;
        }
    }
}