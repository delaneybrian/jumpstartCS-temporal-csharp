using System.Runtime.Serialization;

namespace JumpstartCS.TemporalTickets.Definitions
{
    [DataContract]
    public record Ticket
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public TicketStatus TicketStatus { get; set; }

        [DataMember]
        public Guid? AssignedToCustomerId { get; set; }
    }
}
