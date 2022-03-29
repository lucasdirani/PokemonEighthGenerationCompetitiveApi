using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;

namespace EighthGenerationCompetitive.Api.Conventions
{
    public static class EighthGenerationCompetitiveApiConventions
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Get(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)] [FromQuery] object parameters)
        {
            throw new NotSupportedException();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Get(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)] string name)
        {
            throw new NotSupportedException();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Get(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)] string name,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)] Guid id)
        {
            throw new NotSupportedException();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Update(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)] object identifier,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)] [FromBody] object updateData)
        {
            throw new NotSupportedException();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Update(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)] object identifier,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)] object subResourceIdentifier,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)][FromBody] object updateData)
        {
            throw new NotSupportedException();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Remove(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)] object identifier,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)] object subResourceIdentifier,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)][FromBody] object removeData)
        {
            throw new NotSupportedException();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Register(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)][FromBody] object register)
        {
            throw new NotSupportedException();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Register(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)] object identifier,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)][FromBody] object register)
        {
            throw new NotSupportedException();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Login(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)][FromBody] object login)
        {
            throw new NotSupportedException();
        }
    }
}