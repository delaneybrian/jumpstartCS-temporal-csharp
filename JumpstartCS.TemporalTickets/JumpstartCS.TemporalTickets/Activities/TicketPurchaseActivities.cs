using JumpstartCS.TemporalTickets.Interfaces;
using Temporalio.Activities;

namespace JumpstartCS.TemporalTickets.Activities
{
    public class TicketPurchaseActivities
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketPurchaseActivities(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

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
