using JumpstartCS.TemporalTickets.Definitions.Configuration;
using JumpstartCS.TemporalTickets.Definitions.Exceptions;
using JumpstartCS.TemporalTickets.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;

namespace JumpstartCS.TemporalTickets.Infrastructure
{
    public record Ticket
    {
        public Guid Id { get; init; }

        public TicketStatus TicketStatus { get; init; }

        public Guid? AssignedToCustomerId { get; init; }
    }

    public enum TicketStatus
    {
        Default = 0,
        Unassigned = 1,
        Reserved = 2,
        Confirmed = 3,
        Cancelled = 4
    }

    public class InMemoryTicketRepository : ITicketRepository
    {
        private readonly ConcurrentDictionary<Guid, ConcurrentBag<Ticket>> TicketsByEventId = new()
        {
            [Guid.Parse("a123fabc-1234-4e21-8d5b-12e4c9ab89c7")] = GenerateTickets(10),
            [Guid.Parse("f2341cde-5678-41a1-92ba-45b6ec124d3f")] = GenerateTickets(10),
            [Guid.Parse("b345d9ef-9101-4b24-a456-78d9f10cba4f")] = GenerateTickets(10),
            [Guid.Parse("c456e0ab-2345-4c53-b78a-90bcfe32d5e6")] = GenerateTickets(10),
            [Guid.Parse("d567f1bc-6789-4d67-c89a-1234dc98f7e9")] = GenerateTickets(10),
        };

        private readonly IOptions<InMemoryTicketRepositoryOptions> _options;

        private readonly Random _random;

        public InMemoryTicketRepository(IOptions<InMemoryTicketRepositoryOptions> options)
        {
            _options = options;
            _random = new Random();
        }

        public bool ConfirmTickets(Guid customerId, Guid eventId)
        {
            if (IsTransientFailure())
                throw new TransientTicketsException();
        }

        public bool ReleaseTickets(Guid customerId, Guid eventId)
        {
            if (IsTransientFailure())
                throw new TransientTicketsException();
        }

        public ICollection<Guid> ReserveTickets(Guid customerId, Guid eventId, int numberOfTickets)
        {
            if (IsTransientFailure())
                throw new TransientTicketsException();

            var heldTickets = TicketsByEventId[eventId]
                .Where(x => x.AssignedToCustomerId == customerId);

            foreach(var heldTicket in heldTickets)
            {
                heldTicket = heldTicket with { TicketStatus = TicketStatus.Confirmed };
            }
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
