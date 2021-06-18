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
using System.Collections.Generic;
using System.Linq;
using EvenCart.Services.Products;
using Genesis.Helpers;
using Genesis.Infrastructure.Mvc;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Modules.Navigation;
using Genesis.Modules.Pluggable;
using Genesis.Plugins;
using Genesis.ViewEngines.GlobalObjects;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Components.Widgets
{
    [ViewComponent(Name = WidgetSystemName)]
    public class MenuWidget : GenesisComponent, IWidget
    {
        private const string WidgetSystemName = "Menu";
        private readonly IWidgetService _widgetService;
        private readonly IMenuService _menuService;
        private readonly ICategoryService _categoryService;
        public MenuWidget(IWidgetService widgetService, IMenuService menuService, ICategoryService categoryService)
        {
            _widgetService = widgetService;
            _menuService = menuService;
            _categoryService = categoryService;
        }

        public override IViewComponentResult Invoke(object data = null)
        {
            var dataAsDict = data as Dictionary<string, object>;
            if (dataAsDict == null)
                return R.Success.ComponentResult;
            var widgetId = dataAsDict["id"].ToString();
            var widgetSettings = _widgetService.LoadWidgetSettings<MenuWidgetSettings>(widgetId);
            var menu = _menuService.Get(widgetSettings.MenuId);
            if (menu == null || menu.Stores == null || menu.Stores.All(x => x.Id != CurrentStore.Id))
                return R.Success.ComponentResult;
            var widgetNavigation = NavigationObject.GetNavigation(menu.MenuItems, widgetId, _categoryService.GetFullCategoryTree());
            return R.Success.With("title", widgetSettings.Title)
                .With("widgetNavigation", widgetNavigation)
                .With("widgetId", widgetId)
                .ComponentResult;
        }

        public string DisplayName { get; } = "Menu";

        public string SystemName { get; } = WidgetSystemName;

        public IList<string> WidgetZones { get; } = null;

        public bool HasConfiguration { get; } = true;

        public bool SkipDragging { get; } = false;

        public string ConfigurationUrl { get; } = null;

        public Type SettingsType { get; } = typeof(MenuWidgetSettings);

        public object GetViewObject(object settings)
        {
            var widgetSettings = settings as MenuWidgetSettings;
            var menus = _menuService.Get(x => true).ToList();
            var menusSelectList = SelectListHelper.GetSelectItemList(menus, x => x.Id, x => x.Name);
            return new
            {
                title = widgetSettings?.Title,
                menuId = widgetSettings?.MenuId,
                availableMenus = menusSelectList
            };
        }

        public class MenuWidgetSettings : WidgetSettingsModel
        {
            public string Title { get; set; }

            public int MenuId { get; set; }
        }
    }
}