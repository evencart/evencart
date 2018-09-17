using RoastedMarketplace.Core.Config;

namespace RoastedMarketplace.Data.Entity.Settings
{

    public class MediaSettings : ISettingGroup
    {
        /// <summary>
        /// Maximum file upload size in bytes for images
        /// </summary>
        public long MaximumFileUploadSizeForImages { get; set; }

        public int ImageQuality { get; set; }

    }


}