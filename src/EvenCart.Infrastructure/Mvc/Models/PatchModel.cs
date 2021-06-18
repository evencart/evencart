﻿#region License
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
using System.Collections.Generic;
using System.Linq;
using Genesis.Data;
using Genesis.DataStructures;
using Genesis.Infrastructure.Attributes;
using EvenCart.Services.Extensions;

namespace EvenCart.Infrastructure.Mvc.Models
{
    public class PatchModel<T> : FoundationPatchModel where T : FoundationEntity
    {
        public T PassedInstance { get; set; }

        public T TargetInstance { get; set; }

        public PatchModel(T passedInstance)
        {
            PassedInstance = passedInstance;
        }
        /// <summary>
        /// Patches the entity and returns true if entire patch has been applied. Returns false if any patch fails
        /// </summary>
        /// <param name="targetInstance"></param>
        /// <returns></returns>
        public bool Patch(T targetInstance)
        {
            if (PatchFields.Count == 0)
                return false;
            TargetInstance = targetInstance;
            //we loop through dictionary and check which fields have changed
            var entityType = typeof(T);

            var patchResult = true;

            foreach (var property in entityType.GetProperties())
            {
                if (!PatchFields.ContainsKey(property.Name))
                    continue; //the field hasn't modified

                if (property.GetMethod.IsVirtual || property.Name=="Id")
                    continue;

                //check if the field is patchable or not, skip if not,we shouldn't check virtual properties either
                if (Attribute.IsDefined(property, typeof(NonPatchableAttribute)))
                {
                    //first get the attribute and check if any user roles are provided in it
                    var nonPatchableAttribute = property.GetCustomAttributes(typeof(NonPatchableAttribute), true)
                        .Cast<NonPatchableAttribute>()
                        .First();
                    var currentUser = ApplicationEngine.CurrentUser;
                    //are there any role names
                    if (nonPatchableAttribute.RoleNames.Any())
                    {
                        if (currentUser.IsOneOf(nonPatchableAttribute.RoleNames))
                        {
                            patchResult = false;
                            PatchFields[property.Name].Second = false;
                            continue;
                        }
                    }
                    else
                    {
                        patchResult = false;
                        PatchFields[property.Name].Second = false;
                        continue;
                    }
                   
                }

                //get passed value if exist
                var passedPropertyValue = PatchFields[property.Name].First;
                try
                {
                    var expectedValueType = property.PropertyType;
                    var compatibleValue = passedPropertyValue;
                    if (passedPropertyValue.GetType() != expectedValueType && !expectedValueType.IsInstanceOfType(passedPropertyValue))
                    {
                        //if types differ, try to change the type, else continue with the set
                        compatibleValue = Convert.ChangeType(passedPropertyValue, expectedValueType);
                    }
                    property.SetValue(targetInstance, compatibleValue);
                    PatchFields[property.Name].Second = true;
                }
                catch (Exception ex)
                {
                    //dont set this property, some error occured
                    //todo: use logger to track this error
                }
            }
            return patchResult;
        }

        public List<string> FailedFieldList
        {
            get { return PatchFields.Where(x => !x.Value.Second).Select(x => x.Key).ToList(); }
        }

        public override Dictionary<string, Pair<object, bool>> PatchFields { get; set; }
    }
}