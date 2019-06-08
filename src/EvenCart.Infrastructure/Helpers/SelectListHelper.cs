using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using EvenCart.Core;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Infrastructure.Providers;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Enum;
using EvenCart.Data.Extensions;
using EvenCart.Services.Addresses;
using EvenCart.Services.Emails;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvenCart.Infrastructure.Helpers
{
    public static class SelectListHelper
    {
        public static List<SelectListItem> GetSelectItemList<T, TIdProperty, TTextProperty>(IList<T> entities, Expression<Func<T, TIdProperty>> idPropertyExpression, Expression<Func<T, TTextProperty>> textPropertyExpression, string selectorItem = null)
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

            if (!selectorItem.IsNullEmptyOrWhiteSpace())
            {
                var propertyInfo = (PropertyInfo) idMemberExpression.Member;
                var propertyType = propertyInfo.PropertyType;
                var defaultValue = propertyType == typeof(int) ? "0" : "";
                selectList.Insert(0, new SelectListItem(selectorItem, defaultValue, true));
            }
            return selectList;
        }

        public static List<SelectListItem> GetSelectItemListWithAction<T, TIdProperty>(IList<T> entities, Expression<Func<T, TIdProperty>> idPropertyExpression, Func<T, string> textPropertyFunction, string selectorItem = null)
        {

            var tType = typeof(T);
            var idMemberExpression = idPropertyExpression.Body as MemberExpression;
            if (idMemberExpression == null)
                throw new ArgumentException($"Expression {idPropertyExpression} refers a method, not a property");

            var idFieldName = idMemberExpression.Member.Name;
            //check if the fields asked actually exist
            var idProperty = tType.GetProperty(idFieldName);
            //so we are good to go
            var selectList = new List<SelectListItem>();

            foreach (var entity in entities)
            {
                var idValue = idProperty.GetValue(entity)?.ToString() ?? "0";
                var textValue = textPropertyFunction(entity);
                selectList.Add(new SelectListItem()
                {
                    Value = idValue,
                    Text = textValue
                });
            }

            if (!selectorItem.IsNullEmptyOrWhiteSpace())
            {
                var propertyInfo = (PropertyInfo)idMemberExpression.Member;
                var propertyType = propertyInfo.PropertyType;
                var defaultValue = propertyType == typeof(int) ? "0" : "";
                selectList.Insert(0, new SelectListItem(selectorItem, defaultValue, true));
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
            var countries = countryService.Get(x => x.Published).ToList();
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

        public static List<SelectListItem> GetPaymentStatusItems()
        {
            return GetSelectItemList<PaymentStatus>();
        }

        public static List<SelectListItem> GetOrderStatusItems()
        {
            return GetSelectItemList<OrderStatus>();
        }

        public static List<SelectListItem> GetShipmentStatusItems()
        {
            return GetSelectItemList<ShipmentStatus>();
        }

        public static List<SelectListItem> GetTimezones()
        {
            var timeZones = ServerHelper.GetAvailableTimezones();
            return GetSelectItemList(timeZones, info => info.Id, info => info.DisplayName);
        }

        public static List<SelectListItem> GetCultures()
        {
            var cultureInfos = ServerHelper.GetAvailableCultureInfos().ToList();
            return GetSelectItemListWithAction(cultureInfos, info => info.Name, info => $"{info.DisplayName} - {info.Name}");
        }

        public static List<SelectListItem> GetAvailableEmailAccounts()
        {
            var emailAccountService = DependencyResolver.Resolve<IEmailAccountService>();
            var emailAccounts = emailAccountService.Get(x => true).ToList();
            return GetSelectItemList(emailAccounts, account => account.Id, account => account.Email);
        }

        public static List<SelectListItem> GetAvailableFlags()
        {
            var localFileProvider = DependencyResolver.Resolve<ILocalFileProvider>();
            var files = localFileProvider.GetFiles(ApplicationEngine.MapPath(ApplicationConfig.FlagsDirectory, true), "*.png");
            var fileInfos = files.Select(x => new FileInfo(x));
            return GetSelectItemList(fileInfos.ToList(), x => x.Name, x => x.Name);
        }
    }
}