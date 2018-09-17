// #region Author Information
// // FoundationModelBinder.cs
// // 
// // (c) 2017 Apexol Technologies. All Rights Reserved.
// // 
// #endregion


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using F1Suite.WebApi.Models.ExtraFields;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RoastedMarketplace.Infrastructure.Mvc.Models
{
    public class FoundationModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (!typeof(FoundationModel).IsAssignableFrom(bindingContext.ModelType))
            {
                return null;
            }
            //bind with existing model
            var defaultModelBinder = new FoundationModelBinder();
            defaultModelBinder.BindModelAsync(bindingContext);
            
            var model = Activator.CreateInstance(bindingContext.ModelType) as FoundationModel;

            var extraFieldKeyword = "extraField";
            var pattern = extraFieldKeyword + @"\[([a-zA-Z0-9]+)\].+";
            var request = ApplicationEngine.CurrentHttpContext.Request;
            var formKeys = request.Form.Keys
                .Where(x => x.StartsWith(extraFieldKeyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
            var regEx = new Regex(pattern);
            var fieldNames = formKeys.Select(x => regEx.Match(x).Groups[1].Value).ToArray();

            if (fieldNames.Any())
            {
                model.ExtraFields = new List<ExtraFieldItemModel>();
                foreach (var field in fieldNames)
                {
                    var extraField = new ExtraFieldItemModel() {
                        FieldName = field,
                        FieldValue = request[field]
                    };
                    model.ExtraFields.Add(extraField);
                }
            }
            return true;
        }
        
    }
}