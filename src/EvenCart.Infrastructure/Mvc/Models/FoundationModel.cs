using DotLiquid;
using EvenCart.Infrastructure.Mvc.Formatters;

namespace EvenCart.Infrastructure.Mvc.Models
{
    public abstract class FoundationModel : Drop //inheriting from drop, so we can allow models to be used by dotliquid
    {
        private DynamicFormatterObject _formatterObject;

        public DynamicFormatterObject Formatted
        {
            get
            {
                _formatterObject = _formatterObject ?? new DynamicFormatterObject();
                return _formatterObject;
            }
        }
    }
}