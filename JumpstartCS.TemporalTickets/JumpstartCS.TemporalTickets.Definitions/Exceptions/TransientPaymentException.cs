namespace JumpstartCS.TemporalTickets.Definitions.Exceptions
{
    [Serializable]
    public class TransientPaymentException : Exception
    {
        public TransientPaymentException()
            : base() { }

        public TransientPaymentException(string message)
            : base(message) { }

        public TransientPaymentException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
