#region Author Information
// CaseInsensitiveDictionary.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace RoastedMarketplace.Core.DataStructures
{
    /// <summary>
    /// Represents a dictionary where keys are string and case insensitive
    /// </summary>
    public class StringKeyDictionary<TValue> : Dictionary<string, TValue>
    {
        public new bool ContainsKey(string key)
        {
            if (base.ContainsKey(key))
                return true;

            return !string.IsNullOrEmpty(GetActualKey(key));
        }

        public new TValue this[string key]
        {
            get
            {
                key = GetActualKey(key);
                return base[key];
            }
            set
            {
                key = GetActualKey(key);
                base[key] = value;
            }
        }

        public new void Add(string key, TValue value)
        {
            key = GetActualKey(key);
            base.Add(key, value);
        }
        /// <summary>
        /// Tries to get the key in other case if present
        /// </summary>
        /// <param name="passedKey"></param>
        /// <returns></returns>
        private string GetActualKey(string passedKey)
        {
            return base.Keys.FirstOrDefault(x => string.Compare(x, passedKey, StringComparison.OrdinalIgnoreCase) == 0);
        }
    }
}