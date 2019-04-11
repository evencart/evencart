using EvenCart.Core.Data;
using EvenCart.Data.Entity.MediaEntities;

namespace Ui.Slider.Data
{
    public class UiSlider : FoundationEntity
    {
        public string Title { get; set; }

        public int MediaId { get; set; }

        public int DisplayOrder { get; set; }

        public bool Visible { get; set; }

        public string Url { get; set; }

        #region Virtual Properties
        public virtual Media Media { get; set; }
        #endregion
    }
}