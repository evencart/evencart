using EvenCart.Areas.Administration.Models.Common;
using EvenCart.Data.Entity.Common;

namespace EvenCart.Areas.Administration.Factories.Common
{
    public class CustomLabelModelFactory : ICustomLabelModelFactory
    {
        public CustomLabelModel Create(CustomLabel entity)
        {
            return new CustomLabelModel()
            {
                Id = entity.Id,
                DisplayOrder = entity.DisplayOrder,
                LabelType = entity.Type,
                Text = entity.Text
            };
        }
    }
}