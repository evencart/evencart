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
using EvenCart.Services.HttpServices;

namespace EvenCart.Services.MediaServices
{
    public class EmbeddedUrlProviderService : IEmbeddedUrlProviderService
    {
        private const string FetchUrl = "https://noembed.com/embed?nowrap=on&url={0}";

        private readonly IRequestProvider _requestProvider;
        public EmbeddedUrlProviderService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public EmbeddedMedia GetEmbeddedMedia(string url)
        {
            var fetchUrl = string.Format(FetchUrl, url);
            var response = _requestProvider.Get<NoEmbedResponse>(fetchUrl);
            if (response == null)
                return null;
            return new EmbeddedMedia()
            {
                ProviderName = response.provider_name,
                Url = url,
                ThumbnailUrl = response.thumbnail_url,
                Author = response.author_name,
                Html = response.html,
                Title = response.title
            };

        }

        private class NoEmbedResponse
        {
            public string html { get; set; }
            public string thumbnail_url { get; set; }
            public string provider_name { get; set; }
            public string title { get; set; }
            public string author_url { get; set; }
            public string author_name { get; set; }
        }
    }


}