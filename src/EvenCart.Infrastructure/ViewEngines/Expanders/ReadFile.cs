using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Infrastructure.Providers;
using Microsoft.AspNetCore.Mvc.Internal;

namespace EvenCart.Infrastructure.ViewEngines.Expanders
{
    public class ReadFile
    {
        public string FileName { get; set; }

        public DateTime LastModified { get; set; }

        public string Content { get; set; }

        private List<ReadFile> Children { get; } = new List<ReadFile>();

        private List<ReadFile> Parents { get; } = new List<ReadFile>();

        private Dictionary<string, List<object>> Meta { get; set; }

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

        public void AddMeta(string key, object value, string context)
        {
            key = $"{context}.{key}";
            Meta = Meta ?? new Dictionary<string, List<object>>();
            if (Meta.ContainsKey(key))
                Meta[key].Add(value);
            else
            {
                Meta.Add(key, new List<object>() { value });
            }
        }

        public IEnumerable<KeyValuePair<string, object>> GetMeta(string context)
        {
            Meta = Meta ?? new Dictionary<string, List<object>>();
            foreach (var kp in Meta)
            {
                if (kp.Key.StartsWith($"{context}."))
                {
                    var key = kp.Key.Substring($"{context}.".Length);
                    foreach (var value in kp.Value)
                        yield return new KeyValuePair<string, object>(key, value);
                }
            }    
        }

        public bool IsModified()
        {
            if (IsDirty || FileName == null)
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
                readFile.Meta?.Clear();
                readFile.MarkAllParentsDirty();
            }
            return readFile;
        }

        public static void PurgeCache()
        {
            //mark all the readfiles as dirty, so they'll be reloaded
            foreach (var readFile in ReadFileCache.Values)
                readFile.IsDirty = true;
        }
    }
}