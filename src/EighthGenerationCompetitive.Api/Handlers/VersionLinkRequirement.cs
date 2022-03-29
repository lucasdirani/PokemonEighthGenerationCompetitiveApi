using Microsoft.AspNetCore.Routing;
using RiskFirst.Hateoas;
using System;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Api.Handlers
{
    public class VersionLinkRequirement<TResource> : LinksHandler<VersionLinkRequirement<TResource>>, ILinksRequirement
    {
        public string Id { get; set; }
        public string RouteName { get; set; }
        public Func<TResource, RouteValueDictionary> GetRouteValues { get; set; }

        protected override Task HandleRequirementAsync(LinksHandlerContext context, VersionLinkRequirement<TResource> requirement)
        {
            if (string.IsNullOrEmpty(requirement.RouteName))
            {
                context.Skipped(requirement, LinkRequirementSkipReason.Error, $"Requirement did not have a RouteName specified for link: {requirement.Id}");

                return Task.CompletedTask;
            }

            var route = context.RouteMap.GetRoute(requirement.RouteName);

            if (route == null)
            {
                context.Skipped(requirement, LinkRequirementSkipReason.Error, $"No route was found for route name: {requirement.RouteName}");

                return Task.CompletedTask;
            }

            var values = new RouteValueDictionary();

            if (requirement.GetRouteValues != null)
            {
                values = requirement.GetRouteValues((TResource)context.Resource);
            }

            var link = new LinkSpec(requirement.Id, route, values);

            if (context.ActionContext.RouteData.Values.TryGetValue("version", out var version))
            {
                link.RouteValues.Add("version", version);
            }

            context.Links.Add(link);

            context.Handled(requirement);

            return Task.CompletedTask;
        }
    }
}