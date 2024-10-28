namespace JumpstartCS.TemporalTickets.Definitions.Configuration
{
    public record SmsNotificationServiceOptions
    {
        public long MinDelayMillis { get; init; }

        public long MaxDelayMillis { get; init; }
    }
}
