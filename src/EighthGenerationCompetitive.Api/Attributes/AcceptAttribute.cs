using EighthGenerationCompetitive.Api.Extensions;
using EighthGenerationCompetitive.Business.Errors;
using EighthGenerationCompetitive.Business.Errors.Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Linq;
using System.Net;
using System.Text;

namespace EighthGenerationCompetitive.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AcceptAttribute : Attribute, IActionConstraint
    {
        const string AcceptAny = "*/*";

        public string[] AcceptValues { get; private set; }

        public int Order => 0;

        public AcceptAttribute(params string[] acceptValues)
        {
            AcceptValues = acceptValues;
        }

        public bool Accept(ActionConstraintContext context)
        {
            if (!context.RouteContext.HttpContext.Request.Headers.ContainsKey("Accept")) return true;

            string acceptValueInRequest = context.RouteContext.HttpContext.Request.Headers["Accept"].ToArray()[0];

            string[] clientAccepts = acceptValueInRequest.Split(new char[] { ',', ';' }, StringSplitOptions.None);

            if (clientAccepts.Contains(AcceptAny)) return true;

            if (clientAccepts.Any(clientAccept => AcceptValues.Contains(clientAccept))) return true;

            if (!context.RouteContext.HttpContext.Response.HasStarted)
                SetNotAcceptableResponseForHttpContext(context.RouteContext.HttpContext);

            return false;
        }

        private static void SetNotAcceptableResponseForHttpContext(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = (int) HttpStatusCode.NotAcceptable;

            httpContext.Response.ContentType = "application/problem+json";

            var notAcceptableResponse = BuildNotAcceptableResponse(httpContext);

            var notAcceptableResponseData = Encoding.UTF8.GetBytes(notAcceptableResponse.ToJson());

            httpContext.Response.Body.Write(notAcceptableResponseData, 0, notAcceptableResponseData.Length);
        }

        private static ProblemDetails BuildNotAcceptableResponse(HttpContext httpContext) =>
            AppErrorFactory
              .Make(AppErrorType.NotAccepted)
              .ToProblemDetails(httpContext, HttpStatusCode.NotAcceptable);
    }
}