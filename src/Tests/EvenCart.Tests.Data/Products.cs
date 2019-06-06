using System;
using System.Collections.Generic;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Tests.Data
{
    public static class Products
    {
        public static List<Product> GetList()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Name = "Microsoft Surface Pro",
                    CanOrderWhenOutOfStock = true,
                    ChargeTaxes = true,
                    ComparePrice = 100,
                    Price = 80,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow,
                    IsShippable = true,
                    IsFeatured = true,
                    IsVisibleIndividually = true,
                    DisplayOrder = 1,
                    Mpn = "1",
                    Gtin = "2",
                    Sku = "SUR12",
                    IsDownloadable = false,
                    Description = "Surface Pro delivers even more speed and performance thanks to a powerful Intel® Core™ processor — with up to 50% more battery life1 than Surface Pro 4 and 2.5x more performance than Surface Pro 3.",
                    Summary = "Surface Pro delivers even more speed and performance thanks to a powerful Intel® Core™ processor — with up to 50% more battery life1 than Surface Pro 4 and 2.5x more performance than Surface Pro 3.",
                    Published = true,
                    Deleted = false,
                    TrackInventory = true
                },
                new Product()
                {
                    Name = "Apple iPad",
                    CanOrderWhenOutOfStock = true,
                    ChargeTaxes = true,
                    ComparePrice = 200,
                    Price = 170,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow,
                    IsShippable = true,
                    IsFeatured = true,
                    IsVisibleIndividually = true,
                    DisplayOrder = 2,
                    Mpn = "141",
                    Gtin = "123",
                    Sku = "APP12",
                    IsDownloadable = false,
                    Description = "Shop online for the main versions like the apple iPad mini, apple iPad with retina display, apple iPad Air and the original iPad. ... The retina display infused into Apple iPads is a wonderful option and gives you a much sharper image. iPad mini is a smaller and more compact version of the original tablet.",
                    Summary = "Shop online for the main versions like the apple iPad mini, apple iPad with retina display, apple iPad Air and the original iPad. ... The retina display infused into Apple iPads is a wonderful option and gives you a much sharper image. iPad mini is a smaller and more compact version of the original tablet.",
                    Published = true,
                    Deleted = false,
                    TrackInventory = true
                },
                new Product()
                {
                    Name = "Samsung Galaxy",
                    CanOrderWhenOutOfStock = false,
                    ChargeTaxes = false,
                    ComparePrice = null,
                    Price = 170,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow,
                    IsShippable = true,
                    IsFeatured = false,
                    IsVisibleIndividually = true,
                    DisplayOrder = 3,
                    Mpn = "7891",
                    Gtin = "1987",
                    Sku = "SMSNG7891",
                    IsDownloadable = false,
                    Description = "The Super AMOLED Infinity display of the new Galaxy J8 offers a one-of-a-kind viewing experience. ... The Galaxy J8 enables you to use chatting apps without pausing your videos. ... With 4GB RAM, 64GB memory and the latest Android Oreo, Galaxy J8’s processing power lets you multitask ...",
                    Summary = "The Super AMOLED Infinity display of the new Galaxy J8 offers a one-of-a-kind viewing experience. ... The Galaxy J8 enables you to use chatting apps without pausing your videos. ... With 4GB RAM, 64GB memory and the latest Android Oreo, Galaxy J8’s processing power lets you multitask ...",
                    Published = true,
                    Deleted = false,
                    TrackInventory = true
                }
            };
        }
    }
}
