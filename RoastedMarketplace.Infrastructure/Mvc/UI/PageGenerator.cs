using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoastedMarketplace.Core;

namespace RoastedMarketplace.Infrastructure.Mvc.UI
{
    public class PageGenerator : IPageGenerator
    {

        private readonly IDictionary<ResourceRegistrationType, IList<Resource>> _resourceRegistrations;

        public PageGenerator()
        {
            _resourceRegistrations = new Dictionary<ResourceRegistrationType, IList<Resource>>
            {
                {ResourceRegistrationType.Style, new List<Resource>()},
                {ResourceRegistrationType.Script, new List<Resource>()}
            };
        }

        public void RegisterResource(string resourceName, string resourcePath, ResourceRegistrationType resourceRegistrationType)
        {
            if (string.IsNullOrEmpty(resourceName))
                throw new Exception("Can't register a resource with empty name");

            var resource =
                _resourceRegistrations[resourceRegistrationType].FirstOrDefault(x => x.ResourceName == resourceName);

            if (resource == null)
            {
                resource = new Resource()
                {
                    ResourceName = resourceName,
                    ResourcePath = resourcePath,
                    RegistrationType = resourceRegistrationType,
                    PlacementType =
                        resourceRegistrationType == ResourceRegistrationType.Script
                            ? ResourcePlacementType.BeforeEndBodyTag
                            : ResourcePlacementType.HeadTag,
                    Enqueued = false,
                    Rendered = false
                };
                _resourceRegistrations[resourceRegistrationType].Add(resource);
            }
            else
            {
                resource.ResourcePath = resourcePath;
            }

        }

        public void EnqueueStyles(string[] resourceNames, string[] requiredResourceNames, ResourcePlacementType placementType = ResourcePlacementType.HeadTag)
        {
            if (requiredResourceNames == null)
                requiredResourceNames = new string[] { };

            //append required resources
            foreach (var resourceName in requiredResourceNames)
            {
                EnqueueResource(resourceName, ResourceRegistrationType.Script, placementType, null);
            }

            //first get the dependent resources
            var dependentResources =
                _resourceRegistrations[ResourceRegistrationType.Style].Where(
                    x => requiredResourceNames.Contains(x.ResourceName)).ToList();

            //and now the actual resources
            foreach (var resourceName in resourceNames)
            {
                EnqueueResource(resourceName, ResourceRegistrationType.Style, placementType, dependentResources);
            }

        }

        public void EnqueueScripts(string[] resourceNames, string[] requiredResourceNames, ResourcePlacementType placementType = ResourcePlacementType.BeforeEndBodyTag)
        {
            if (requiredResourceNames == null)
                requiredResourceNames = new string[] {};

            //append required resources
            foreach (var resourceName in requiredResourceNames)
            {
                EnqueueResource(resourceName, ResourceRegistrationType.Script, placementType, null);
            }

            //first get the dependent resources
            var dependentResources =
                _resourceRegistrations[ResourceRegistrationType.Script].Where(
                    x => requiredResourceNames.Contains(x.ResourceName)).ToList();

            //and now the actual resources
            foreach (var resourceName in resourceNames)
            {
                EnqueueResource(resourceName, ResourceRegistrationType.Script, placementType, dependentResources);
            }

        }

        public string RenderStyles(ResourcePlacementType placementType, bool includeAsBundle = false)
        {
            return RenderResource(ResourceRegistrationType.Style, placementType, includeAsBundle);
        }

        public string RenderScripts(ResourcePlacementType placementType, bool includeAsBundle = false)
        {
            return RenderResource(ResourceRegistrationType.Script, placementType, includeAsBundle);
        }

        private string RenderResource(ResourceRegistrationType registrationType,
            ResourcePlacementType placementType, bool includeAsBundle = false)
        {
            var resourcesEnqueued =
                _resourceRegistrations[registrationType].Where(x => x.Enqueued && x.PlacementType == placementType)
                    .ToList();

            //set all as unrendered because we are using singleton instance
            foreach (var resource in resourcesEnqueued)
                resource.Rendered = false;

            var renderFormat = registrationType == ResourceRegistrationType.Style
                ? "<link href='{0}' rel='stylesheet' type='text/css' />"
                : "<script src='{0}' type='text/javascript'></script>";
            var renderBuilder = new StringBuilder();
            foreach (var resource in resourcesEnqueued)
            {
               renderBuilder.Append(_RenderSingleResource(resource, renderFormat));
            }

            return renderBuilder.ToString();
        }

        private string _RenderSingleResource(Resource resource, string renderFormat)
        {
            if (resource.Rendered)
                return string.Empty;

            var renderBuilder = new StringBuilder();
            if (resource.DependsOn != null)
            {
                //first render the dependent ones
                foreach (var r in resource.DependsOn)
                {
                    renderBuilder.Append(_RenderSingleResource(r, renderFormat));
                }
            }
            
            if (!string.IsNullOrEmpty(resource.ResourcePath))
            {
                var resourcePath = WebHelper.GetUrlFromPath(resource.ResourcePath);
                renderBuilder.Append(Environment.NewLine + string.Format(renderFormat, resourcePath));
            }
            //mark current resource as rendered
            resource.Rendered = true;

            return renderBuilder.ToString();

        }

        private void EnqueueResource(string resourceName, ResourceRegistrationType resourceRegistrationType, ResourcePlacementType placementType, IList<Resource> dependsOn)
        {
            var registeredResource =
                _resourceRegistrations[resourceRegistrationType].FirstOrDefault(x => x.ResourceName == resourceName);
            if (registeredResource == null)
            {
                //may be this resource will be registered later, do it now
                RegisterResource(resourceName, string.Empty, resourceRegistrationType);

                //query again
                registeredResource =
                _resourceRegistrations[resourceRegistrationType].First(x => x.ResourceName == resourceName);
            }
            registeredResource.PlacementType = placementType;
            registeredResource.Enqueued = true;
            if(dependsOn != null)
                registeredResource.DependsOn = dependsOn;

        }
    }
}