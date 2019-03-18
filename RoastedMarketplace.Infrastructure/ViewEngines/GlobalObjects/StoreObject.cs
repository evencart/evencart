using System.Linq;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Infrastructure.Helpers;
using RoastedMarketplace.Infrastructure.MediaServices;
using RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects.Implementations;
using RoastedMarketplace.Services.Products;

namespace RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects
{
    public class StoreObject : GlobalObject
    {
        public override object GetObject()
        {
            var generalSettings = DependencyResolver.Resolve<GeneralSettings>();
            var catalogSettings = DependencyResolver.Resolve<CatalogSettings>();
            var orderSettings = DependencyResolver.Resolve<OrderSettings>();

            var mediaAccountant = DependencyResolver.Resolve<IMediaAccountant>();
            var categoryService = DependencyResolver.Resolve<ICategoryService>();
            var categories = categoryService.Get(x => x.ParentCategoryId == 0).ToList();
            var logoUrl = "";
            if (generalSettings.LogoId > 0)
            {
                logoUrl = mediaAccountant.GetPictureUrl(generalSettings.LogoId);
            }

            var categoryDefaultName =
                LocalizationHelper.Localize("All Categories", ApplicationEngine.CurrentLanguageCultureCode);
            return new StoreImplementation()
            {
                Url = generalSettings.StoreDomain,
                Name = generalSettings.StoreName,
                Theme = new ThemeImplementation()
                {
                    Name = "Default",
                    Url = "/default"
                },
                LogoUrl = logoUrl,
                CurrentPage = ApplicationEngine.GetActiveRouteName(),
                Categories = SelectListHelper.GetSelectItemList(categories, x => x.Id, x => x.Name, categoryDefaultName),
                WishlistEnabled = orderSettings.EnableWishlist,
                RepeatOrdersEnabled = orderSettings.AllowReorder
            };
        }
    }
}