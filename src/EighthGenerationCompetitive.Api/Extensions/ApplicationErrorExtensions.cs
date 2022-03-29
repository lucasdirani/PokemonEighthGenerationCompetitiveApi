using EighthGenerationCompetitive.Business.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EighthGenerationCompetitive.Api.Extensions
{
    public static class ApplicationErrorExtensions
    {
        public static ProblemDetails ToProblemDetails(
            this AppError applicationError, 
            HttpContext context,
            HttpStatusCode statusCode) =>
            new()
            {
                Detail = applicationError.ErrorMessage,
                Title = applicationError.ErrorTitle,
                Status = (int) statusCode,
                Instance = context.Request.Path.Value,
                Type = string.Format("/errors/{0}", applicationError.ErrorCode)
            };
    }
}