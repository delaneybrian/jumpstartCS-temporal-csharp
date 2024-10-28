using Temporalio.Activities;

namespace JumpstartCS.TemporalTickets.Activities
{
    public class NotificationActivities
    {
        [Activity]
        public Task NotifyUser()
        {
            return Task.CompletedTask;
        }
    }
}
