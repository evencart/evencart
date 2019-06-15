using System.Collections.Generic;
using System.Linq;
using EvenCart.Core;
using EvenCart.Core.Infrastructure.Providers;
using EvenCart.Services.Serializers;

namespace EvenCart.Infrastructure.Localization
{
    public class Localizer : ILocalizer
    {
        private readonly Dictionary<string, LocalizedStringCollection> _localizedStrings;
        private readonly ILocalFileProvider _localFileProvider;
        private readonly IDataSerializer _dataSerializer;
        public Localizer(ILocalFileProvider localFileProvider, IDataSerializer dataSerializer)
        {
            _localFileProvider = localFileProvider;
            _dataSerializer = dataSerializer;
            _localizedStrings = new Dictionary<string, LocalizedStringCollection>();
        }

        public string Localize(string key, string languageCode = "en-US")
        {
            if (!_localizedStrings.TryGetValue(key, out LocalizedStringCollection collection))
            {
                return key;
            }
            var matchedResource = collection.FirstOrDefault(x => x.CultureLanguageCode == languageCode);
            return matchedResource == null ? key : matchedResource.Value;
        }

        public void LoadLanguage(string languageCode)
        {
            var languageFile = ServerHelper.MapPath(
                _localFileProvider.CombinePaths(ApplicationConfig.GlobalLanguagesDirectory, $"{languageCode}.json"));
            if (!_localFileProvider.FileExists(languageFile))
                return; // don't do anything if file doesn't exist
            //read the file
            var file = _localFileProvider.ReadAllText(languageFile);
            LanguageResource languageResource;
            try
            {
                languageResource = _dataSerializer.DeserializeAs<LanguageResource>(file);
                if (languageResource == null)
                    return;
            }
            catch
            {
                return;
            }

            foreach (var kv in languageResource.Translations)
            {
                if (!_localizedStrings.TryGetValue(kv.Key, out LocalizedStringCollection collection))
                {
                    collection = new LocalizedStringCollection();
                    _localizedStrings.Add(kv.Key, collection);
                }
                var languageValue = kv.Value;
                switch (languageValue) {
                    case null:
                        languageValue = string.Empty;
                        break;
                    case "":
                        languageValue = kv.Key;
                        break;
                }
                collection.Add(new LocalizedString()
                {
                    Value = languageValue,
                    CultureLanguageCode = languageCode
                });
            }
        }

        #region Private Class
        private class LanguageResource
        {
            public string ContextName { get; set; }

            public string Name { get; set; }

            public string CultureCode { get; set; }

            public Dictionary<string, string> Translations { get; set; }
        }
        #endregion
    }
}