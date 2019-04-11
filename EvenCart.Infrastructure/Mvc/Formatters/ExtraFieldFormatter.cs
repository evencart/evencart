// #region Author Information
// // ExtraFieldFormatter.cs
// // 
// // (c) Apexol Technologies. All Rights Reserved.
// // 
// #endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using F1Suite.Core.DataStructures;
using F1Suite.Core.Infrastructure.Utils;
using F1Suite.Services.Extensions;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using F1Suite.WebApi.Models.ExtraFields;
using Newtonsoft.Json;

namespace RoastedMarketplace.Infrastructure.Mvc.Formatters
{
    public class ExtraFieldFormatter : BufferedMediaTypeFormatter
    {

        public ExtraFieldFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

        }
        public override bool CanReadType(Type type)
        {

            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(WithExtraFieldsModel<>) ||
                    type.IsArray && type.GetElementType().GetGenericTypeDefinition() == typeof(WithExtraFieldsModel<>) ||
                    type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IList<>) && type.GetGenericArguments()[0].GetGenericTypeDefinition() == typeof(WithExtraFieldsModel<>)
            );
        }

        public override bool CanWriteType(Type type)
        {
            return false;
        }

        public override object ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
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
                if (type.GetGenericTypeDefinition() == typeof(WithExtraFieldsModel<>))
                {
                    //get the argument types
                    var typeArguments = type.GetGenericArguments();
                    var modelType = typeArguments.First();

                    var modelInstance = CreateTypeInstance(type, modelType, jsonBody);
                    return modelInstance;
                }
            }
            return null;
        }

        private object CreateTypeInstance(Type singleObjectType, Type modelType, string jsonBody)
        {
            var passedValues = JsonConvert.DeserializeObject<StringKeyDictionary<object>>(jsonBody);
            if (passedValues == null)
                return null;

            //child instance
            var childInstance = Activator.CreateInstance(modelType);

            var extraFields = new List<ExtraFieldItemModel>();
            //set the properties of this instance
            foreach (var property in modelType.GetProperties())
            {
                if (!passedValues.ContainsKey(property.Name))
                    continue; //value wasn't passed or it's Id column which shouldn't be patched anyways

                //get passed value if exist
                var passedPropertyValue = passedValues[property.Name];
                try
                {
                    var compatibleValue = TypeConverter.CastPropertyValue(property, passedPropertyValue);
                    property.SetValue(childInstance, compatibleValue);
                }
                catch (Exception ex)
                {
                    // ignored
                }
            }

            //are there any extra fields?
            if (passedValues.ContainsKey(ExtraFieldExtensions.ExtraFieldsRequestKey))
            {
                //deserialize to another dictionary
                var extraFieldsDictionary =
                    JsonConvert.DeserializeObject<StringKeyDictionary<string>>(passedValues[ExtraFieldExtensions.ExtraFieldsRequestKey].ToString());

                foreach (var ef in extraFieldsDictionary)
                {
                    var efField = new ExtraFieldItemModel()
                    {
                        FieldName = ef.Key,
                        FieldValue = ef.Value
                    };
                    extraFields.Add(efField);
                }
            }

            //create the instance now
            var modelInstance = Activator.CreateInstance(singleObjectType);
            singleObjectType.GetProperty("Model").SetValue(modelInstance, childInstance);
            singleObjectType.GetProperty("ExtraFields").SetValue(modelInstance, extraFields);
            return modelInstance;
        }
    }
}