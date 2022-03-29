using EighthGenerationCompetitive.Api.Enumerations;
using EighthGenerationCompetitive.Api.Extensions;
using EighthGenerationCompetitive.Business.Errors;
using EighthGenerationCompetitive.Business.Interfaces;
using EighthGenerationCompetitive.Business.Monads;
using EighthGenerationCompetitive.Business.Notifications;
using EighthGenerationCompetitive.Business.Utils;
using KissLog;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RiskFirst.Hateoas;
using RiskFirst.Hateoas.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Api.BaseControllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier _notifier;
        private readonly ILinksService _linksService;

        public IUser ApplicationUser { get; }
        public ILogger Logger { get; }

        protected MainController(
            INotifier notifier, 
            IUser user,
            ILinksService linksService,
            ILogger logger)
        {
            _notifier = notifier;
            _linksService = linksService;
            Logger = logger;
            ApplicationUser = user;
        }

        protected bool EndpointOperationWasSuccessful => !_notifier.HasNotification();

        protected bool EndpointOperationFailed => !EndpointOperationWasSuccessful;

        protected ActionResult CustomResponseForGetEndpoint(
            object response = null, 
            Resource resource = Resource.Found) =>
            CustomResponse(resource, response);

        protected ActionResult CustomResponseForPutEndpoint(
            object response = null, 
            Resource resource = Resource.Found) =>
            CustomResponse(resource, response);

        protected ActionResult CustomResponseForPutEndpoint(Result operationResult) =>
            CustomResponse(operationResult);

        protected ActionResult CustomResponseForPatchEndpoint(Result operationResult) =>
            CustomResponse(operationResult);

        protected ActionResult CustomResponseForPostEndpoint(
            object response,
            object resourceIdentifier = null,
            string resourceCreatedAt = null,
            ResourceIdentifier resourceIdentifierType = ResourceIdentifier.Name) =>
            CustomResponse(response, resourceIdentifier, resourceCreatedAt, resourceIdentifierType);

        protected ActionResult CustomResponseForDeleteEndpoint(Result operationResult)
        {
            if (EndpointOperationWasSuccessful && operationResult.IsSuccess)
            {
                return Ok();
            }

            var errorResult = _notifier.GetNotifications().ToProblemDetails(Request, HttpStatusCode.BadRequest);

            return BadRequest(errorResult);
        }

        protected ActionResult CustomResponseFor(
            ModelStateDictionary modelState, 
            AppError applicationError = null)
        {
            if (!modelState.IsValid)
            {
                NotifyErrorsForInvalidModelState(modelState, applicationError);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponseFor(IEnumerable<IdentityError> errors, string actionName)
        {
            foreach (var error in errors)
                NotifyError(error, errorTitle: actionName);

            return CustomResponse();
        }

        protected void SetXPaginationHeaderFor<TEntity>(PagedList<TEntity> pagedList)
        {
            var pagination = new
            {
                pagedList.TotalCount,
                pagedList.PageSize,
                pagedList.CurrentPage,
                pagedList.TotalPages,
                pagedList.HasNext,
                pagedList.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
        }

        protected void AddHateoasFor<TEntity>(PagedList<TEntity> entities)
            where TEntity : LinkContainer =>
            entities.AddHateoasWith(_linksService);

        protected async Task AddHateoasForAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : LinkContainer =>
            await entities.AddHateoasWithAsync(_linksService);

        protected async Task AddHateoasForAsync<TEntity>(TEntity entity)
            where TEntity : LinkContainer =>
            await _linksService.AddLinksAsync(entity);

        protected void NotifyError(string errorTitle, string errorMessage, string errorCode) =>
            _notifier.Handle(new Notification(errorMessage, errorCode, errorTitle));

        protected void NotifyError(AppError applicationError) =>
            _notifier.Handle(new Notification(
                applicationError.ErrorMessage, 
                applicationError.ErrorCode, 
                applicationError.ErrorTitle));

        protected void NotifyError(IdentityError errorDetails, string errorTitle) =>
            _notifier.Handle(new Notification(errorDetails.Description, errorDetails.Code, errorTitle));

        private ActionResult CustomResponse(object response = null)
        {
            if (EndpointOperationWasSuccessful)
            {
                return Ok(response);
            }

            return BadRequest(_notifier.GetNotifications().ToProblemDetails(Request, HttpStatusCode.BadRequest));
        }

        private ActionResult CustomResponse(Result result)
        {
            if (EndpointOperationWasSuccessful && result.IsSuccess)
            {
                return NoContent();
            }

            var errorResult = _notifier.GetNotifications().ToProblemDetails(Request, HttpStatusCode.BadRequest);

            return BadRequest(errorResult);
        }

        private ActionResult CustomResponse(Resource resource, object response)
        {
            if (resource == Resource.NotFound)
            {
                return NotFound(_notifier.GetNotifications().ToProblemDetails(Request, HttpStatusCode.NotFound));
            }

            return CustomResponse(response);
        } 

        private ActionResult CustomResponse(
            object response, 
            object resourceIdentifier, 
            string resourceCreatedAt, 
            ResourceIdentifier resourceIdentifierType)
        {
            if (EndpointOperationFailed)
            { 
                return BadRequest(_notifier.GetNotifications().ToProblemDetails(Request, HttpStatusCode.BadRequest));
            }

            if (resourceIdentifierType.IsNameResource())
            {
                return CreatedAtAction(resourceCreatedAt, new { name = resourceIdentifier }, response);
            }

            return CreatedAtAction(resourceCreatedAt, new { id = resourceIdentifier }, response);
        }

        private void NotifyErrorsForInvalidModelState(
            ModelStateDictionary modelState, 
            AppError applicationError)
        {
            var errors = modelState.Values.SelectMany(m => m.Errors);

            foreach (var error in errors)
            {
                var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;

                var errorCode = applicationError != null ? applicationError.ErrorCode : error.Exception.HResult.ToString();

                var errorTitle = applicationError != null ? applicationError.ErrorTitle : error.Exception.Source;

                NotifyError(errorTitle, errorMessage, errorCode);
            }
        }
    }
}