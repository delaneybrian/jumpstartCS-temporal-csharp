namespace JumpstartCS.TemporalTickets.Definitions.Configuration
{
    public record StripePaymentGatewayOptions
    {
        public double FailureRatio { get; init; }
    }
}
