using EighthGenerationCompetitive.Api.Enumerations;

namespace EighthGenerationCompetitive.Api.Extensions
{
    internal static class ResourceIdentifierExtensions
    {
        public static bool IsNameResource(this ResourceIdentifier resourceIdentifier)
        {
            return resourceIdentifier == ResourceIdentifier.Name;
        }
    }
}