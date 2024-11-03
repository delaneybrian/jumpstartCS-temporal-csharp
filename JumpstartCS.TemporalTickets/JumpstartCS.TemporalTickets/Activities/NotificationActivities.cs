using JumpstartCS.TemporalTickets.Interfaces;
using Temporalio.Activities;

namespace JumpstartCS.TemporalTickets.Activities
{
    public class NotificationActivities
    {
        private readonly INotificationService _notificationService;

        public NotificationActivities(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [Activity]
        public async Task NotifyUser(Guid customerId)
        {
            await _notificationService.SendPushNotification(customerId, "Tickets bought");
        }
    }
}
