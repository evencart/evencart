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

using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using DotLiquid;
using Genesis.Data;
using Genesis.Infrastructure;
using EvenCart.Data.Extensions;

namespace EvenCart.Infrastructure.Mvc.Formatters
{
    public sealed class DynamicFormatterObject : DynamicObject, ILiquidizable
    {
        private readonly Dictionary<string, object> _properties;

        public DynamicFormatterObject()
        {
            _properties = new Dictionary<string, object>();
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _properties.Keys;
        }

        public void Set(string name, object value)
        {
            if (_properties.ContainsKey(name))
            {
                _properties[name] = value;
            }
            else
            {
                _properties.Add(name, value);

            }
        }
        public T Get<T>(string name)
        {
            if (_properties.ContainsKey(name))
            {
                return (T) _properties[name];
            }
            else
            {
                return default(T);
            }
        }
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_properties.ContainsKey(binder.Name))
            {
                result = _properties[binder.Name];
                return true;
            }

            result = null;
            return false;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (_properties.ContainsKey(binder.Name))
            {
                _properties[binder.Name] = value;
                return true;
            }
            return false;
        }

        public object ToLiquid()
        {
            return Hash.FromDictionary(_properties.ToDictionary(x => x.Key.ToCamelCase(), x => x.Value));
            var serializer = DependencyResolver.Resolve<IDataSerializer>();
            //first get a anonymous type that matches the keys in the dictionary
            var dynamicType = RuntimeTypeBuilder.GetDynamicType(_properties.Keys);
            //todo: To make things work for now, we serialize and deserialize to convert dictionary to target type
            dynamic obj = serializer.Deserialize(serializer.Serialize(_properties), dynamicType);
            return new DropProxy(obj, _properties.Keys.ToArray());
        }
    }
}