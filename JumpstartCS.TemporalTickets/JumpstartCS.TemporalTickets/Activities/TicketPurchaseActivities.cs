using JumpstartCS.TemporalTickets.Definitions;
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
        public async Task HoldTickets(Guid customerId, Guid eventId, int numberOfTickets)
        {
            await _ticketRepository.ReserveTickets(customerId, eventId, numberOfTickets);
        }
        
        [Activity]
        public async Task<ICollection<Ticket>> ReserveTickets(Guid customerId, Guid eventId)
        {
            return await _ticketRepository.ConfirmTickets(customerId, eventId);
        }

        [Activity]
        public async Task ReleaseTickets(Guid customerId, Guid eventId)
        {
            await _ticketRepository.ReleaseTickets(customerId, eventId);
        }
    }
}
