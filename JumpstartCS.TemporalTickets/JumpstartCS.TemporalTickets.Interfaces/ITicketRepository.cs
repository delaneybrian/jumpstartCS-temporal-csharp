using JumpstartCS.TemporalTickets.Definitions;

namespace JumpstartCS.TemporalTickets.Interfaces
{
    public interface ITicketRepository
    {
        public Task ReserveTickets(Guid customerId, Guid eventId, int numberOfTickets);

        public Task<ICollection<Ticket>> ConfirmTickets(Guid customerId, Guid eventId);

        public Task ReleaseTickets(Guid customerId, Guid eventId);
    }
}
