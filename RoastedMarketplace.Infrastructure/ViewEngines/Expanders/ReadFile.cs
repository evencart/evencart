using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Core.Infrastructure.Providers;

namespace RoastedMarketplace.Infrastructure.ViewEngines.Expanders
{
    public class ReadFile
    {
        public string FileName { get; set; }

        public DateTime LastModified { get; set; }

        public string Content { get; set; }

        private List<ReadFile> Children { get; } = new List<ReadFile>();

        private List<ReadFile> Parents { get; } = new List<ReadFile>();

        private bool IsDirty { get; set; }

        public void AddChild(ReadFile readFile)
        {
            if(!Children.Contains(readFile))
                Children.Add(readFile);
            //interlink
            readFile.AddParent(this);
        }

        private void AddParent(ReadFile readFile)
        {
            if(!Parents.Contains(readFile))
                Parents.Add(readFile);
        }

        private void MarkAllParentsDirty()
        {
            foreach (var parent in Parents)
                parent.IsDirty = true;
        }

        public bool IsModified()
        {
            if (IsDirty)
                return true;
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
                readFile.IsDirty = false;
                readFile.MarkAllParentsDirty();
            }
            return readFile;
        }
    }
}