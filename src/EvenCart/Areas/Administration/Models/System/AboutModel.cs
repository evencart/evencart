using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.System
{
    /// <summary>
    /// Represents a system and software information object
    /// </summary>
    public class AboutModel : FoundationModel
    {
        public string OperatingSystemName { get; set; }

        public string AspNetVersion { get; set; }

        public string FrameworkVersion { get; set; }

        public string TimeZone { get; set; }

        public string UtcTime { get; set; }

        public IList<string> LoadedAssemblies { get; set; }
    }
}