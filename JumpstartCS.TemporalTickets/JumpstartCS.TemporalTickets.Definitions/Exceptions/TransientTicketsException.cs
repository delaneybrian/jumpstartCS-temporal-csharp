namespace JumpstartCS.TemporalTickets.Definitions.Exceptions
{
    [Serializable]
    public class TransientTicketsException : Exception
    {
        public TransientTicketsException()
            : base() { }

        public TransientTicketsException(string message)
            : base(message) { }

        public TransientTicketsException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
