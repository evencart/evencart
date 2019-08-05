using System;
using System.Collections.Specialized;
using EvenCart.Areas.Administration.Models.Updates;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Services.HttpServices;
using EvenCart.Services.Serializers;
using EvenCart.Services.Settings;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Components
{
    [ViewComponent(Name = "EvenCartUpdates")]
    public class UpdatesComponent : FoundationComponent
    {
#if DEBUG
        private const string UpdatesFetchUrl = "http://localhost:52886/samples/updates.json";
#else
        private const string UpdatesFetchUrl = "https://www.evencart.com/api/feed";
#endif
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
                if (!updates.Success && _systemSettings.LatestUpdatesFetched.IsNullEmptyOrWhiteSpace())
                    return R.Fail.ComponentResult;
                _systemSettings.LatestUpdatesFetched = _dataSerializer.Serialize(updates);
                _settingService.Save(_systemSettings);
            }

            updates = updates ?? (_systemSettings.LatestUpdatesFetched.IsNullEmptyOrWhiteSpace()
                          ? null
                          : _dataSerializer.DeserializeAs<UpdatesResponseModel>(_systemSettings.LatestUpdatesFetched));
            var r = R;
            if (updates != null)
            {
                r.With("evencartUpdates", updates.Updates);
            }
            return r.Success.ComponentResult;
        }
    }
}