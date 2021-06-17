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
using System.Linq;
using EvenCart.Areas.Administration.Models.Navigation;
using EvenCart.Areas.Administration.Models.Pages;
using EvenCart.Areas.Administration.Models.Shop;
using EvenCart.Data.Entity.Shop;
using EvenCart.Services.Products;
using Genesis;
using Genesis.Extensions;
using Genesis.Helpers;
using Genesis.Infrastructure.Mvc;
using Genesis.Infrastructure.Mvc.Attributes;
using Genesis.Infrastructure.Mvc.ModelFactories;
using Genesis.Infrastructure.Security.Attributes;
using Genesis.Modules.Navigation;
using Genesis.Modules.Web;
using Genesis.Routing;
using Genesis.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvenCart.Areas.Administration.Controllers
{
    public class NavigationController : GenesisAdminController
    {
        private readonly IMenuService _menuService;
        private readonly IMenuItemService _menuItemService;
        private readonly IModelMapper _modelMapper;
        private readonly IContentPageService _contentPageService;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public NavigationController(IMenuService menuService, IMenuItemService menuItemService, IModelMapper modelMapper, IContentPageService contentPageService, IProductService productService, ICategoryService categoryService)
        {
            _menuService = menuService;
            _menuItemService = menuItemService;
            _modelMapper = modelMapper;
            _contentPageService = contentPageService;
            _productService = productService;
            _categoryService = categoryService;
        }

        [DualGet("", Name = AdminRouteNames.MenuList)]
        [CapabilityRequired(CapabilitySystemNames.ManageMenu)]
        public IActionResult MenuList()
        {
            var menus = _menuService.Get(x => true);
            var menuModels = menus.Select(x => _modelMapper.Map<MenuModel>(x)).OrderBy(x => x.Name).ToList();
            //categories
            var categories = _categoryService.GetFullCategoryTree();
            var pages = _contentPageService.Get(x => true).ToList();
            var categoryModels = categories.Select(x => new CategoryModel()
            {
                Id = x.Id,
                Name = x.Name,
                ParentId = x.ParentId
            }).ToList();

            var pageModels = pages.Select(x => new ContentPageModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return R.Success.With("menus", menuModels)
                .With("categories", categoryModels)
                .With("pages", pageModels)
                .Result;
        }

        [DualGet("{menuId}", Name = AdminRouteNames.GetMenu)]
        [CapabilityRequired(CapabilitySystemNames.ManageMenu)]
        public IActionResult MenuEditor(int menuId)
        {
            var menu = menuId > 0 ? _menuService.Get(menuId) : new Menu();
            if (menu == null)
                return NotFound();
            var menuStoreIds = menu.Stores?.Select(x => x.Id).ToList();
            var menuModel = _modelMapper.Map<MenuModel>(menu);
            menuModel.StoreIds = menuStoreIds;
            return R.Success.With("menu", menuModel).WithAvailableStores(menuStoreIds).Result;
        }

        [DualPost("", Name = AdminRouteNames.SaveMenu, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageMenu)]
        [ValidateModelState(ModelType = typeof(MenuModel))]
        public IActionResult SaveMenu(MenuModel menuModel)
        {
            var menu = menuModel.Id > 0 ? _menuService.Get(menuModel.Id) : new Menu();
            if (menu == null)
                return NotFound();
            menu.Name = menuModel.Name;
            menu.StoreIds = menuModel.StoreIds;
            _menuService.InsertOrUpdate(menu);
            menuModel.Id = menu.Id;
            return R.Success.With("menu", menuModel).Result;
        }

        [DualPost("delete", Name = AdminRouteNames.DeleteMenu, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageMenu)]
        public IActionResult DeleteMenu(int menuId)
        {
            var menu = menuId > 0 ? _menuService.Get(menuId) : null;
            if (menu == null)
                return NotFound();
            _menuService.Delete(menu);
            return R.Success.Result;
        }

        [DualGet("{menuId}/menuitems/{parentMenuItemId}", Name = AdminRouteNames.MenuItemList)]
        [CapabilityRequired(CapabilitySystemNames.ManageMenu)]
        public IActionResult MenuItemList(int menuId, int parentMenuItemId = 0)
        {
            if (parentMenuItemId < 0)
                return NotFound();
            var menu = _menuService.Get(menuId);
            if (menu == null)
                return NotFound();
            menu.MenuItems = menu.MenuItems ?? new List<MenuItem>();
            var parentMenuItem = menu.MenuItems.FirstOrDefault(x => x.Id == parentMenuItemId);
            if (parentMenuItemId > 0 && parentMenuItem == null)
                return NotFound();

            //also ignore the group ones
            if (parentMenuItem != null && parentMenuItem.IsGroup)
                return NotFound();

            var menuItems = menu.MenuItems.Where(x => x.ParentId == parentMenuItemId).ToList();
            var menuItemModels = menuItems.Select(x =>
            {
                var itemModel = _modelMapper.Map<MenuItemModel>(x);
                /*Uncomment if required*/

                //if (x.SeoMeta != null)
                //{
                //    var entityId = x.SeoMeta.EntityId;
                //    switch (x.SeoMeta.EntityName)
                //    {
                //        case nameof(Product):
                //            itemModel.Url = GenesisEngine.Instance.RouteUrl(RouteNames.SingleProduct,
                //                new {seName = x.SeoMeta.Slug});
                //            itemModel.SeoMetaTargetName = _productService.FirstOrDefault(y => y.Id == entityId)?.Name ?? "NA";
                //            break;
                //        case nameof(Category):
                //            itemModel.Url = GenesisEngine.Instance.RouteUrl(RouteNames.ProductsPage,
                //                new { categoryId = x.SeoMeta.EntityId });
                //            itemModel.SeoMetaTargetName =
                //                _categoryService.FirstOrDefault(y => y.Id == entityId)?.Name ?? "NA";
                //            break;
                //        case nameof(ContentPage):
                //            itemModel.Url = GenesisEngine.Instance.RouteUrl(RouteNames.SinglePage,
                //                new { id = x.SeoMeta.EntityId });
                //            itemModel.SeoMetaTargetName =
                //                _contentPageService.FirstOrDefault(y => y.Id == entityId)?.Name ?? "NA";
                //            break;
                //    }
                //}
                return itemModel;
            }).ToList();

            var parentMenuItemModel = _modelMapper.Map<MenuItemModel>(parentMenuItem);
            var grandParent = menu.MenuItems.FirstOrDefault(x => x.Id == parentMenuItem?.ParentId);
            var grandParentMenuItemModel = _modelMapper.Map<MenuItemModel>(grandParent);
            return R.Success.With("menuItems", menuItemModels).With("parentMenuItem", parentMenuItemModel)
                .With("grandParentMenuItem", grandParentMenuItemModel).Result;
        }

        [DualGet("{menuId}/menuitem/{menuItemId}", Name = AdminRouteNames.GetMenuItem)]
        [CapabilityRequired(CapabilitySystemNames.ManageMenu)]
        public IActionResult MenuItemEditor(int menuId, int menuItemId)
        {
            var menu = _menuService.Get(menuId);
            if (menu == null)
                return NotFound();
            var menuItem = menuItemId > 0 ? menu.MenuItems.FirstOrDefault(x => x.Id == menuItemId) : new MenuItem();
            if (menuItem == null)
                return NotFound();
            var model = _modelMapper.Map<MenuItemModel>(menuItem);
            if (menuItem.SeoMeta != null)
            {
                model.Url = Genesis.Modules.Web.SeoMetaHelper.GetUrl(menuItem.SeoMeta);
                var entityId = menuItem.SeoMeta.EntityId;
                switch (menuItem.SeoMeta.EntityName)
                {
                    case nameof(Product):
                        model.SeoMetaTargetName = _productService.FirstOrDefault(y => y.Id == entityId)?.Name ?? "NA";
                        break;
                    case nameof(Category):
                        model.SeoMetaTargetName =
                            _categoryService.FirstOrDefault(y => y.Id == entityId)?.Name ?? "NA";
                        break;
                    case nameof(ContentPage):
                        model.SeoMetaTargetName =
                            _contentPageService.FirstOrDefault(y => y.Id == entityId)?.Name ?? "NA";
                        break;
                }
            }

            IList<SelectListItem> availableMenuItems = null;
            //send available parent menu items to which this menu item can be moved to
            if (menu.MenuItems != null)
            {
                menu.MenuItems = menu.MenuItems.GetWithParentTree();
                availableMenuItems = SelectListHelper.GetSelectItemListWithAction(
                    menu.MenuItems.Where(x => x.ParentId != menuItemId).ToList(), x => x.Id,
                    item => item.GetFieldBreadCrumb(y => y.Name)).OrderBy(x => x.Text).ToList();
            }

            return R.Success.With("menuItem", model).With("availableMenuItems", availableMenuItems).Result;
        }

        [DualPost("{menuId}/menuitems", Name = AdminRouteNames.SaveMenuItem, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageMenu)]
        [ValidateModelState(ModelType = typeof(MenuItemModel))]
        public IActionResult SaveMenuItem(int menuId, MenuItemModel menuItemModel)
        {
            var menu = menuId > 0 ? _menuService.Get(menuId) : null;
            if (menu == null)
                return BadRequest();

            var menuItem = menu.MenuItems.FirstOrDefault(x => x.Id == menuItemModel.Id);
            if (menuItem == null)
                return NotFound();

            //check for parent
            var parentMenuItem =
                menu.MenuItems.FirstOrDefault(x => x.Id == menuItemModel.ParentId);
            if (menuItemModel.ParentId > 0 && (parentMenuItem == null || parentMenuItem.IsGroup))
                return BadRequest();

            menuItem.ParentId = parentMenuItem?.Id ?? 0;
            if (menuItem.Id > 0 && menuItem.Id == menuItem.ParentId)
            {
                return R.Fail.With("error", "A menu item can't be parent to itself.").Result;
            }
            if (!menuItem.IsGroup)
            {
                menuItem.CssClass = menuItemModel.CssClass;
            }
            if (!menuItem.SeoMetaId.HasValue)
            {
                menuItem.Url = menuItemModel.Url;
            }
            menuItem.Name = menuItemModel.Name;
            menuItem.OpenInNewWindow = menuItemModel.OpenInNewWindow;
            menuItem.Description = menuItemModel.Description;
            menuItem.ExtraData = menuItemModel.ExtraData;
            _menuItemService.InsertOrUpdate(menuItem);
            return R.Success.Result;
        }

        [DualPost("menuitems", Name = AdminRouteNames.DeleteMenuItem, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageMenu)]
        public IActionResult DeleteMenuItem(int menuItemId)
        {
            var menuItem = menuItemId > 0 ? _menuItemService.Get(menuItemId) : null;
            if (menuItem == null)
                return NotFound();
            //get the menu to get the entire tree
            var menu = _menuService.Get(menuItem.MenuId);
            if (menu == null)
                return NotFound();
            Transaction.Initiate(transaction =>
            {
                _menuItemService.Delete(menuItem, transaction);
                //get all the children
                var childMenuItems = menu.MenuItems.First(x => x.Id == menuItemId).Children.SelectManyRecursive(x => x.Children);
                foreach (var cm in childMenuItems)
                    _menuItemService.Delete(cm, transaction);
            });

            return R.Success.Result;
        }


        [DualPost("{menuId}/menuitems/displayorder", Name = AdminRouteNames.UpdateMenuItemDisplayOrder, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageMenu)]
        public IActionResult UpdateMenuItemDisplayOrder(int menuId, MenuItemModel[] menuItemModel)
        {
            if (menuItemModel == null)
                return BadRequest();

            //get category models with no-zero ids
            var validModels = menuItemModel.Where(x => x.Id != 0);
            Transaction.Initiate(transaction =>
            {
                foreach (var model in validModels)
                {
                    _menuItemService.Update(new { DisplayOrder = model.DisplayOrder }, m => m.Id == model.Id, transaction);
                }
            });
            return R.Success.Result;
        }

        [DualPost("menuitems/bulk", Name = AdminRouteNames.BulkCreateMenuItems, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageMenu)]
        public IActionResult BulkCreateMenuItems(CreateMenuItemModel menuItemModel)
        {
            //is menu valid
            var menu = _menuService.Get(menuItemModel.MenuId);
            if (menu == null)
                return NotFound();
            menu.MenuItems = menu.MenuItems ?? new List<MenuItem>();
            var menuItemCount = menu.MenuItems.Count;
            if (menuItemModel.CategoryIds?.Any() ?? false)
            {
                var categoryIds = menuItemModel.CategoryIds;
                var categories = _categoryService.GetFullCategoryTree().Where(x => categoryIds.Contains(x.Id)).ToList();

                //create an inline function for local recursive addition
                void InsertCategoryMenu(Category category, int parentMenuItemId)
                {
                    var menuItem = new MenuItem()
                    {
                        Name = category.Name,
                        SeoMetaId = category.SeoMeta.Id,
                        ParentId = parentMenuItemId,
                        MenuId = menu.Id,
                        DisplayOrder = menu.MenuItems.Count(x => x.ParentId == parentMenuItemId)
                    };
                    _menuItemService.Insert(menuItem);
                    categoryIds.Remove(category.Id);
                    menu.MenuItems.Add(menuItem);

                    //now the child categories
                    foreach (var childCategory in category.Children.Where(x => categoryIds.Contains(x.Id)))
                    {
                        InsertCategoryMenu(childCategory, menuItem.Id);
                    }
                }

                foreach (var category in categories)
                {
                    if (!categoryIds.Contains(category.Id))
                        continue;
                    InsertCategoryMenu(category, menuItemModel.ParentMenuItemId);
                }

            }

            if (menuItemModel.ContentPageIds?.Any() ?? false)
            {
                var contentPageIds = menuItemModel.ContentPageIds;
                var contentPages = _contentPageService.Get(x => contentPageIds.Contains(x.Id)).ToList();
                foreach (var contentPage in contentPages)
                {
                    _menuItemService.Insert(new MenuItem()
                    {
                        Name = contentPage.Name,
                        SeoMetaId = contentPage.SeoMeta.Id,
                        DisplayOrder = menuItemCount + 1,
                        ParentId = menuItemModel.ParentMenuItemId,
                        MenuId = menu.Id
                    });
                    menuItemCount++;
                }
            }

            if (!menuItemModel.Name.IsNullEmptyOrWhiteSpace())
            {
                _menuItemService.Insert(new MenuItem()
                {
                    Name = menuItemModel.Name,
                    Url = menuItemModel.Url,
                    DisplayOrder = menuItemCount + 1,
                    ParentId = menuItemModel.ParentMenuItemId,
                    MenuId = menu.Id,
                    IsGroup = menuItemModel.IsGroup
                });
            }

            return R.Success.Result;
        }
    }
}