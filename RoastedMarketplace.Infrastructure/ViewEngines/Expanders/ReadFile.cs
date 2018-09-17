using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Infrastructure.Providers;

namespace RoastedMarketplace.Infrastructure.ViewEngines.Expanders
{
    public class ReadFile
    {
        public string FileName { get; set; }

        public DateTime LastModified { get; set; }

        public string Content { get; set; }

        public List<ReadFile> Children { get; } = new List<ReadFile>();

        public bool IsModified()
        {
            var localFileProvider = DependencyResolver.Resolve<ILocalFileProvider>();
            var lastModified = localFileProvider.GetLastModifiedDateTime(FileName);
            return LastModified != lastModified || Children.Any(x => x.IsModified());
        }

        private static readonly ConcurrentDictionary<string, ReadFile> ReadFileCache =
            new ConcurrentDictionary<string, ReadFile>();
        public static ReadFile From(string fileName)
        {
            var localFileProvider = DependencyResolver.Resolve<ILocalFileProvider>();
            var lastModified = localFileProvider.GetLastModifiedDateTime(fileName);
            if (!ReadFileCache.TryGetValue(fileName, out ReadFile readFile) || readFile.IsModified())
            {
                if (readFile == null)
                {
                    readFile = new ReadFile();
                    ReadFileCache.TryAdd(fileName, readFile);
                }
                
                readFile.Content = localFileProvider.ReadAllText(fileName);
                readFile.LastModified = lastModified;
                readFile.FileName = fileName;
            }
            return readFile;
        }
    }
}