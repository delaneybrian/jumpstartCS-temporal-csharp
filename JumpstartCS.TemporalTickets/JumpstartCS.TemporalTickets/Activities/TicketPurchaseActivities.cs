using Temporalio.Activities;

namespace JumpstartCS.TemporalTickets.Activities
{
    public class TicketPurchaseActivities
    {
        [Activity]
        public Task HoldTickets()
        {
            return Task.CompletedTask;
        }
        
        [Activity]
        public Task ReserveTickets()
        {
            return Task.CompletedTask;
        }

        [Activity]
        public Task ReleaseTickets()
        {
            return Task.CompletedTask;
        }
    }
}
