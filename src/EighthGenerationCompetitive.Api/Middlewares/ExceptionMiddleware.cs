using EighthGenerationCompetitive.Api.Extensions;
using EighthGenerationCompetitive.Business.Errors;
using EighthGenerationCompetitive.Business.Errors.Factory;
using KissLog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _nextRequest;

        public ExceptionMiddleware(RequestDelegate nextRequest)
        {
            _nextRequest = nextRequest;          
        }

        public async Task InvokeAsync(HttpContext httpContext, ILogger logger)
        {
            try
            {
                await _nextRequest(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext);

                logger.Log(LogLevel.Error, ex);
            }
        }

        private async static Task HandleExceptionAsync(HttpContext httpContext)
        {
            if (!httpContext.Response.HasStarted)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                httpContext.Response.ContentType = "application/problem+json";

                ProblemDetails exceptionResponse = BuildExceptionResponse(httpContext);

                await httpContext.Response.WriteAsync(exceptionResponse.ToJson());
            }
        }

        private static ProblemDetails BuildExceptionResponse(HttpContext httpContext) 
            => AppErrorFactory
                .Make(AppErrorType.InternalServer)
                .ToProblemDetails(httpContext, HttpStatusCode.InternalServerError);
    }
}