using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Entity.Addresses;
using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Services.Addresses;

namespace RoastedMarketplace.Infrastructure.Helpers
{
    public static class SelectListHelper
    {
        public static List<SelectListItem> GetSelectItemList<T, TIdProperty, TTextProperty>(IList<T> entities, Expression<Func<T, TIdProperty>> idPropertyExpression, Expression<Func<T, TTextProperty>> textPropertyExpression)
        {

            var tType = typeof(T);
            var idMemberExpression = idPropertyExpression.Body as MemberExpression;
            if (idMemberExpression == null)
                throw new ArgumentException($"Expression {idPropertyExpression} refers a method, not a property");

            var idFieldName = idMemberExpression.Member.Name;

            var textMemberExpression = textPropertyExpression.Body as MemberExpression;
            if (textMemberExpression == null)
                throw new ArgumentException($"Expression {textPropertyExpression} refers a method, not a property");

            var textFieldName = textMemberExpression.Member.Name;

            //check if the fields asked actually exist
            var idProperty = tType.GetProperty(idFieldName);
            var textProperty = tType.GetProperty(textFieldName);

            //so we are good to go
            var selectList = new List<SelectListItem>();

            foreach (var entity in entities)
            {
                var idValue = idProperty.GetValue(entity)?.ToString() ?? "0";
                var textValue = textProperty.GetValue(entity)?.ToString() ?? "";
                selectList.Add(new SelectListItem(){
                    Value = idValue,
                    Text = textValue
                });
            }
            return selectList;
        }

        public static List<SelectListItem> GetSelectItemList<T>(IList<T> restrictToList = null) where T : IConvertible
        {
            //so we are good to go
            var selectList = new List<SelectListItem>();
            foreach (var eType in Enum.GetValues(typeof(T)))
            {
                if (restrictToList != null && restrictToList.Any(x => x.ToString(CultureInfo.InvariantCulture) != eType.ToString()))
                    continue;
                var field = eType.GetType().GetField(eType.ToString());
                //do we have a description attribute
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                    as DescriptionAttribute;
                var id = eType.ToString();
                var text = attribute == null ? eType.ToString() : attribute.Description;
                selectList.Add(new SelectListItem() {
                    Value = id,
                    Text = text
                });
            }
            return selectList;
        }

        public static List<SelectListItem> GetCountries()
        {
            var countryService = DependencyResolver.Resolve<ICountryService>();
            var countries = countryService.Get(x => true).ToList();
            return GetSelectItemList(countries, country => country.Id, country => country.Name);
        }

        public static List<SelectListItem> GetStatesOrProvinces(int countryId)
        {
            var stateProvinceService = DependencyResolver.Resolve<IStateOrProvinceService>();
            var states = stateProvinceService.Get(x => x.CountryId == countryId).ToList();
            return GetSelectItemList(states, state => state.Id, state => state.Name);
        }

        public static List<SelectListItem> GetInputTypes()
        {
            return GetSelectItemList<InputFieldType>();
        }

        public static List<SelectListItem> GetAddressTypes()
        {
            return GetSelectItemList<AddressType>();
        }
    }
}