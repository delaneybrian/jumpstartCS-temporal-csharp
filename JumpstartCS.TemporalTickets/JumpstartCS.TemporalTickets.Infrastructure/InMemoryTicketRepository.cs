using System.Collections.Concurrent;
using JumpstartCS.TemporalTickets.Definitions;
using JumpstartCS.TemporalTickets.Definitions.Configuration;
using JumpstartCS.TemporalTickets.Definitions.Exceptions;
using JumpstartCS.TemporalTickets.Interfaces;
using Microsoft.Extensions.Options;

namespace JumpstartCS.TemporalTickets.Infrastructure
{
    public class InMemoryTicketRepository : ITicketRepository
    {
        private readonly ConcurrentDictionary<Guid, ConcurrentBag<Ticket>> TicketsByEventId = new()
        {
            [Guid.Parse("a123fabc-1234-4e21-8d5b-12e4c9ab89c7")] = GenerateTickets(10),
            [Guid.Parse("f2341cde-5678-41a1-92ba-45b6ec124d3f")] = GenerateTickets(10),
        };

        private readonly IOptions<InMemoryTicketRepositoryOptions> _options;

        private readonly Random _random;

        public InMemoryTicketRepository(IOptions<InMemoryTicketRepositoryOptions> options)
        {
            _options = options;
            _random = new Random();
        }


        public Task ReserveTickets(Guid customerId, Guid eventId, int numberOfTickets)
        {
            if (IsTransientFailure())
                throw new TransientTicketsException();

            if (!TicketsByEventId.ContainsKey(eventId))
                throw new InvalidEventException();

            var ticketsToHold = TicketsByEventId[eventId]
                .Where(x => x.TicketStatus == TicketStatus.Unassigned)
                .Take(numberOfTickets)
                .ToList();

            if (ticketsToHold.Count < numberOfTickets)
                throw new InsufficientTicketsAvaibableException();

            foreach(var ticketToHold in ticketsToHold)
            {
                ticketToHold.TicketStatus = TicketStatus.Reserved;
                ticketToHold.AssignedToCustomerId = customerId;
            }

            return Task.CompletedTask;
        }

        public Task<ICollection<Ticket>> ConfirmTickets(Guid customerId, Guid eventId)
        {
            if (IsTransientFailure())
                throw new TransientTicketsException();

            var heldTickets = TicketsByEventId[eventId]
                .Where(x => x.AssignedToCustomerId == customerId && x.TicketStatus == TicketStatus.Reserved)
                .ToList();

            foreach (var heldTicket in heldTickets)
            {
                heldTicket.TicketStatus = TicketStatus.Confirmed;
            }

            return Task.FromResult<ICollection<Ticket>>(heldTickets);
        }

        public Task ReleaseTickets(Guid customerId, Guid eventId)
        {
            if (IsTransientFailure())
                throw new TransientTicketsException();

            var heldTickets = TicketsByEventId[eventId]
               .Where(x => x.AssignedToCustomerId == customerId && x.TicketStatus == TicketStatus.Reserved)
               .ToList();

            foreach (var heldTicket in heldTickets)
            {
                heldTicket.TicketStatus = TicketStatus.Unassigned;
                heldTicket.AssignedToCustomerId = null;
            }

            return Task.CompletedTask;
        }

        private bool IsTransientFailure()
        {
            return _random.NextDouble() < _options.Value.FailureRatio;
        }

        private static ConcurrentBag<Ticket> GenerateTickets(int ticketsToCreate)
        {
            var tickets = new ConcurrentBag<Ticket>();

            for(int i = 0; i < ticketsToCreate; i++)
            {
                var ticket = new Ticket
                {
                    Id = Guid.NewGuid(),
                    AssignedToCustomerId = null,
                    TicketStatus = TicketStatus.Unassigned
                };

                tickets.Add(ticket);
            }

            return tickets;
        }
    }
}
