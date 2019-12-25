using System.Diagnostics;
using System.Reflection;

namespace EvenCart.Core
{
    public class AppVersionEvaluator
    {
        private static string _version = null;
        public static string Version
        {
            get
            {
                if (_version == null)
                {
                    var assembly = Assembly.GetEntryAssembly();
                    var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
                    _version = fileVersionInfo.ProductVersion;
                }

                return _version;
            }
        }
        /// <summary>
        /// Checks if the version that's being passed matches the major and minor portions of the app version
        /// </summary>
        public static bool IsVersionSupported(string version)
        {
            if (string.IsNullOrEmpty(version) || !version.Contains("."))
                return false;
            //our version numbers shall be of following types (semver)
            //major.minor.revision => 1.2.3
            //major.minor.revision-{release} => 1.2.3-beta

            //we support if major and minor versions match, irrespective of revision
            return GetMajorMinorPart(Version) == version;
        }

        private static string GetMajorMinorPart(string version)
        {
            return version.Substring(0, version.LastIndexOf('.'));
        }
    }
}