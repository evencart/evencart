using System.Collections.Generic;
using System.Linq;
using EvenCart.Areas.Administration.Models.Navigation;
using EvenCart.Areas.Administration.Models.Pages;
using EvenCart.Areas.Administration.Models.Shop;
using EvenCart.Core.Services;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Navigation;
using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Extensions;
using EvenCart.Services.Navigation;
using EvenCart.Services.Pages;
using EvenCart.Services.Products;
using EvenCart.Services.Serializers;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class NavigationController : FoundationAdminController
    {
        private readonly IMenuService _menuService;
        private readonly IMenuItemService _menuItemService;
        private readonly IModelMapper _modelMapper;
        private readonly IContentPageService _contentPageService;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ICategoryAccountant _categoryAccountant;
        private readonly IDataSerializer _dataSerializer;
        public NavigationController(IMenuService menuService, IMenuItemService menuItemService, IModelMapper modelMapper, IContentPageService contentPageService, IProductService productService, ICategoryService categoryService, ICategoryAccountant categoryAccountant, IDataSerializer dataSerializer)
        {
            _menuService = menuService;
            _menuItemService = menuItemService;
            _modelMapper = modelMapper;
            _contentPageService = contentPageService;
            _productService = productService;
            _categoryService = categoryService;
            _categoryAccountant = categoryAccountant;
            _dataSerializer = dataSerializer;
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
                ParentCategoryId = x.ParentCategoryId
            }).ToList();

            var pageModels = pages.Select(x => new ContentPageModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return R.Success.With("menus", menuModels)
                .With("categories", () => categoryModels, () => _dataSerializer.Serialize(categoryModels))
                .With("pages", () => pageModels, () => _dataSerializer.Serialize(pageModels))
                .Result;
        }

        [DualGet("{menuId}", Name = AdminRouteNames.GetMenu)]
        [CapabilityRequired(CapabilitySystemNames.ManageMenu)]
        public IActionResult MenuEditor(int menuId)
        {
            var menu = menuId > 0 ? _menuService.Get(menuId) : new Menu();
            if (menu == null)
                return NotFound();
            var menuModel = _modelMapper.Map<MenuModel>(menu);
            return R.Success.With("menu", menuModel).Result;
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

            var menuItems = menu.MenuItems.Where(x => x.ParentMenuItemId == parentMenuItemId).ToList();
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
                //            itemModel.Url = ApplicationEngine.RouteUrl(RouteNames.SingleProduct,
                //                new {seName = x.SeoMeta.Slug});
                //            itemModel.SeoMetaTargetName = _productService.FirstOrDefault(y => y.Id == entityId)?.Name ?? "NA";
                //            break;
                //        case nameof(Category):
                //            itemModel.Url = ApplicationEngine.RouteUrl(RouteNames.ProductsPage,
                //                new { categoryId = x.SeoMeta.EntityId });
                //            itemModel.SeoMetaTargetName =
                //                _categoryService.FirstOrDefault(y => y.Id == entityId)?.Name ?? "NA";
                //            break;
                //        case nameof(ContentPage):
                //            itemModel.Url = ApplicationEngine.RouteUrl(RouteNames.SinglePage,
                //                new { id = x.SeoMeta.EntityId });
                //            itemModel.SeoMetaTargetName =
                //                _contentPageService.FirstOrDefault(y => y.Id == entityId)?.Name ?? "NA";
                //            break;
                //    }
                //}
                return itemModel;
            }).ToList();

            var parentMenuItemModel = _modelMapper.Map<MenuItemModel>(parentMenuItem);
            var grandParent = menu.MenuItems.FirstOrDefault(x => x.Id == parentMenuItem?.ParentMenuItemId);
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
                model.Url = SeoMetaHelper.GetUrl(menuItem.SeoMeta);
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
            return R.Success.With("menuItem", model).Result;
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
                menu.MenuItems.FirstOrDefault(x => x.Id == menuItemModel.ParentMenuItemId);
            if (menuItemModel.ParentMenuItemId > 0 && (parentMenuItem == null || parentMenuItem.IsGroup))
                return BadRequest();
            menuItem.ParentMenuItemId = parentMenuItem?.Id ?? 0;
            if (!menuItem.IsGroup)
            {
                menuItem.CssClass = menuItemModel.CssClass;
            }

            if (!menuItem.SeoMetaId.HasValue)
            {
                menuItem.Url = menuItemModel.Url;
            }
            menuItem.Name = menuItemModel.Name;

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
               var childMenuItems = menu.MenuItems.First(x => x.Id == menuItemId).ChildMenuItems.SelectManyRecursive(x => x.ChildMenuItems);
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
                        ParentMenuItemId = parentMenuItemId,
                        MenuId = menu.Id,
                        DisplayOrder = menu.MenuItems.Count(x => x.ParentMenuItemId == parentMenuItemId)
                    };
                    _menuItemService.Insert(menuItem);
                    categoryIds.Remove(category.Id);
                    menu.MenuItems.Add(menuItem);

                    //now the child categories
                    foreach (var childCategory in category.ChildCategories.Where(x => categoryIds.Contains(x.Id)))
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
                        ParentMenuItemId = menuItemModel.ParentMenuItemId,
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
                    ParentMenuItemId = menuItemModel.ParentMenuItemId,
                    MenuId = menu.Id,
                    IsGroup = menuItemModel.IsGroup
                });
            }

            return R.Success.Result;
        }
    }
}