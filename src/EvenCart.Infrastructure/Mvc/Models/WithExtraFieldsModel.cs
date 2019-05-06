// #region Author Information
// // ExtraFieldModel.cs
// // 
// // (c) Apexol Technologies. All Rights Reserved.
// // 
// #endregion


using System.Linq;
using F1Suite.Core.Data;
using F1Suite.Core.Infrastructure.AppEngine;
using F1Suite.Data.Extensions;
using F1Suite.Services.Enum;
using F1Suite.Services.Extensions;
using F1Suite.Services.ExtraFields;
using RoastedMarketplace.Infrastructure.Infrastructure;
using RoastedMarketplace.Infrastructure.Mvc.Formatters;
using F1Suite.WebApi.Models.ExtraFields;
using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Services.Enum;
using RoastedMarketplace.Services.Extensions;
using RoastedMarketplace.Services.ExtraFields;

namespace RoastedMarketplace.Infrastructure.Mvc.Models
{
    public class WithExtraFieldsModel<TModelType> : FoundationExtraFieldModel where TModelType : FoundationModel
    {
        public TModelType Model { get; set; }

        public bool ValidateExtraFieldsForEntity<T>() where T : FoundationEntity
        {
            var extraFieldService = ApplicationEngine.Resolve<IExtraFieldService>();
            var entityName = typeof(T).Name;

            var dbExtraFields = extraFieldService.Get(x => x.EntityName == entityName).ToList();

            //current user is agent or registered user?
            var currentUser = ApplicationEngine.CurrentUser;

            foreach (var dbExField in dbExtraFields)
            {
                var displayableFieldLabel = currentUser.IsAgent()
                    ? dbExField.LabelBackOffice
                    : dbExField.LabelFrontEnd;

                var expectedFieldName = dbExField.GetDbFieldName();

                var sExField = SubmittedExtraFields.FirstOrDefault(x => x.FieldName == expectedFieldName);
                if (sExField == null)
                {
                   
                    //#1 if user is required to submit this field
                    if ((currentUser.IsRegistered() && dbExField.RequiredForUsers) ||
                        (currentUser.IsAgent() && dbExField.RequiredForAgents))
                    {
                        SubmittedExtraFields.Add(new ExtraFieldItemModel()
                        {
                            FieldName = expectedFieldName,
                            FieldValue = null,
                            IsValid = false,
                            ValidationMessage = $"Field '{displayableFieldLabel}' is required"
                        });
                    }
                }
                else
                {
                    //if field was submitted, let's validate if valid values where submitted
                    var validationResult = dbExField.ValidateValueForUser(sExField.FieldValue, currentUser);
                    if (validationResult != ExtraFieldValidationResult.ValidField)
                    {
                        sExField.IsValid = false;
                        switch (validationResult)
                        {
                            case ExtraFieldValidationResult.InvalidValueForFieldType:
                                sExField.ValidationMessage = $"Invalid value for '{displayableFieldLabel}' submitted";
                                break;
                            case ExtraFieldValidationResult.EmptyValueForRequiredField:
                                sExField.ValidationMessage = $"Field '{displayableFieldLabel}' is required";
                                break;
                            case ExtraFieldValidationResult.OutOfRangeValue:
                                sExField.ValidationMessage = $"Invalid value for '{displayableFieldLabel}' submitted";
                                break;
                            case ExtraFieldValidationResult.NonEditableField:
                                sExField.ValidationMessage = $"'{displayableFieldLabel}' can't be edited";
                                break;
                        }
                    }
                    else
                    {
                        //we can set value to a default one if empty
                        if (sExField.FieldValue.IsNullEmptyOrWhiteSpace())
                        {
                            sExField.FieldValue = dbExField.DefaultValue;
                        }
                        sExField.IsValid = true;
                    }
                        
                }
            }
            return SubmittedExtraFields.All(x => x.IsValid);
        }
        
    }
}