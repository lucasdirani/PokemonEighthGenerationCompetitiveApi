using EighthGenerationCompetitive.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace EighthGenerationCompetitive.Api.Providers
{
    public class ApiVersioningErrorResponseProvider : IErrorResponseProvider
    {
        public IActionResult CreateResponse(ErrorResponseContext context)
        {
            var apiVersioningError = new ProblemDetails()
            {
                Status = context.StatusCode,
                Instance = context.Request.Path.Value,
                Detail = context.MessageDetail,
                Title = context.Message,
                Type = string.Format("/errors/{0}", context.ErrorCode)
            };

            var response = new ObjectResult(apiVersioningError)
            {
                StatusCode = context.StatusCode,
                ContentTypes = ObjectResultHelper.BuildMediaTypeCollectionWith("application/problem+json")
            };

            return response;
        }
    }
}