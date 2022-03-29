using Microsoft.AspNetCore.Mvc.Formatters;

namespace EighthGenerationCompetitive.Api.Helpers
{
    public static class ObjectResultHelper
    {
        public static MediaTypeCollection BuildMediaTypeCollectionWith(params string[] contentTypes)
        {
            var mediaTypeCollection = new MediaTypeCollection();

            foreach (var contentType in contentTypes)
            {
                mediaTypeCollection.Add(contentType);
            }

            return mediaTypeCollection;
        }
    }
}