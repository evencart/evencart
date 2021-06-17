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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Genesis.Infrastructure;
using Genesis.Infrastructure.Providers;

namespace EvenCart.Infrastructure.ViewEngines.Expanders
{
    public class ReadFile
    {
        public string FileName { get; set; }

        public DateTime LastModified { get; set; }

        public string Content { get; set; }

        private List<ReadFile> Children { get; } = new List<ReadFile>();

        private List<ReadFile> Parents { get; } = new List<ReadFile>();

        private ConcurrentDictionary<string, List<object>> Meta { get; set; }

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
            Meta = Meta ?? new ConcurrentDictionary<string, List<object>>();
            if (Meta.ContainsKey(key))
                Meta[key].Add(value);
            else
            {
                Meta.TryAdd(key, new List<object>() { value });
            }
        }

        public IEnumerable<KeyValuePair<string, object>> GetMeta(string context)
        {
            Meta = Meta ?? new ConcurrentDictionary<string, List<object>>();
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
            var cacheKey = fileName + ApplicationEngine.CurrentStore?.Id + ApplicationEngine.CurrentLanguage.CultureCode;
            if (!ReadFileCache.TryGetValue(cacheKey, out ReadFile readFile) || readFile.IsModified())
            {
                if (readFile == null)
                {
                    readFile = new ReadFile();
                    ReadFileCache.TryAdd(cacheKey, readFile);
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