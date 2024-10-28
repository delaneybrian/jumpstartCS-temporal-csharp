namespace JumpstartCS.TemporalTickets.Interfaces
{
    public interface ITicketRepository
    {
        public ICollection<Guid> ReserveTickets(Guid customerId, Guid eventId, int numberOfTickets);

        public bool ReleaseTickets(Guid customerId, Guid eventId);

        public bool ConfirmTickets(Guid customerId, Guid eventId);
    }
}
