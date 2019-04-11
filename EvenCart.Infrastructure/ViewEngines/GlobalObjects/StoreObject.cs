using System.Linq;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Entity.Settings;
using EvenCart.Services.Products;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.MediaServices;
using EvenCart.Infrastructure.ViewEngines.GlobalObjects.Implementations;

namespace EvenCart.Infrastructure.ViewEngines.GlobalObjects
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
                RepeatOrdersEnabled = orderSettings.AllowReorder,
                ReviewsEnabled = catalogSettings.EnableReviews,
                ReviewModificationAllowed = catalogSettings.AllowReviewModification
            };
        }

        public override bool RenderInAdmin => true;

        public override bool RenderInPublic => true;
    }
}