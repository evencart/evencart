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
using System.Collections.Specialized;
using EvenCart.Areas.Administration.Models.Updates;
using EvenCart.Core.Data;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Services.HttpServices;
using EvenCart.Services.Settings;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Components
{
    [ViewComponent(Name = "EvenCartUpdates")]
    public class UpdatesComponent : FoundationComponent
    {
        private const string UpdatesFetchUrl = "https://evencart.co/api/feed";
        private readonly IRequestProvider _requestProvider;
        private readonly SystemSettings _systemSettings;
        private readonly ISettingService _settingService;
        private readonly GeneralSettings _generalSettings;
        private readonly IDataSerializer _dataSerializer;
        public UpdatesComponent(IRequestProvider requestProvider, SystemSettings systemSettings, ISettingService settingService, GeneralSettings generalSettings, IDataSerializer dataSerializer)
        {
            _requestProvider = requestProvider;
            _systemSettings = systemSettings;
            _settingService = settingService;
            _generalSettings = generalSettings;
            _dataSerializer = dataSerializer;
        }

        public override IViewComponentResult Invoke(object data = null)
        {
            var r = R;
            try
            {
                UpdatesResponseModel updates = null;
                //do we need to fetch?
                if (_systemSettings.LatestUpdatesFetched.IsNullEmptyOrWhiteSpace() || DateTime.UtcNow.Subtract(_systemSettings.LatestFetchedOn).TotalHours >=
                    _systemSettings.UpdateFetchIntervalInHours)
                {
                    //we need to
                    updates = _requestProvider.Get<UpdatesResponseModel>(UpdatesFetchUrl, new NameValueCollection()
                    {
                        { "storeDomain", _generalSettings.StoreDomain },
                        { "storeName", _generalSettings.StoreName }
                    });
                    if (updates != null)
                    {
                        if (!updates.Success && _systemSettings.LatestUpdatesFetched.IsNullEmptyOrWhiteSpace())
                            return R.Fail.ComponentResult;
                        _systemSettings.LatestUpdatesFetched = _dataSerializer.Serialize(updates);
                        _systemSettings.LatestFetchedOn = DateTime.UtcNow;
                        _settingService.Save(_systemSettings, CurrentStore.Id);
                    }
                   
                }

                updates = updates ?? (_systemSettings.LatestUpdatesFetched.IsNullEmptyOrWhiteSpace()
                              ? null
                              : _dataSerializer.DeserializeAs<UpdatesResponseModel>(_systemSettings.LatestUpdatesFetched));

                if (updates != null)
                {
                    r.With("evencartUpdates", updates.FeedItems);
                }
                return r.Success.ComponentResult;
            }
            catch
            {
                return r.Success.ComponentResult;
            }
            
        }
    }
}