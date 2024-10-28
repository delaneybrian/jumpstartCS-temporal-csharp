namespace JumpstartCS.TemporalTickets.Definitions.Configuration
{
    public record InMemoryTicketRepositoryOptions
    {
        public double FailureRatio { get; init; }
    }
}
