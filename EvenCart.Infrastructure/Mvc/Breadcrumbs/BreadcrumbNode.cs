using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Infrastructure.Mvc.Breadcrumbs
{
    public class BreadcrumbNode : FoundationModel
    {
        public string Url { get; set; }

        public string DisplayText { get; set; }

        public string Description { get; set; }
    }
}