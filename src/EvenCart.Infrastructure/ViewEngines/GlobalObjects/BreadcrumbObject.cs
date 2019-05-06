using EvenCart.Infrastructure.Extensions;

namespace EvenCart.Infrastructure.ViewEngines.GlobalObjects
{
    public class BreadcrumbObject : GlobalObject
    {
        public override object GetObject()
        {
            return ApplicationEngine.CurrentHttpContext.GetBreadcrumb();
        }

        public override bool RenderInAdmin => false;

        public override bool RenderInPublic => true;
    }
}