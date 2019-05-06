#region Author Information

// PatchFormatter.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using F1Suite.Core.DataStructures;
using F1Suite.Core.Infrastructure.Utils;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using Newtonsoft.Json;

namespace RoastedMarketplace.Infrastructure.Mvc.Formatters
{
    public class PatchFormatter : BufferedMediaTypeFormatter
    {
        public PatchFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
        }

        public override bool CanReadType(Type type)
        {
            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(PatchModel<>) ||
                    type.IsArray && type.GetElementType().GetGenericTypeDefinition() == typeof(PatchModel<>) ||
                    type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IList<>) && type.GetGenericArguments()[0].GetGenericTypeDefinition() == typeof(PatchModel<>)
                );
        }

        public override bool CanWriteType(Type type)
        {
            return false;
        }

        public override object ReadFromStream(Type type, Stream readStream, HttpContent content,
            IFormatterLogger formatterLogger)
        {
            var jsonBody = "";
            //read the body content
            using (var reader = new StreamReader(readStream))
            {
                jsonBody = reader.ReadToEnd();
            }

            if (type.IsArray)
            {
                //todo: to be added when needed
            }
            else
            {
                if (type.GetGenericTypeDefinition() == typeof(PatchModel<>))
                {
                    //get the argument types
                    var argumentType = type.GetGenericArguments().First();
                    var modelInstance = CreateTypeInstance(type, argumentType, jsonBody);
                    return modelInstance;
                }
                else if (type.GetGenericTypeDefinition() == typeof(IList<>))
                {
                    var singleObjectType = type.GetGenericArguments().First();//returns PatchModel<T>
                    var argumentType = singleObjectType.GetGenericArguments().First();//returns T
                    var listType = typeof(List<>).MakeGenericType(singleObjectType); //list of patch models
                    var listInstance = (IList) Activator.CreateInstance(listType);
                    var dataList = JsonConvert.DeserializeObject<List<dynamic>>(jsonBody);
                    foreach (var data in dataList)
                    {
                        var dataInstance = CreateTypeInstance(singleObjectType, argumentType,
                            JsonConvert.SerializeObject(data));
                        listInstance.Add(dataInstance);
                    }
                    return listInstance;
                }
            }
            return null;
        }

        private object CreateTypeInstance(Type singleObjectType, Type argumentType, string jsonBody)
        {
            var passedValues = JsonConvert.DeserializeObject<StringKeyDictionary<object>>(jsonBody);
            if (passedValues == null)
                return null;

            //child instance
            var childInstance = Activator.CreateInstance(argumentType);

            var patchedFields = new Dictionary<string, Pair<object, bool>>();
            //set the properties of this instance
            foreach (var property in argumentType.GetProperties())
            {
                if (!passedValues.ContainsKey(property.Name))
                    continue; //value wasn't passed or it's Id column which shouldn't be patched anyways

                //get passed value if exist
                var passedPropertyValue = passedValues[property.Name];
                try
                {
                    var compatibleValue = TypeConverter.CastPropertyValue(property, passedPropertyValue);
                    property.SetValue(childInstance, compatibleValue);
                    //add this value to the patch fields and mark it false till it's actually patched 
                    //this will ensure that only valid values are patched. 
                    patchedFields.Add(property.Name, Pair.Create(compatibleValue, false));
                }
                catch (Exception ex)
                {
                    // ignored
                }
            }

            //create the instance now
            var modelInstance = (FoundationPatchModel)Activator.CreateInstance(singleObjectType, childInstance);

            //set the dictionary
            modelInstance.PatchFields = patchedFields;
            return modelInstance;
        }
        
    }
}