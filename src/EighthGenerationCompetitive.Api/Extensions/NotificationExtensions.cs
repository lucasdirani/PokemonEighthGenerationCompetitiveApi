using EighthGenerationCompetitive.Business.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace EighthGenerationCompetitive.Api.Extensions
{
    public static class NotificationExtensions
    {
        public static IEnumerable<ProblemDetails> ToProblemDetails(
            this List<Notification> notifications, 
            HttpRequest httpRequest, 
            HttpStatusCode statusCode) 
            => notifications.Select(notification => new ProblemDetails()
            {
                Instance = httpRequest.Path.Value,
                Title = notification.Title,
                Detail = notification.Message,
                Status = (int) statusCode,
                Type = string.Format("/errors/{0}", notification.Code)
            });
    }
}