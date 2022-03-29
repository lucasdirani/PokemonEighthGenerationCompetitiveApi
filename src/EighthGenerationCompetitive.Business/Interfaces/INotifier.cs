using EighthGenerationCompetitive.Business.Notifications;
using System.Collections.Generic;

namespace EighthGenerationCompetitive.Business.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}