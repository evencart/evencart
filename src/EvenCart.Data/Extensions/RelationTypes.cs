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
using EvenCart.Core.Data;

namespace EvenCart.Data.Extensions
{
    public class RelationTypes
    {
        private static readonly ConcurrentDictionary<string, object> PrefetchedActions = new ConcurrentDictionary<string, object>();
        public static Action<T, T1> OneToMany<T, T1>(Action<T, T1> mutationAction = null) where T : FoundationEntity where T1 : FoundationEntity
        {
            var typeofT1 = typeof(T1);
            var typeOfT = typeof(T);
            var key = $"onetomany_{typeOfT}_{typeofT1}";
            //we found one action, return it.
            if (PrefetchedActions.TryGetValue(key, out object actionObject))
                return (Action<T, T1>)actionObject;

            //get field info from T
            //get field info from T
            var propertyInfoT = typeOfT.GetProperties()
                .FirstOrDefault(x => x.PropertyType == typeof(IList<T1>) && x.CanRead && x.CanWrite && x.GetGetMethod().IsVirtual);

            var propertyInfoT1 = typeofT1.GetProperties()
                .FirstOrDefault(x => x.PropertyType == typeOfT && x.CanRead && x.CanWrite && x.GetGetMethod().IsVirtual);

            if (propertyInfoT == null && propertyInfoT1 == null)
                throw new Exception($"Can't create a one to many relation between {typeOfT} and {typeofT1}");

            Action<T, T1> action = (t, t1) =>
            {
                if (t == null || t1 == null)
                    return;

                if (propertyInfoT != null)
                {
                    var property = (IList<T1>)propertyInfoT.GetValue(t);
                    if (property == null)
                    {
                        property = new List<T1>();
                        propertyInfoT.SetValue(t, property);
                    }
                    if (!property.Contains(t1))
                        property.Add(t1);
                }

                if (propertyInfoT1 != null)
                {
                    if (propertyInfoT1.GetValue(t1) == null)
                        propertyInfoT1.SetValue(t1, t);
                }

                mutationAction?.Invoke(t, t1);
            };

            //save for future use
            PrefetchedActions.TryAdd(key, action);
            return action;
        }

        public static Action<T, T1> OneToOne<T, T1>(Action<T, T1> mutationAction = null) where T : FoundationEntity where T1 : FoundationEntity
        {
            var typeofT1 = typeof(T1);
            var typeOfT = typeof(T);
            var key = $"onetoone_{typeOfT}_{typeofT1}";
            //we found one action, return it.
            if (PrefetchedActions.TryGetValue(key, out object actionObject))
                return (Action<T, T1>)actionObject;

            //get field info from T
            var propertyInfoT = typeOfT.GetProperties()
                .FirstOrDefault(x => x.PropertyType == typeofT1 && x.CanRead && x.CanWrite && x.GetGetMethod().IsVirtual);

            var propertyInfoT1 = typeofT1.GetProperties()
                .FirstOrDefault(x => x.PropertyType == typeOfT && x.CanRead && x.CanWrite && x.GetGetMethod().IsVirtual);

            if (propertyInfoT == null && propertyInfoT1 == null)
                throw new Exception($"Can't create a one to one relation between {typeOfT} and {typeofT1}");

            Action<T, T1> action = (t, t1) =>
            {
                if (t == null || t1 == null)
                    return;
                if (propertyInfoT != null)
                {
                    propertyInfoT.SetValue(t, t1);
                }
                if (propertyInfoT1 != null)
                {
                    propertyInfoT1.SetValue(t1, t);
                }
                mutationAction?.Invoke(t, t1);
            };

            //save for future use
            PrefetchedActions.TryAdd(key, action);
            return action;
        }
    }
}