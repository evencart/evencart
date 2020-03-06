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

using EvenCart.Data.Entity.MediaEntities;
using Microsoft.AspNetCore.Http;

namespace EvenCart.Infrastructure.MediaServices
{
    public interface IMediaAccountant
    {
        Media GetMediaInstance(IFormFile mediaFile, int userId);

        string GetPictureUrl(Media picture, int width = 0, int height = 0, bool returnDefaultIfNotFound = false);

        string GetPictureUrl(Media picture, string size, bool returnDefaultIfNotFound = false);

        string GetVideoUrl(Media media);

        string GetPictureUrl(int pictureId, int width = 0, int height = 0, bool returnDefaultIfNotFound = false);
    }
}