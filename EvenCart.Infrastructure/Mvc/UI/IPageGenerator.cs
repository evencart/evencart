namespace RoastedMarketplace.Infrastructure.Mvc.UI
{
    public interface IPageGenerator
    {
        void RegisterResource(string resourceName, string resourcePath, ResourceRegistrationType resourceRegistrationType);

        void EnqueueStyles(string[] resourceNames, string[] requiredResourceNames, ResourcePlacementType placementType);

        void EnqueueScripts(string[] resourceNames, string[] requiredResourceNames, ResourcePlacementType placementType);

        string RenderStyles(ResourcePlacementType placementType, bool includeAsBundle = false);

        string RenderScripts(ResourcePlacementType placementType, bool includeAsBundle = false);

    }
}